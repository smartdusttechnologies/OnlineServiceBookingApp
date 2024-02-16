import {axiosInstance} from "../utils/axiosInstance";
export const verifyPhoneServices = 
function sendOtp(num){
    return axiosInstance.request({
        method:"GET",
        url:"/user"
    })
}
export function sendMobileOtp(phone,otp){
    return axiosInstance.post('api/security/forgotPassword',{phone:phone,otp:otp});
   }