/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable no-unused-vars */
import { gql, useMutation } from "@apollo/client";
import {
  Box,
  CircularProgress,
  Container,
  Grid,
  Typography,
} from "@mui/material";
import React, { useCallback, useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { myOrders, placeOrder } from "../../apollo/server";
import CodIcon from "../../assets/icons/CodIcon";
import {
  CartItemCard,
  DeliveryCard,
  PaymentCard,
  PersonalCard,
  OrderOption,
} from "../../components/Checkout";
import FlashMessage from "../../components/FlashMessage";
import Footer from "../../components/Footer/Footer";
import { Header } from "../../components/Header";
import { RestaurantClose } from "../../components/Modals";
import ConfigurationContext from "../../context/Configuration";
import { LocationContext, useLocationContext } from "../../context/Location";
import UserContext from "../../context/User";
import { useRestaurant } from "../../hooks";
import { DAYS } from "../../utils/constantValues";
import { paypalCurrencies, stripeCurrencies } from "../../utils/currencies";
import { calculateDistance } from "../../utils/customFunction";
import useStyle from "./styles";
import { SERVER_URL } from "../../config/constants";
import Analytics from "../../utils/analytics";
import { getRestaurant } from "../../services/resturantServices";
import { getProfile } from "../../services/userService";
import { orderService } from "../../services/orderServices";

const PLACEORDER = gql`
  ${placeOrder}
`;
const ORDERS = gql`
  ${myOrders}
`;

const PAYMENT = {
  id: 2,
  payment: "COD",
  label: "Cash",
  icon: <CodIcon />,
};

function Checkout() {
  const classes = useStyle();
  const navigate = useNavigate();
  const [isClose, setIsClose] = useState(false);
  const [mainError, setMainError] = useState({});
  const [loadingData, setLoadingData] = useState(false);
  const configuration = useContext(ConfigurationContext);
  const {
    clearCart,
    restaurant: cartRestaurant,
    cart,
    cartCount,
  } = useContext(UserContext);
  const { location, setLocation } = useLocationContext();

  const [minimumOrder, setMinimumOrder] = useState("");
  const [selectedTip, setSelectedTip] = useState();
  const [paymentMethod, setPaymentMethod] = useState(PAYMENT);
  const [taxValue, setTaxValue] = useState();
  const [selectedAddress, setSelectedAddress] = useState();
  const [coupon, setCoupon] = useState({});
  const [selectedDate, handleDateChange] = useState(new Date());
  const [isPickUp, setIsPickUp] = useState(false);
  const [deliveryCharges, setDeliveryCharges] = useState(0);
  const [data,setData] = useState(null);
  const [loading,setLoading] = useState(true);
  const error = false;
  const [profile,setProfile] = useState();

  const [mutateOrder, { loading: loadingOrderMutation }] = useMutation(
    PLACEORDER,
    {
      onCompleted,
      onError,
      update,
    }
  );
 
  useEffect(() => {
    const fetchPro = () => {
      return getProfile(206)
        .then((res) => {
          setProfile(res.data);
          console.log(res, "raj data ready");
        })
        .catch((error) => {
          console.error("Error fetching profile:", error);
        });
    };
    const fetchData = () => {
      return getRestaurant(3, 'example-item').then((response)=>{
      
      setData(response.data);
      setLoading(false);
      });
    };
    fetchPro();
    fetchData();
  }, []);
   
  
  useEffect(() => {
    
   
    // getRestaurant(3, 'example-item').then((response)=>{
    //     setData(response.data);
    //     console.log(response.data);
    //  });
    if (!location) {
      let localStorageLocation = localStorage.getItem("location");
      localStorageLocation = JSON.parse(localStorageLocation);
      if (localStorageLocation) {
        setLocation(localStorageLocation);
      }
    }
  }, []);

  useEffect(() => {
    (async () => {
      if (data && !!data.restaurant) {
        const latOrigin = Number(data.restaurant.location.coordinates[1]);
        const lonOrigin = Number(data.restaurant.location.coordinates[0]);
        const latDest = Number(location.latitude);
        const longDest = Number(location.longitude);
        const distance = await calculateDistance(
          latOrigin,
          lonOrigin,
          latDest,
          longDest
        );
        const amount = Math.ceil(distance) * configuration.deliveryRate;
        setDeliveryCharges(amount > 0 ? amount : configuration.deliveryRate);
      }
    })();
  }, [data, location]);

  const isOpen = () => {
    const date = new Date();
    const day = date.getDay();
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const todaysTimings = data.restaurant.openingTimes.find(
      (o) => o.day === DAYS[day]
    );
    const times = todaysTimings.times.filter(
      (t) =>
        hours >= Number(t.startTime[0]) &&
        minutes >= Number(t.startTime[1]) &&
        hours <= Number(t.endTime[0]) &&
        minutes <= Number(t.endTime[1])
    );

    return times.length > 0;
  };

  useEffect(() => {
    if (cart && cartCount > 0) {
      if (
        data &&
        data.restaurant &&
        (!data.restaurant.isAvailable || !isOpen())
      ) {
        setIsClose((prev) => {
          if (!prev) return true;
          else return prev;
        });
      }
    }
  }, [data]);

  const setDeliveryAddress = (item) => {
    setSelectedAddress(item);
    setLocation({
      _id: item._id,
      label: item.label,
      latitude: Number(item.location.coordinates[1]),
      longitude: Number(item.location.coordinates[0]),
      deliveryAddress: item.deliveryAddress,
      details: item.details,
    });
  };

  const toggleCloseModal = useCallback(() => {
    setIsClose((prev) => !prev);
  }, []);

  const restaurantData = data?.restaurant ?? null;

  const showMessage = useCallback((messageObj) => {
    setMainError(messageObj);
  }, []);

  const toggleSnackbar = useCallback(() => {
    setMainError({});
  }, []);

  if (loading || loadingData) {
    return (
      <Grid container>
        <Header />
        <Box className={classes.spinnerContainer}>
          <CircularProgress color="primary" size={48} />
        </Box>
      </Grid>
    );
  }

  if (error) {
    return (
      <Grid container>
        <Header />
        <Box className={classes.spinnerContainer}>
          <Typography>Unable to fetch data</Typography>
        </Box>
      </Grid>
    );
  }

  function update(cache, { data: { placeOrder } }) {
    console.log("update");
    if (placeOrder && placeOrder.paymentMethod === "COD") {
      const data = cache.readQuery({ query: ORDERS });
      if (data) {
        cache.writeQuery({
          query: ORDERS,
          data: { orders: [placeOrder, ...data.orders] },
        });
      }
    }
  }

  function onError(error) {
    console.log("Check-out Error", error);
    showMessage({
      type: "error",
      message: error.message,
    });
  }

  function calculateTip() {
    if (selectedTip) {
      let total = 0;
      const delivery = isPickUp ? 0 : deliveryCharges;
      total += +calculatePrice(delivery, true);
      total += +taxCalculation();
      const tipPercentage = (
        (total / 100) *
        parseFloat(selectedTip).toFixed(2)
      ).toFixed(2);
      return tipPercentage;
    } else {
      return 0;
    }
  }

  function taxCalculation() {
    const tax = taxValue ?? 0;
    if (tax === 0) {
      return tax.toFixed(2);
    }
    const delivery = isPickUp ? 0 : deliveryCharges;
    const amount = +calculatePrice(delivery, true);
    const taxAmount = ((amount / 100) * tax).toFixed(2);
    return taxAmount;
  }
  async function onCompleted(data) {
    await Analytics.track(Analytics.events.ORDER_PLACED, {
      userId: data.userid,
      orderId: data.orderId,
      restaurantName: data.restaurant.name,
      restaurantAddress: data.restaurant.address,
      orderItems: data.items,
      orderPaymentMethod: data.paymentMethod,
      orderAmount: data.orderAmount,
      orderPaidAmount: data.paidAmount,
      tipping: data.tipping,
      orderStatus: data.orderStatus,
      orderDate: data.orderDate,
    });
    if (paymentMethod.payment === "COD") {
      await clearCart();
      navigate(`/order-detail/${data.id}`, { replace: true });
    } else if (paymentMethod.payment === "PAYPAL") {
      navigate(`/paypal?id=${data.id}`, { replace: true });
    } else if (paymentMethod.payment === "STRIPE") {
      window.location = `${SERVER_URL}stripe/create-checkout-session?id=${data.orderId}&platform=web`;
    }
  }

  function calculatePrice(delivery = 0, withDiscount) {
    let itemTotal = 0;
    cart.forEach((cartItem) => {
      itemTotal += cartItem.price * cartItem.quantity;
    });
    if (withDiscount && coupon && coupon.discount) {
      itemTotal = itemTotal - (coupon.discount / 100) * itemTotal;
    }
    const deliveryAmount = delivery > 0 ? deliveryCharges : 0;
    return (itemTotal + deliveryAmount).toFixed(2);
  }

  function calculateTotal() {
    let total = 0;
    const delivery = isPickUp ? 0 : deliveryCharges;
    total += +calculatePrice(delivery, true);
    total += +taxCalculation();
    total += +calculateTip();
    return parseFloat(total).toFixed(2);
  }

  function transformOrder(cartData) {
    return cartData.map((food) => {
      return {
        food: food._id,
        quantity: food.quantity,
        variation: food.variation._id,
        addons: food.addons
          ? food.addons.map(({ _id, options }) => ({
              _id,
              options: options.map(({ _id }) => _id),
            }))
          : [],
        specialInstructions: food.specialInstructions,
      };
    });
  }

  function checkPaymentMethod(currency) {
    if (paymentMethod.payment === "STRIPE") {
      return stripeCurrencies.find((val) => val.currency === currency);
    }
    if (paymentMethod.payment === "PAYPAL") {
      return paypalCurrencies.find((val) => val.currency === currency);
    }
    return true;
  }

  async function onPayment() {
    if (checkPaymentMethod(configuration.currency)) {
      debugger;
      console.log("payment");
      const items = transformOrder(cart);
      const req = {
          id: 0,
          userid: profile._id,
          restaurant: cartRestaurant,
          orderInput: items,
          paymentMethod: paymentMethod.payment,
          couponCode: coupon ? coupon.title : null,
          tipping: +calculateTip(),
          taxationAmount: +taxCalculation(),
          address: {
            label: location.label,
            deliveryAddress: location.deliveryAddress,
            details: location.details,
            longitude: "" + location.longitude,
            latitude: "" + location.latitude,
          },
          orderDate: selectedDate,
          isPickedUp: isPickUp,
          deliveryCharges: isPickUp ? 0 : deliveryCharges,
        };
        await orderService(req).then((resp)=>{
          if(resp){
            req.id = 1
            onCompleted(req);
          }
        })
      console.log(req,"raj reqst for order");
    } else {
      showMessage({
        type: "warning",
        message: "Payment not supported",
      });
    }
  }

  function validateOrder() {
    if (!data.restaurant.isAvailable || !isOpen()) {
      toggleCloseModal();
      return;
    }
    if (!cart.length) {
      showMessage({
        type: "error",
        message: "Cart is Empty.",
      });
      return false;
    }
    const delivery = isPickUp ? 0 : deliveryCharges;
    if (calculatePrice(delivery, true) < minimumOrder) {
      showMessage({
        type: "warning",
        message: `The minimum amount of (${configuration.currencySymbol} ${minimumOrder}) for your order has not been reached.`,
      });
      return false;
    }
    if (!location._id) {
      showMessage({
        alive: true,
        type: "Warning",
        message: "Select your address.",
      });
      return false;
    }
    if (!paymentMethod) {
      showMessage({
        type: "warning",
        message: "Set payment method before checkout",
      });
      return false;
    }
    if (profile.phone.length < 1) {
      showMessage({
        alive: true,
        type: "Error",
        message: "Phone Number is missing",
      });
      return false;
    }
    if (!profile.phoneIsVerified) {
      showMessage({
        alive: true,
        type: "Error",
        message: "Phone Number is not verified",
      });
      return false;
    }
    return true;
  }

  return (
    <Grid container className={classes.root}>
      <FlashMessage
        open={Boolean(mainError.type)}
        severity={mainError.type}
        alertMessage={mainError.message}
        handleClose={toggleSnackbar}
        alive={mainError.alive || false}
      />
      <Header />
      <Grid
        container
        item
        className={classes.mainContainer}
        justifyContent="center"
      >
        <Container maxWidth="md">
          <Grid container spacing={2}>
            <Grid item xs={12} sm={8}>
              <OrderOption
                selectedDate={selectedDate}
                handleDateChange={handleDateChange}
                setIsPickUp={setIsPickUp}
              />
              {!isPickUp && (
                <DeliveryCard
                  selectedAddress={selectedAddress}
                  setSelectedAddress={setDeliveryAddress}
                />
              )}
              <PersonalCard />
              <PaymentCard
                paymentMethod={paymentMethod}
                setPaymentMethod={setPaymentMethod}
                validateOrder={validateOrder}
                onPayment={onPayment}
                loading={loadingOrderMutation}
              />
            </Grid>
            <Grid item xs={12} sm={4}>
              <CartItemCard
                setSelectedTip={setSelectedTip}
                selectedTip={selectedTip}
                setTaxValue={setTaxValue}
                setCoupon={setCoupon}
                restaurantData={restaurantData}
                setFlashMessage={showMessage}
                calculateTotal={calculateTotal}
                calculatePrice={calculatePrice}
                taxCalculation={taxCalculation}
                calculateTip={calculateTip}
                isPickUp={isPickUp}
                deliveryCharges={deliveryCharges}
              />
            </Grid>
          </Grid>
        </Container>
      </Grid>
      <Footer />
      <RestaurantClose
        closeMenu={false}
        isVisible={isClose}
        toggleModal={toggleCloseModal}
        restaurant={restaurantData.name}
      />
    </Grid>
  );
}

export default Checkout;
