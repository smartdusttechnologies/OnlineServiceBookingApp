import datax from './data.json';
import resdata from './restData.json';
import axios from "axios"
import {axiosInstance} from "../utils/axiosInstance";
import { execute } from 'graphql';

 export function getRestaurant(id, slug) {
  return axios.get(`/api/resturant/GetResturantDetail/${slug}`);
}
export function getRestaurents() {
    return axios.get("/api/resturant/GetResturant");
}