import axios from "./api";

export const getUsers = () => {
  return axios().get(`/Users`);
};
export const getUserById = (UserId) => {
  return axios().get(`/Users/${UserId}`);
};
export const addUser = (newUser) => {
  return axios().post(`/Users`, newUser);
};
export const updateUser = (newUser) => {
  return axios().put(`/Users`, newUser);
};
export const login = (userInfo) => {
  return axios().post(
    `/Users/login`,
    {},
    {
      headers: {
        UserMail: userInfo.email,
        Password: userInfo.password,
      },
    }
  );
};
export const deleteUser = (UserId) => {
  return axios().delete(`/Users/${UserId}`);
};
