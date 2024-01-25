import { useCallback, useContext, useState } from "react";
import UserContext from "../context/User";
import Analytics from "../utils/analytics";
import { login } from "../services/userService";

function useLogin() {
  const { setTokenAsync } = useContext(UserContext);
  const [loading, setLoading] = useState(false);
  const [loginError, setLoginError] = useState(false);
  const [isLoggedIn, setLoggedIn] = useState(false);
  const [loginButton, setLoginButton] = useState(null);

  const toggleSnackbar = useCallback(() => {
    setLoginError("");
    setLoggedIn(false);
  }, []);

  async function onLoginSuccess(userData) {
    try {
      if (userData != null) {
        await Analytics.identify(
          {
            userId: 0,
            name: "raj gupta",
            email: "rajgupta@gmail.com",
          },
          0
        );
        await Analytics.track(Analytics.events.USER_CREATED_ACCOUNT, {
          userId: 0,
            name: "raj gupta",
            email: "rajgupta@gmail.com",
        });
      } else {
        await Analytics.identify(
          {
            userId: 0,
            name: "raj gupta",
            email: "rajgupta@gmail.com",
          },
          0
        );
        await Analytics.track(Analytics.events.USER_LOGGED_IN, {
          userId: 0,
            name: "raj gupta",
            email: "rajgupta@gmail.com",
        });
      }
      setLoggedIn(true);
      await setTokenAsync(userData.requestedObject?.accessToken);
    } catch (e) {
      setLoginError("Something went wrong");
      console.log("Error While saving token:", e);
    } finally {
      setLoading(false);
    }
  }

  function onLoginError(errors) {
    setLoading(false);
    setLoginError(errors.message || "Invalid credentials!");
  }

  const authenticationFailure = useCallback((response) => {
    console.log("Authentication Failed: ", response);
    setLoading(false);
    setLoginButton(null);
    setLoginError("Something went wrong");
  }, []);

  const loginUser = useCallback(
    async (user) => {
      try {
        setLoading(true);
        await login(user).then((userData)=>{
          console.log(userData);
          onLoginSuccess(userData.data);
        }).catch((error)=>{
          console.log(error);
        });
        
      } catch (error) {
        onLoginError(error);
      }
    },
    [setLoading, onLoginSuccess, onLoginError]
  );

  const googleSuccess = useCallback(
    (response) => {
      const user = {
        phone: "",
        email: response.profileObj.email,
        password: "",
        name: response.profileObj.name,
        picture: response.profileObj.imageUrl,
        type: "google",
      };
      loginUser(user);
    },
    [loginUser]
  );

  return {
    loading,
    setLoading,
    loginButton,
    setLoginButton,
    loginUser,
    googleSuccess,
    authenticationFailure,
    setLoginError,
    loginError,
    isLoggedIn,
    toggleSnackbar,
  };
}

export default useLogin;
