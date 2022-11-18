import { GoogleOAuthProvider } from "@react-oauth/google";
import {  PropsWithChildren } from "react";
import { AuthProvider } from "./auth-context";
import { UserProvider } from "./user-context";

function AppProviders(props: PropsWithChildren) {
    return (
        <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
            <AuthProvider>
                <UserProvider>
                    {props.children}
                </UserProvider>
            </AuthProvider>
        </GoogleOAuthProvider>
    )
}
export default AppProviders;