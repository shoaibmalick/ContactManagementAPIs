// src/theme.ts
import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#0D47A1", // dark blue
    },
    secondary: {
      main: "#1565C0", // lighter blue
    },
    background: {
      default: "#f5f7fa", // light background
    },
    text: {
      primary: "#0D1B2A",
    },
  },
  typography: {
    fontFamily: "Roboto, sans-serif",
    h4: {
      fontWeight: 700,
    },
    h6: {
      fontWeight: 500,
    },
  },
});

export default theme;
