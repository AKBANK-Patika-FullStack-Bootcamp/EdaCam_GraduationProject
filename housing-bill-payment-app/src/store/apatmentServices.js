import axios from "./api";

export const getApartments = () => {
  return axios().get(`/Apartments`);
};
export const getApartmentDetails = () => {
  return axios().get(`/Apartments/detail`);
};
export const addApartment = (newApartment) => {
  return axios().post(`/Apartments`, newApartment);
};
export const updateApartment = (newApartment) => {
  return axios().put(`/Apartments`, newApartment);
};
export const deleteApartment = (apartmentId) => {
  return axios().delete(`/Apartments/${apartmentId}`);
};
