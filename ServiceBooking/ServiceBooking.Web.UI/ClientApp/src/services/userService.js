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
 export function register(user){
  return axiosInstance.post('api/security/singup',user);
 }

