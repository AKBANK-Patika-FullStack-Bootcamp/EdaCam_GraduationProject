import axios from "./api";

export const getPayments = () => {
  return axios().get(`/Payments`);
};
export const getPaymentDetails = () => {
  return axios().get(`/Payments/detail`);
};
export const getUserPayment = (userId) => {
  return axios().get(`/Payments/users/${userId}`);
};
export const addPayment = (newPayment) => {
  return axios().post(`/Payments`, newPayment);
};
export const updatePayment = (newPayment) => {
  return axios().put(`/Payments`, newPayment);
};
export const deletePayment = (PaymentId) => {
  return axios().delete(`/Payments/${PaymentId}`);
};
