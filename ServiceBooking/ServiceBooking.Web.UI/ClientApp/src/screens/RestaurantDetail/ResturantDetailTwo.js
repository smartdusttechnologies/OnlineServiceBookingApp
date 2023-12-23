import React, { useEffect, useState } from 'react';
import { getRestaurant } from '../../services/resturantServices';

const RestaurantDetailTwo = () => {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const id = 123; // Replace with your actual ID
        const slug = 'example-restaurant'; // Replace with your actual slug
        const response = await getRestaurant(id, slug);
        setData(response.data);
        setLoading(false);
      } catch (error) {
        setError(error);
        setLoading(false);
      }
    };

    fetchData();
  }, []); 

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>Error: {error.message}</p>;
  }

  if (!data) {
    return <p>No data found</p>;
  }

  return (
    <div>
      <h2>Data loaded</h2>
      {/* Display other restaurant details */}
    </div>
  );
};

export default RestaurantDetailTwo;
