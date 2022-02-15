import React, { useState } from "react";
import "antd/dist/antd.css";
import "./AppLayout.css";
import { Menu, Layout, Divider } from "antd";
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  UserOutlined,
  CreditCardOutlined,
  HomeOutlined,
  PrinterOutlined,
  LogoutOutlined,
} from "@ant-design/icons";
import ApartmentPage from "../pages/ApartmentPage";
import UserPage from "../pages/UserPage";
import DebtPage from "../pages/DebtPage";
import PaymentPage from "../pages/PaymentPage";
import UserPaymentPage from "../pages/UserPaymentPage";
import UserInfoPage from "../pages/UserInfoPage";
import UserDebtPage from "../pages/UserDebtPage";
import Auth from "../authentication/Authentication";
import Message from "../components/messages/Message";
const { Sider, Content } = Layout;

const auth = new Auth();

const AppLayout = ({ setLogin, authority }) => {
  const [collapsed, setCollapsed] = useState(false);
  const [pageState, setPageState] = useState();
  const [logoClassName, setLogoClassName] = useState("logo-big");

  const siderButtonOnClick = () => {
    setCollapsed(!collapsed);
    if (collapsed === false) {
      setLogoClassName("logo-small");
    } else {
      setLogoClassName("logo-big");
    }
  };
  const changePageState = (value) => {
    setPageState(value);
  };

  const logout = () => {
    auth.logout();
    setLogin(false);
  };

  let menu = "";
  if (authority.toLowerCase() === "admin") {
    menu = (
      <Menu className="menu" theme="dark" mode="inline">
        <Menu.Item
          key="1"
          icon={<UserOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("User")}
        >
          Kullanıcı İşlemleri
        </Menu.Item>
        <Menu.Item
          key="2"
          icon={<HomeOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("Apartment")}
        >
          Daire İşlemleri
        </Menu.Item>
        <Menu.Item
          key="3"
          icon={<PrinterOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("Debt")}
        >
          Fatura İşlemleri
        </Menu.Item>
        <Menu.Item
          key="4"
          icon={<CreditCardOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("Payment")}
        >
          Ödeme İşlemleri
        </Menu.Item>
        <Menu.Item
          key="5"
          icon={<LogoutOutlined style={{ fontSize: "18px" }} />}
          onClick={logout}
        >
          Çıkış
        </Menu.Item>
      </Menu>
    );
  } else if (authority.toLowerCase() === "user") {
    menu = (
      <Menu className="menu" theme="dark" mode="inline">
        <Menu.Item
          key="1"
          icon={<UserOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("UserInfo")}
        >
          Kullanıcı Bilgileri
        </Menu.Item>
        <Menu.Item
          key="2"
          icon={<PrinterOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("UserDebt")}
        >
          Faturalarım
        </Menu.Item>
        <Menu.Item
          key="3"
          icon={<CreditCardOutlined style={{ fontSize: "18px" }} />}
          onClick={() => changePageState("UserPayment")}
        >
          Ödemelerim
        </Menu.Item>
        <Menu.Item
          key="4"
          icon={<LogoutOutlined style={{ fontSize: "18px" }} />}
          onClick={logout}
        >
          Çıkış
        </Menu.Item>
      </Menu>
    );
  }

  return (
    <div>
      <Layout>
        <Sider
          className="sider"
          trigger={null}
          collapsible
          collapsed={collapsed}
        >
          <div style={{ height: "64px" }}>
            {collapsed ? (
              <MenuUnfoldOutlined
                className="sider-button sider-collapsed"
                onClick={siderButtonOnClick}
              />
            ) : (
              <MenuFoldOutlined
                className="sider-button sider-not-collapsed"
                onClick={siderButtonOnClick}
              />
            )}
          </div>
          <div className="logo">
            <img
              src={process.env.PUBLIC_URL + "/logo.png"}
              className={logoClassName}
              alt="site-fatura-yönetim-logo"
            />
          </div>
          {collapsed ? null : (
            <div className="sider-header">
              <h2>Site Fatura Ödeme Sistemi</h2>
            </div>
          )}
          <Divider />
          {menu}
        </Sider>
        <Content
          className="site-layout-background"
          style={{
            margin: "24px 16px",
            padding: 0,
          }}
        >
          <div>
            <Message />
            {pageState === "User" && <UserPage />}
            {pageState === "Apartment" && <ApartmentPage />}
            {pageState === "Debt" && <DebtPage />}
            {pageState === "Payment" && <PaymentPage />}
            {pageState === "UserInfo" && <UserInfoPage />}
            {pageState === "UserDebt" && <UserDebtPage />}
            {pageState === "UserPayment" && <UserPaymentPage />}
          </div>
        </Content>
      </Layout>
    </div>
  );
};
export default AppLayout;
