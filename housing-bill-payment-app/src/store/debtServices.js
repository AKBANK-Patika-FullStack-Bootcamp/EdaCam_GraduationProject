import axios from "./api";

export const getDebts = () => {
  return axios().get(`/Debts`);
};
export const getDebtDetails = () => {
  return axios().get(`/Debts/detail`);
};
export const getUserDebt = (userId) => {
  return axios().get(`/Debts/users/${userId}`);
};
export const addDebt = (newDebt) => {
  return axios().post(`/Debts`, newDebt);
};
export const addDebts = (newDebt) => {
  return axios().post(`/Debts/bulk`, newDebt);
};
export const updateDebt = (newDebt) => {
  return axios().put(`/Debts`, newDebt);
};
export const deleteDebt = (debtId) => {
  return axios().delete(`/Debts/${debtId}`);
};
