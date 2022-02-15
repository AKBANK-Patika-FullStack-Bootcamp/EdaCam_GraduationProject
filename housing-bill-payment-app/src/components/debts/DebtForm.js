import React, { useState } from "react";
import { Form, Input, Button } from "antd";
import { useFormik } from "formik";
import { getInitialValues } from "./debtHelper";
import { Select } from "antd";
import { getUsers } from "../../store/userServices";
import { getDebtTypes } from "../../store/debtTypeService";

const { Option } = Select;
const DebtForm = ({
  debt,
  add,
  bulkAdd,
  onUpdate,
  onAdd,
  onBulkAdd,
  ...props
}) => {
  const [userList, setUserList] = useState([]);
  const [debtTypeList, setDebtTypeList] = useState([]);
  const getUserList = () => {
    getUsers().then((res) => setUserList(res.data));
  };

  const getDebtTypeList = () => {
    getDebtTypes().then((res) => setDebtTypeList(res.data));
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
    initialValues: getInitialValues(debt),
    onSubmit(values) {
      const data = { ...values };
      if (add) {
        onAdd(data);
      }
      if (bulkAdd) {
        onBulkAdd(data);
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
  const debtTypeOptions = debtTypeList.map((debtType) => {
    return (
      <Option value={debtType.id} key={debtType.id}>
        {debtType.type}
      </Option>
    );
  });
  const handleDebtTypeChange = (value) => {
    setFieldValue("debtTypeId", value);
  };

  return (
    <Form layout="vertical" style={{ width: "300px" }} onFinish={handleSubmit}>
      <Form.Item required label="Tarih">
        <Input
          {...getFieldProps("date")}
          placeholder="Tarih"
          name="date"
        ></Input>
      </Form.Item>
      <Form.Item required label="Tutar">
        <Input
          {...getFieldProps("amount")}
          placeholder="Tutar"
          name="amount"
        ></Input>
      </Form.Item>
      <Form.Item label="Fatura Tipi" name="debtTypeId">
        <Select
          dropdownStyle={{ zIndex: 2000 }}
          {...getFieldProps("debtTypeId")}
          onClick={getDebtTypeList}
          onChange={handleDebtTypeChange}
        >
          {debtTypeOptions}
        </Select>
      </Form.Item>
      <Form.Item label="Fatura Sahibi" name="userId">
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
          {add || bulkAdd ? "Ekle" : "Güncelle"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default DebtForm;
