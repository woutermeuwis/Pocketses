import { Routes, Route, Navigate } from "react-router-dom"
import Campaigns from "../main/campaigns";
import Characters from "../main/characters";
import Dashboard from "../main/dashboard";

function AppRouter() {
    return (
        <Routes>
            <Route path="campaigns" element={<Campaigns />} />
            <Route path="characters" element={<Characters />} />
            <Route path="dashboard" element={<Dashboard />} />
            <Route path="*" element={<Navigate to="/dashboard" />} />
        </Routes>
    );
}
export default AppRouter;