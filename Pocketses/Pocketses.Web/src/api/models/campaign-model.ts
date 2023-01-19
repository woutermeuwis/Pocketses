import {Character} from "./character-model";

export interface Campaign {
    name: string,
    id: string
}

export interface CampaignDetail extends Campaign {
    characters: Character[]
}