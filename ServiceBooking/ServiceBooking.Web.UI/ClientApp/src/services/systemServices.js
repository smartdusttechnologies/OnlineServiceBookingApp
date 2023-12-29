import axios from "axios"
import {axiosInstance} from "../utils/axiosInstance";

 export function getConfiguration() {
  return axiosInstance.get(`api/system/getConfiguration`);
 }