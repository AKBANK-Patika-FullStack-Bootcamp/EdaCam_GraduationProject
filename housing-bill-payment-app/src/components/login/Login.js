import React, { useState } from "react";
import { Form, Input, Button } from "antd";
import Auth from "../../authentication/Authentication";
import decode from "jwt-decode";

const Login = ({ setLogin, setAuthority }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const auth = new Auth();

  const handleSubmit = (e) => {
    auth.login(email, password).then((res) => {
      const decoded = decode(res.data.data.token);
      setAuthority(decoded.role);
      setLogin(true);
    });
  };

  return (
    <div
      style={{
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        height: "100%",
      }}
    >
      <Form
        layout="vertical"
        style={{ width: "300px" }}
        onFinish={handleSubmit}
      >
        <Form.Item label="E-Posta">
          <Input
            placeholder="E-Posta"
            name="eMail"
            onChange={(e) => setEmail(e.target.value)}
          ></Input>
        </Form.Item>
        <Form.Item label="Parola">
          <Input
            type="password"
            placeholder="Parola"
            name="password"
            onChange={(e) => setPassword(e.target.value)}
          ></Input>
        </Form.Item>
        <Form.Item style={{ textAlign: "center" }}>
          <Button type="primary" htmlType="submit">
            {"Giriş Yap"}
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default Login;
