/* eslint-disable react-hooks/exhaustive-deps */
import {
  Box,
  CircularProgress,
  Container,
  Typography,
  useTheme,
} from "@mui/material";
import clsx from "clsx";
import React, { useContext, useEffect, useState } from "react";
import DeliveryIcon from "../../../assets/icons/DeliveryIcon";
import UserContext from "../../../context/User";
import { getRestaurant } from "../../../services/resturantServices";
import CartItem from "./CartItem";
import PricingView from "./PricingView";
import useStyles from "./styles";
import { responsePathAsArray } from "graphql";

function CartView(props) {
  const classes = useStyles();
  const theme = useTheme();
  const [loadingData, setLoadingData] = useState(true);
  const [data, setData] = useState({});
  const {
    clearCart,
    restaurant: cartRestaurant,
    cart,
    cartCount,
    addQuantity,
    removeQuantity,
    updateCart,
  } = useContext(UserContext);
 

  const restaurantData = data?.restaurant ?? null;
  useEffect(()=>{
    const fetchData = async () => {
    try {
      const response = await getRestaurant(2, cartRestaurant);
      console.log('Data:', response.data);
      setData(response.data);
      setLoadingData(false);
    } catch (error) {
      console.error('Error:', error);
      setLoadingData(false);
    }
  };
  fetchData();
  })
  useEffect(() => {
    if (restaurantData) didFocus();
  }, [restaurantData, cartCount]);

  const didFocus = async () => {
    const foods = restaurantData.categories.map((c) => c.foods.flat()).flat();
    const { addons, options } = restaurantData;

    try {
      if (cart && cartCount) {
        const transformCart = cart.map((cartItem) => {
          const foodItem = foods.find((food) => food._id === cartItem._id);
          if (!foodItem) return null;
          const variationItem = foodItem.variations.find(
            (variation) => variation._id === cartItem.variation._id
          );
          if (!variationItem) return null;
          const foodItemTitle = `${foodItem.title}${
            variationItem.title ? `(${variationItem.title})` : ""
          }`;
          let foodItemPrice = variationItem.price;
          let optionTitles=[]
          if (cartItem.addons) {
            cartItem.addons.forEach((addon) => {
              const cartAddon = addons.find((add) => add._id === addon._id);
              if (!cartAddon) return null;
              addon.options.forEach((option) => {
                const cartOption = options.find(
                  (opt) => opt._id === option._id
                );
                if (!cartOption) return null;
                foodItemPrice += cartOption.price;
                optionTitles.push(cartOption.title)
              });
            });
          }
          return {
            ...cartItem,
            title: foodItemTitle,
            foodTitle: foodItem.title,
            variationTitle: variationItem.title,
            optionTitles,
            price: foodItemPrice.toFixed(2),
          };
        });
        const updatedItems = transformCart.filter((item) => item);
        if (updatedItems.length === 0) await clearCart();
        await updateCart(updatedItems);
        setLoadingData((prev) => {
          if (prev) return false;
          else return prev;
        });
        if (transformCart.length !== updatedItems.length) {
          props.showMessage({
            type: "warning",
            message: "One or more item is not available",
          });
        }
      }
    } catch (e) {
      props.showMessage({
        type: "error",
        message: e.message,
      });
    } finally {
      setLoadingData((prev) => {
        if (prev) return false;
        else return prev;
      });
    }
  };

  if (loadingData) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <>
      <Box
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          marginBottom: theme.spacing(2),
        }}
      >
        <DeliveryIcon />
        <Typography
          style={{
            marginLeft: theme.spacing(1),
            ...theme.typography.body1,
            color: theme.palette.text.disabled,
            fontSize: "0.875rem",
          }}
        >
          {`${restaurantData?.deliveryTime ?? "..."}  min`}
        </Typography>
      </Box>
      <Box
        style={{
          display: "flex",
          justifyContent: "center",
          background: theme.palette.common.white,
        }}
      >
        <Typography
          variant="h6"
          color="textSecondary"
          className={clsx(classes.mediumFont, classes.textBold)}
        >
          {`Your order from ${restaurantData?.name ?? "..."}`}
        </Typography>
      </Box>
      <Container
        style={{
          maxHeight: "30vh",
          overflow: "scroll",
          paddingBottom: theme.spacing(2),
          background: theme.palette.common.white,
        }}
      >
        {cart?.map((foodItem) => (
          <CartItem
            key={`ITEM_${foodItem.key}`}
            quantity={foodItem.quantity}
            dealName={foodItem.title}
            foodTitle={foodItem.foodTitle}
            variationTitle={foodItem.variationTitle}
            optionTitles={foodItem.optionTitles}
            dealPrice={(parseFloat(foodItem.price) * foodItem.quantity).toFixed(
              2
            )}
            addQuantity={() => {
              addQuantity(foodItem.key);
            }}
            removeQuantity={() => {
              removeQuantity(foodItem.key);
            }}
          />
        ))}
      </Container>
      <PricingView
        restaurantData={restaurantData}
        deliveryCharges={restaurantData.deliveryCharges}
      />
    </>
  );
}
export default React.memo(CartView);
