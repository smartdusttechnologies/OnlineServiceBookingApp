import {axiosInstance} from "../utils/axiosInstance";

export function orderService(req) {
  return axiosInstance.post(`api/order/newOrder`,req);
}
export function getOrders() {
  return axiosInstance.get(`api/order/getOrders`);
}
export function applyCoupon(coupon) {
   return axiosInstance.get(`api/order/applyCoupon`,coupon);
}