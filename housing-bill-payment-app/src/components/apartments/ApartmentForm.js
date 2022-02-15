import React, { useState } from "react";
import { Form, Input, Radio, Button } from "antd";
import { useFormik } from "formik";
import { getInitialValues } from "./apartmentHelper";
import { Select } from "antd";
import { getUsers } from "../../store/userServices";

const { Option } = Select;

const ApartmentForm = ({ apartment, add, onUpdate, onAdd, ...props }) => {
  const [userList, setUserList] = useState([]);
  const getUserList = () => {
    getUsers().then((res) => setUserList(res.data));
  };
  const {
    errors,
    touched,
    values,
    getFieldProps,
    handleSubmit,
    handleChange,
    setFieldValue,
    setFieldTouched,
    isValid,
  } = useFormik({
    initialValues: getInitialValues(apartment),
    onSubmit(values) {
      const data = { ...values };
      if (add) {
        onAdd(data);
      } else {
        onUpdate(data);
      }
      props.onClose();
    },
  });
  const userOptions = userList.map((user) => {
    return (
      <Option value={user.id} key={user.id}>
        {user.name + " " + user.surname}
      </Option>
    );
  });
  const handleUserChange = (value) => {
    setFieldValue("userId", value);
  };
  return (
    <Form layout="vertical" style={{ width: "300px" }} onFinish={handleSubmit}>
      <Form.Item required label="Blok">
        <Input
          {...getFieldProps("block")}
          placeholder="Blok"
          name="block"
        ></Input>
      </Form.Item>
      <Form.Item required label="Kat">
        <Input
          {...getFieldProps("floor")}
          placeholder="Kat"
          name="floor"
        ></Input>
      </Form.Item>
      <Form.Item required label="Daire No">
        <Input
          {...getFieldProps("apartmentNo")}
          placeholder="Daire No"
          name="apartmentNo"
        ></Input>
      </Form.Item>
      <Form.Item required label="Tipi">
        <Input
          {...getFieldProps("type")}
          placeholder="Tipi"
          name="type"
        ></Input>
      </Form.Item>
      <Form.Item required label="Durumu">
        <Radio.Group
          {...getFieldProps("isEmpty")}
          optionType="button"
          buttonStyle="solid"
        >
          <Radio.Button value={true}>Boş</Radio.Button>
          <Radio.Button value={false}>Dolu</Radio.Button>
        </Radio.Group>
      </Form.Item>
      <Form.Item label="Daire Sahibi" name="userId">
        <Select
          dropdownStyle={{ zIndex: 2000 }}
          {...getFieldProps("userId")}
          onClick={getUserList}
          onChange={handleUserChange}
        >
          {userOptions}
        </Select>
      </Form.Item>
      <Form.Item style={{ textAlign: "center" }}>
        <Button type="primary" htmlType="submit">
          {add ? "Ekle" : "Güncelle"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default ApartmentForm;
