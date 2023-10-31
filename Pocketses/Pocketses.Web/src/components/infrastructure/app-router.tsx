import {Routes, Route, Navigate} from "react-router-dom"
import Campaigns from "../../Pages/campaigns/campaigns";
import Characters from "../../Pages/characters/characters";
import Dashboard from "../../Pages/main/dashboard";
import JoinCampaign from "../../Pages/campaigns/join-campaign";
import CampaignDetail from "../../Pages/campaigns/campaign-detail";
import CharacterDetail from "../../Pages/characters/character-detail";

function AppRouter() {
    return (
        <Routes>
            <Route path={"campaigns"} element={<Campaigns/>}/>
            <Route path={"campaigns/:campaignId/detail"} element={<CampaignDetail/>}/>
            <Route path={"campaigns/:campaignId/join"} element={<JoinCampaign/>}/>

            <Route path={"characters"} element={<Characters/>}/>
            <Route path={"characters/:characterId/detail"} element={<CharacterDetail/>}/>

            <Route path={"dashboard"} element={<Dashboard/>}/>

            <Route path="*" element={<Navigate to="/dashboard"/>}/>
        </Routes>
    );
}

export default AppRouter;
