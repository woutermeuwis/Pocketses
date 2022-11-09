import LoginPage from "./Pages/authentication/loginPage";
import AppRouter from "./Pages/infrastructure/app-router";
import useToken from "./components/hooks/useToken";
import { useAuth } from "./components/contexts/auth-context";

function App() {
    const auth = useAuth();
    return auth.token 
        ? <AppRouter />
        : <LoginPage/>
}

export default App
