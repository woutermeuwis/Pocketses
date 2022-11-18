import React, {ReactNode, useContext} from 'react'
import {useAuth} from './auth-context';
import jwt, {JwtDecodeOptions} from "jwt-decode"

interface UserContextProps  {
    email: string;
    name: string;
    givenName: string;
    familyName: string;
    userId: string;
    image: string
}

interface JWT  {
    email: string;
    family_name: string;
    given_name: string;
    unique_name: string;
    sub: string;
    picture:string;
}

const UserContext = React.createContext<UserContextProps | undefined>(undefined);

const UserProvider = ({children}: { children: ReactNode }) => {

    const getUser = (token: string | null): UserContextProps | undefined => {
        if (!token)
            return undefined;

        let user = jwt(token) as JWT;
        return {
            email: user.email,
            familyName: user.family_name,
            givenName: user.given_name,
            name: user.unique_name,
            userId: user.sub,
            image: user.picture
        };
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

export {UserProvider, useUser}