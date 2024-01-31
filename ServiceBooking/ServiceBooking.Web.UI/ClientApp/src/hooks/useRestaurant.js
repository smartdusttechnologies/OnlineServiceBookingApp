// import { gql, useQuery } from "@apollo/client";
// import { restaurant } from "../apollo/server";
// import datax from './data.json';


// export default function useRestaurant(id, slug) {
//   const   refetch = 7; const networkStatus = 7; const loading = false; const error = undefined; 
//   console.log("dum");
//   console.log(data);

// const data = datax;
// console.log("refetch");
// console.log(refetch);
// console.log("networkStatus");
// console.log(networkStatus);
// console.log("loading");
// console.log(loading);
// console.log(error);
//   return { data, refetch, networkStatus, loading, error };
// }


import { gql, useQuery } from "@apollo/client";
import { restaurant } from "../apollo/server";

import { getRestaurant } from "../services/resturantServices";
import { useState } from "react";


export default function useRestaurant(id, slug) {
    const [data,setData] = useState();
  const   refetch = 7; const networkStatus = 7; const loading = false; const error = undefined; 
  console.log("dum");
  console.log(data);
     getRestaurant(id, slug).then((response)=>{
        setData(response.data);
        console.log(response.data);
     });
        
console.log("refetch");
console.log(refetch);
console.log("networkStatus");
console.log(networkStatus);
console.log("loading");
console.log(loading);
console.log(error);
  return { data, refetch, networkStatus, loading, error };
}
