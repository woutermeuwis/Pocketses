import LoginPage from "./Pages/Authentication/login-page";
import { useAuth } from "./components/contexts/auth-context";
import Application from "./components/infrastructure/application";

function App() {
    const auth = useAuth();
    return auth.token 
        ? <Application />
        : <LoginPage/>
}

export default App
