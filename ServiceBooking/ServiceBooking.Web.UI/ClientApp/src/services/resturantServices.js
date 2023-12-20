import datax from './data.json';
import resdata from './restData.json';
import axios from "axios"
import {axiosInstance} from "../utils/axiosInstance";
import { execute } from 'graphql';
export function getRestaurent(id, slug) {
    axiosInstance.get("/resturant/get").then((response) => {
        //setPost(response.data);
        return response.data;
      });
    //const refetch = 7;
    //const networkStatus = 7;
    //const loading = false;
    //const error = undefined;

    //const data = datax;
    //return { data, refetch, networkStatus, loading, error };
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