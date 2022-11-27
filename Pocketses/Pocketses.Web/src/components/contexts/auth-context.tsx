import axios, { AxiosInstance } from 'axios';
import React, { useCallback, useContext, useMemo, PropsWithChildren } from 'react'
import { useNavigate } from 'react-router-dom';
import useLocalStorageState from 'use-local-storage-state'
import { apiRoutes } from '../../api/api-routes';
import LoginPage from '../../Pages/Authentication/login-page';

type AuthContextProps = {
    token: string | null;
    logout: () => void;
    authenticate: (code: string | null) => void;
    http: AxiosInstance
}

const AuthContext = React.createContext<AuthContextProps | undefined>(undefined);

const AuthProvider = (props: PropsWithChildren) => {
    const [token, setToken] = useLocalStorageState<string | null>('token', { defaultValue: null });
    const navigate = useNavigate();

    const logout = useCallback(() => {
        navigate('/');
        setToken(null);
    }, [navigate, setToken]);

    const authenticate = (code: string | null) => {
        const url = apiRoutes.base + apiRoutes.auth;
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
            baseURL: apiRoutes.base,
            headers: {
                Authorization: 'Bearer ' + token || '',
                'Content-Type': 'application/json'
            }
        });

        http.interceptors.response.use(undefined, (error) => {
            if (error.response.status == 401) {
                logout();
                return;
            }
            throw error;
        });

        return http;
    }, [logout, token]);

    return (
        <AuthContext.Provider value={{ token, logout, authenticate, http }}>
            {!token ? <LoginPage /> : props.children}
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