import { GoogleOAuthProvider } from "@react-oauth/google";
import { Children, ReactNode } from "react";
import { AuthProvider } from "./auth-context";
import { UserProvider } from "./user-context";

function AppProviders({ children }: { children: ReactNode }) {
    return (
        <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
            <AuthProvider>
                <UserProvider>
                    {children}
                </UserProvider>
            </AuthProvider>
        </GoogleOAuthProvider>
    )
}
export default AppProviders;