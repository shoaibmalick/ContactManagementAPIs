import axios from "axios";

const instance = axios.create({
  baseURL: "http://localhost:5037/api", // Adjust if your API runs on another port
  headers: {
    "Content-Type": "application/json",
  },
});

export default instance;
