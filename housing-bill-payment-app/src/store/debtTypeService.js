import axios from "./api";

export const getDebtTypes = () => {
  return axios().get(`/DebtTypes`);
};
