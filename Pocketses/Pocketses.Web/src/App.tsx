import LoginPage from "./pages/authentication/loginPage";
import AppRouter from "./pages/infrastructure/app-router";
import useToken from "./components/hooks/useToken";
import { useAuth } from "./components/contexts/auth-context";
import Application from "./pages/main/application";

function App() {
    const auth = useAuth();
    return auth.token 
        ? <Application />
        : <LoginPage/>
}

export default App
