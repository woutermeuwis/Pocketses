import {Routes, Route} from "react-router-dom"
import Application from "../main/application";

function AppRouter(){
    return (
        <Routes>
            <Route path="*" element={<Application />}/>
        </Routes>
    );
}
export default AppRouter;