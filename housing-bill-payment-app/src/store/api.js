import axios from "axios";

const instance = () => {
  return axios.create({
    baseURL: `https://localhost:7283/api`,
    headers: {
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
    },
  });
};

export default instance;
