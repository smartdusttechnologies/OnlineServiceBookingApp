import { Avatar } from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CircularProgress from "@mui/material/CircularProgress";
import { useTheme } from "@mui/material/styles";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import React, { useCallback, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import EmailImage from "../../assets/images/email.png";
import FlashMessage from "../../components/FlashMessage";
import { LoginWrapper } from "../Wrapper";
import useStyles from "./styles";
import { emailExist } from "../../services/userService";
function isValidEmailAddress(address) {
  return /^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[A-Za-z]+$/.test(address);
}


function NewLogin() {
  const theme = useTheme();
  const classes = useStyles();
  const [error, setError] = useState("");
  const formRef = useRef(null);
  const navigate = useNavigate();
  const location = useLocation();
  const [loading,setLoading] = useState(false);
  // const [EmailEixst, { loading }] = useMutation(EMAIL, {
  //   onCompleted,
  //   onError,
  // });

  async function emailVerify(email) {
    var response = await emailExist(email);
    if (!response.data?.IsSuccessful) {
      navigate("/login-email", {
        replace: true,
        state: {
          email: formRef.current["email"].value,
          from: location.state?.from,
        },
      });
    } else {
      navigate("/registration", {
        replace: true,
        state: {
          email: formRef.current["email"].value,
          from: location.state?.from,
        },
      });
    }
  }
  function onError({ error }) {
    setError("Something went wrong");
  }

  const handleAction = () => {
    const emailValue = formRef.current["email"].value;
    if (isValidEmailAddress(emailValue)) {
      setError("");
      setLoading(true);
      emailVerify(emailValue);
    } else {
      setError("Invalid Email");
    }
  };

  const toggleSnackbar = useCallback(() => {
    setError("");
  }, []);

  return (
    <LoginWrapper>
      <FlashMessage
        open={Boolean(error)}
        severity={"error"}
        alertMessage={error}
        handleClose={toggleSnackbar}
      />
      <Box display="flex">
        <Box m="auto">
          <Avatar
            m="auto"
            alt="email"
            src={EmailImage}
            sx={{
              width: 100,
              height: 100,
              display: "flex",
              alignSelf: "center",
            }}
          />
        </Box>
      </Box>
      <Box mt={theme.spacing(1)} />
      <Typography variant="h5" className={classes.font700}>
        What's your email?
      </Typography>
      <Box mt={theme.spacing(1)} />
      <Typography
        variant="caption"
        className={`${classes.caption} ${classes.fontGrey}`}
      >
        We'll check if you have an account
      </Typography>
      <Box mt={theme.spacing(4)} />
      <form ref={formRef}>
        <TextField
          name={"email"}
          error={Boolean(error)}
          helperText={error}
          variant="outlined"
          label="Email"
          fullWidth
          InputLabelProps={{
            style: {
              color: theme.palette.grey[600],
            },
          }}
        />
        <Box mt={theme.spacing(8)} />
        <Button
          variant="contained"
          fullWidth
          type="email"
          disableElevation
          disabled={loading}
          className={classes.btnBase}
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

export default NewLogin;
