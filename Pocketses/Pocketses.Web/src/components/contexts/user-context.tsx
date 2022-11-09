import React, { ReactNode, useContext } from 'react'
import { useAuth } from './auth-context';
import jwt from "jwt-decode"

type UserContextProps = {
    email: string;
    name: string;
    givenName: string;
    familyName: string;
    userId: string
}

const UserContext = React.createContext<UserContextProps | undefined>(undefined);

const UserProvider = ({ children }: { children: ReactNode }) => {

    const getUser = (token: string | null) : UserContextProps|undefined => {
        if (!token)
            return undefined;

        return jwt(token) as UserContextProps;
    }

    return (
        <UserContext.Provider value={getUser(useAuth().token)}>
            {children}
        </UserContext.Provider>
    )
}

function useUser(): UserContextProps {
    const ctx = useContext(UserContext);
    if (ctx === undefined) {
        throw Error('useUser must be used within UserProvider');
    }
    return ctx;
}

export { UserProvider, useUser }