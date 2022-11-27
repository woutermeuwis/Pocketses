import { Routes, Route, Navigate } from "react-router-dom"
import Campaigns from "../../Pages/campaigns/campaigns";
import CreateCampaign from "../../Pages/campaigns/create-campaign";
import Characters from "../../Pages/main/characters";
import Dashboard from "../../Pages/main/dashboard";

function AppRouter() {
    return (
        <Routes>
            <Route path="campaigns" element={<Campaigns />} />
            <Route path="create-campaign" element={<CreateCampaign />} />
            <Route path="characters" element={<Characters />} />
            <Route path="dashboard" element={<Dashboard />} />
            <Route path="*" element={<Navigate to="/dashboard" />} />
        </Routes>
    );
}
export default AppRouter;