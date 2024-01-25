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
 export function getProfile(id) {
  return axiosInstance.get(`api/security/profile`,{
    params: {
      userId: id,
    },
    
  });
 }
 export function forgotPassword(email) {
  return axiosInstance.get(`api/security/forgotpassword`,{
    params: {
      email: email,
    },
    
  });
 }
 export function verifyOtp(otp) {
  return axiosInstance.get(`api/security/verifyotp`,{
    params: {
      otp: otp,
    },
    
  });
 }
 export function register(user){
  return axiosInstance.post('api/security/SignUp',user);
 }
 export function login(user){
  return axiosInstance.post('api/security/Login',user);
 }
