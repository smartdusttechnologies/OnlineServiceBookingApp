import axios from "axios"
import {axiosInstance} from "../utils/axiosInstance";

 export function getConfiguration() {
  return axios.get(`/api/system/getConfiguration`);
 }