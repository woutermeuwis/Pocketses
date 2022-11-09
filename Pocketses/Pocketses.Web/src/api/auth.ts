import axios from "axios";
import { apiRoutes } from "./apiRoutes";

const api = axios.create({ baseURL: apiRoutes.baseUrl });

const authenticate = (token: string) => {
    return api.post('authenticate',{token});
}