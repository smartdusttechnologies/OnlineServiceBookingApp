import {axiosInstance} from "../utils/axiosInstance";

 export function emailExist(email) {
  return axiosInstance.get(`api/security/emailexist`,{
    params: {
      email: email,
    },
    headers: {
      'Content-Type': 'application/json',
    },
  });
 }