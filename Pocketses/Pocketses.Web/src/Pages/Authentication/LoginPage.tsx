import { CredentialResponse, GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import { useAuth } from "../../components/contexts/auth-context";
import LoginHeading from "../../components/headings/loginHeading";

const LoginPage = () => {
    const { authenticate, logout } = useAuth();
    return (
        <div className="flex flex-col justify-center items-center h-full">
            <div className="bg-white shadow-md rounded p-12" >
                <LoginHeading>Pocketses</LoginHeading>
                <GoogleLogin onSuccess={res => authenticate(res.credential || null)}
                    onError={logout} />
            </div>
        </div>
    )
}
export default LoginPage;