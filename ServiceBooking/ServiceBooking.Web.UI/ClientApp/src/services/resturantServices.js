import datax from './data.json';
import resdata from './restData.json';
import axios from "axios"
import {axiosInstance} from "../utils/axiosInstance";
import { execute } from 'graphql';

 export async function getRestaurant(id, slug) {
  axios.get(`/api/resturant/GetResturantDetail/${slug}`).then((response)=>{
    const {data} = response;
    return response.data;
  });
}
export function getRestaurents() {
    // axiosInstance.get("/resturant/get").then((response) => {
    //     //setPost(response.data);
    //     const raj = "sfd";
    //     return response.data;
    //   });
    axios.get("/resturant/get").then((response)=>{
      // console.log(response.data);
       return response.data;
     })
}