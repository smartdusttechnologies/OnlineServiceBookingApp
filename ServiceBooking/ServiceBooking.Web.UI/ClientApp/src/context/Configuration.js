import {getConfiguration} from '../services/systemServices';
import React from 'react'
import { useState,useEffect } from 'react';

const ConfigurationContext = React.createContext({})

export const ConfigurationProvider = props => {
  const [data,setData] = useState({});
const [loading,setLoading] = useState(true);
  useEffect(()=>{
    const fetchData = async () => {
      try {
        const response = await getConfiguration();
        setData(response.data);
        setLoading(false);
      } catch (error) {
        console.error('Error:', error);
        setLoading(false);
      }
    };
    fetchData();
    
  },[])
  const configuration =
    loading || !data
      ? { currency: '', currencySymbol: '', deliveryRate: 0 }
      : data

  return (
    <ConfigurationContext.Provider value={configuration}>
      {props.children}
    </ConfigurationContext.Provider>
  )
}
export const ConfigurationConsumer = ConfigurationContext.Consumer
export default ConfigurationContext