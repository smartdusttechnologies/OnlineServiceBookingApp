import axios from "axios"
import { BASE_URL }  from "./app_uri";
export const axiosInstance = axios.create({
    baseURL: "https://localhost:44481",
    headers:{
        "content-type": "application/json"
        //,'Authorization': `${auth.accessToken}`
    }
});