import {GoogleOAuthProvider} from "@react-oauth/google";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
import {PropsWithChildren} from "react";
import {AuthProvider} from "./auth-context";
import {UserProvider} from "./user-context";
import {ModalProvider} from "./modal-provider";

function AppProviders(props: PropsWithChildren) {

    const queryClient = new QueryClient();

    return (
        <ModalProvider>
            <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
                <AuthProvider>
                    <UserProvider>
                        <QueryClientProvider client={queryClient}>
                            {props.children}
                        </QueryClientProvider>
                    </UserProvider>
                </AuthProvider>
            </GoogleOAuthProvider>
        </ModalProvider>
    )
}

export default AppProviders;