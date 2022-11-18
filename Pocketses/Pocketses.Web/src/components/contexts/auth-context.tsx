import axios, { AxiosInstance } from 'axios';
import React, { ReactNode, useCallback, useContext, useMemo } from 'react'
import { useNavigate } from 'react-router-dom';
import useLocalStorageState from 'use-local-storage-state'
import { apiRoutes } from '../../api/apiRoutes';
import LoginPage from '../../pages/authentication/loginPage';

type AuthContextProps = {
    token: string | null;
    logout: () => void;
    authenticate: (code: string | null) => void;
    http: AxiosInstance
}

const AuthContext = React.createContext<AuthContextProps | undefined>(undefined);

const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [token, setToken] = useLocalStorageState<string | null>('token', { defaultValue: null });
    const navigate = useNavigate();

    const logout = useCallback(() => {
        navigate('/');
        setToken(null);
    }, [navigate, setToken]);

    const authenticate = (code: string | null) => {
        const url = apiRoutes.baseUrl + apiRoutes.auth;
        const config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };

        axios
            .post(url, code, config)
            .then((response) => {
                if(response.status==200)
                    setToken(response.data)
                else
                    logout()
            });
    }

    const http = useMemo(() => {
        const http = axios.create({
            headers: {
                Authorization: 'Bearer ' + token || ''
            }
        });

        http.interceptors.response.use(undefined, (error) => {
            if (error.response.status == 404) {
                logout();
                return;
            }
            throw error;
        });

        return http;
    }, [logout, token]);

    return (
        <AuthContext.Provider value={{ token, logout, authenticate, http }}>
            {!token ? <LoginPage /> : children}
        </AuthContext.Provider>
    )
}

function useAuth(): AuthContextProps {
    const ctx = useContext(AuthContext);
    if (ctx === undefined) {
        throw Error('useAuth must be used within AuthProvider');
    }
    return ctx;
}

export { AuthProvider, useAuth };