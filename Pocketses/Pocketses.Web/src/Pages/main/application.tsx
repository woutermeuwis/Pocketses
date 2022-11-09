import { apiRoutes } from "../../api/apiRoutes";
import { useAuth } from "../../components/contexts/auth-context";
import { useUser } from "../../components/contexts/user-context";

const Application = () => {

    const { logout, http } = useAuth();
    const user = useUser();

    const fetchWeather = () => {
        http.get(apiRoutes.baseUrl + 'weatherforecast')
    }

    return (
        <div className="flex">
            <div className="position-fixed">


            </div>

            <div className="w-full max-w-xs">
                <h2>Dashboard</h2>
                <p>
                    {user.name}
                </p>
                <button className="bg-blue-400 rounded p-2" onClick={fetchWeather}>
                    Weather
                </button>
                <button className="bg-blue-400 rounded p-2" onClick={logout}>
                    Logout
                </button>
            </div>
        </div>
    )
}
export default Application;