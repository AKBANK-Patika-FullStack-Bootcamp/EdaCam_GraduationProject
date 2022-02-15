import decode from "jwt-decode";
import { login } from "../store/userServices";

export default class Auth {
  constructor() {
    this.login = this.login.bind(this);
  }

  login(email, password) {
    const payload = { email: email, password: password };
    return login(payload).then((response) => {
      if (response.status === 200) {
        const decodeJwt = decode(response.data.data.token);
        this.setAuthority(decodeJwt.role);
        this.setToken(response.data.data.token);
        this.setCurrentUser(decodeJwt.id);
        return Promise.resolve(response);
      }
    });
  }

  loggedIn() {
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token);
  }
  setToken(idToken) {
    localStorage.setItem("id_token", idToken);
  }
  getToken() {
    return localStorage.getItem("id_token");
  }

  setAuthority(authority) {
    localStorage.setItem("authority", authority);
  }

  getAuthority() {
    return localStorage.getItem("authority");
  }

  setCurrentUser(userId) {
    localStorage.setItem("id", userId);
  }
  getCurrentUser() {
    return localStorage.getItem("id");
  }

  logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("authority");
  }

  isTokenExpired(token) {
    try {
      const decoded = decode(token);
      if (decoded.exp < Date.now() / 1000) {
        return true;
      } else return false;
    } catch (err) {
      return true;
    }
  }
}
