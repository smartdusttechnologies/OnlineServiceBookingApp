import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CircularProgress from "@mui/material/CircularProgress";
import IconButton from "@mui/material/IconButton";
import InputAdornment from "@mui/material/InputAdornment";
import { useTheme } from "@mui/material/styles";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import React, { useCallback, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import FlashMessage from "../../components/FlashMessage";
import { isValidEmailAddress } from "../../utils/customFunction";
import { LoginWrapper } from "../Wrapper";
import useStyles from "./styles";
import RegistrationIcon from "../../assets/images/emailLock.png";
import { Avatar } from "@mui/material";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";
import { register } from "../../services/userService";

function Registration() {
  const theme = useTheme();
  const classes = useStyles();
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const formRef = useRef(null);
  const { state } = useLocation();
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [passError, setPassError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [fNameError, setFNameError] = useState("");
  const [lNameError, setLNameError] = useState("");
  const [phone, setPhone] = useState("");
  const [phoneError, setPhoneError] = useState(null);

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const clearErrors = () => {
    setEmailError("");
    setFNameError("");
    setFNameError("");
    setPassError("");
    setError("");
  };
  const handlePhoneChange = (value, data, event, formattedValue) => {
   
    const code = data?.dialCode || "";
    setPhone(value.replace(`+${code}`, ""));
  };

  const handleAction = () => {
    clearErrors();
    let validate = true;
    const emailValue = formRef.current["email"].value;
    const firstNameValue = formRef.current["firstName"].value;
    const lastNameValue = formRef.current["lastName"].value;
    const userPass = formRef.current["userPass"].value;
    const passRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;

    if (!isValidEmailAddress(emailValue)) {
      setEmailError("Invalid Email");
      validate = false;
    }
    if (!firstNameValue.trim()) {
      setFNameError("Invalid First Name");
      validate = false;
    }
    if (!lastNameValue.trim()) {
      setLNameError("Invalid Last Name");
      validate = false;
    }
    if (!userPass) {
      setPassError("Invalid Password");
      validate = false;
    }
    if (!phone) {
      setPhoneError("Invalid phone");
      validate = false;
    }
    if (!passRegex.test(userPass)) {
      setPassError(
        "Invalid Password. Password must contain 1 capital letter, 1 small letter, 1 number"
      );
      validate = false;
    }

    if (validate) {
      setLoading(true);

      // Placeholder for user registration function
      registerUser({
          UserName: firstNameValue,           // User Name
          FirstName: firstNameValue,          // First Name
          LastName: lastNameValue,           // Last Name
          Email: emailValue.toLowerCase().trim(),              // Email Address
          Mobile: `${phone}`,             // Mobile
          Country: "",            // Country
          ISDCode: "",            // ISDCode
          MobileValidationStatus: 0, // Mobile Validation Status
          OrgId: 0,               // OrgId
          Password: userPass,           // Password
          NewPassword: userPass         // New Password
        
      });
    } else {
      setError("Something is missing");
    }
  };

  const toggleSnackbar = useCallback(() => {
    setError("");
  }, []);

   const registerUser = async (userData) => {
    // Replace this with your actual registration logic
    // For example, make an API call to register the user
    console.log("User registration data:", userData);
    await register(userData).then((response)=>{
      console.log(response);
    });
    // After successful registration, navigate to the next step
    // navigate("/verify-email", {
    //   replace: true,
    //   state: {
    //     email: userData.email,
    //     password: userData.password,
    //     name: `${userData.firstName} ${userData.lastName}`,
    //     phone: userData.phone,
    //     picture: "",
    //   },
    // });
  };

  return (
    <LoginWrapper>
      <FlashMessage
        open={Boolean(error)}
        severity={"error"}
        alertMessage={error}
        handleClose={toggleSnackbar}
      />

      <Box display="flex">
        <Avatar
          m="auto"
          alt="email"
          src={RegistrationIcon}
          sx={{
            width: 100,
            height: 100,
            display: "flex",
            alignSelf: "center",
          }}
        />
      </Box>

      <Box mt={theme.spacing(1)} />
      <Typography variant="h5" className={classes.font700}>
        Let's get you started!
      </Typography>

      <Box mt={theme.spacing(1)} />
      <Typography
        variant="caption"
        className={`${classes.caption} ${classes.fontGrey}`}
      >
        First, create your account
      </Typography>

      <Box mt={theme.spacing(3)} />
      <form ref={formRef}>
        <TextField
          name={"email"}
          error={Boolean(emailError)}
          helperText={emailError}
          variant="outlined"
          label="Email"
          fullWidth
          InputLabelProps={{
            style: {
              color: theme.palette.grey[500],
            },
          }}
        />
        
        <Box className={classes.rowField}>
          <TextField
            style={{ width: "45%" }}
            name={"firstName"}
            error={Boolean(fNameError)}
            helperText={fNameError}
            variant="outlined"
            label="First Name"
            fullWidth
            InputLabelProps={{
              style: {
                color: theme.palette.grey[600],
              },
            }}
          />

          <TextField
            style={{ width: "45%" }}
            name={"lastName"}
            error={Boolean(lNameError)}
            helperText={lNameError}
            variant="outlined"
            label="Last Name"
            fullWidth
            InputLabelProps={{
              style: {
                color: theme.palette.grey[600],
              },
            }}
          />
        </Box>

        <TextField
          name={"userPass"}
          InputLabelProps={{
            style: {
              color: theme.palette.grey[600],
            },
          }}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  edge="end"
                  size="large"
                >
                  {showPassword ? (
                    <Visibility color="primary" />
                  ) : (
                    <VisibilityOff color="primary" />
                  )}
                </IconButton>
              </InputAdornment>
            ),
          }}
          error={Boolean(passError)}
          helperText={passError}
          fullWidth
          variant="outlined"
          label="Password"
          type={showPassword ? "text" : "password"}
        />

        <Box className={classes.rowField}>
          <PhoneInput
            placeholder="Enter phone number"
            country={"pk"}
            value={phone}
            onChange={handlePhoneChange}
            containerStyle={{
              textAlign: "center",
              marginRight: theme.spacing(2),
              margin: "auto",
            }}
            inputStyle={{
              paddingLeft: 10,
              width: "85%",
              float: "right",
              borderColor: theme.palette.grey[200],
              height: 50,
            }}
            dropdownStyle={{
              position: "absolute",
              top: "-60px",
              left: "60px",
            }}
          />
        </Box>

        <Typography variant="caption" style={{ color: "red" }}>
          {phoneError}
        </Typography>

        <Box mt={theme.spacing(4)} />
        <Button
          variant="contained"
          fullWidth
          type="email"
          disableElevation
          disabled={loading}
          onClick={(e) => {
            e.preventDefault();
            handleAction();
          }}
        >
          {loading ? (
            <CircularProgress color="primary" />
          ) : (
            <Typography
              variant="caption"
              className={`${classes.caption} ${classes.font700}`}
            >
              CONTINUE
            </Typography>
          )}
        </Button>
      </form>
    </LoginWrapper>
  );
}

export default Registration;
