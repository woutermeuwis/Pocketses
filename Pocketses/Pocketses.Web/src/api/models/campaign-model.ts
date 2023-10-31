import {Character} from "./character-model";

export interface Campaign {
    name: string,
    id: string
}

export interface CampaignInfo extends Campaign {
    characters: Character[]
}
