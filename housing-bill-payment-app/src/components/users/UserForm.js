import React from "react";
import { Form, Input, Button } from "antd";
import { useFormik } from "formik";
import { getInitialValues } from "./userHelper";

const UserForm = ({ user, add, onUpdate, onAdd, ...props }) => {
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
    initialValues: getInitialValues(user),
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
  /*validationSchema: Yup.object().shape({
        tckn: Yup.string().test('empty-or-11-characters-check', 'TC kimlik no geçersiz', (value:any) => !value || (value.length === 11 && value >0)),
        customerNo: Yup.string().required('Doldurulması zorunlu alan'),
        name: Yup.string().matches(/^[A-Za-zığüşöçİĞÜŞÖÇ\s]+$/, 'Lütfen geçerli bir ad giriniz.').required("Doldurulması zorunlu alan"),
        surname: Yup.string().matches(/^[A-Za-zığüşöçİĞÜŞÖÇ\s]+$/, 'Lütfen geçerli bir soyad giriniz.').required("Doldurulması zorunlu alan"),
        balance: Yup.number().min(0,'Bakiye negatif bir sayı olamaz.').required('Doldurulması zorunlu alan'),
        phoneNumber: Yup.string().length(11,"Telefon numarası eksik").required('Doldurulması zorunlu alan'),
        email: Yup.string().email("E-posta adresi geçersiz"),
        registirationDate: Yup.date().required('Doldurulması zorunlu alan'),
    })
  });*/
  return (
    <Form layout="vertical" style={{ width: "300px" }} onFinish={handleSubmit}>
      <Form.Item
        required
        //validateStatus={touched.tckn && errors.tckn ? "error" : ""}
        // help={touched.tckn && errors.tckn ? errors.tckn : null}
        label="Ad"
      >
        <Input {...getFieldProps("name")} placeholder="Ad" name="name"></Input>
      </Form.Item>
      <Form.Item
        required
        // validateStatus={
        //   touched.customerNo && errors.customerNo ? "error" : ""
        // }
        // help={
        //   touched.customerNo && errors.customerNo ? errors.customerNo : null
        // }
        label="Soyad"
      >
        <Input
          {...getFieldProps("surname")}
          placeholder="Soyad"
          name="surname"
        ></Input>
      </Form.Item>
      <Form.Item
        required
        // validateStatus={touched.name && errors.name ? "error" : ""}
        // help={touched.name && errors.name ? errors.name : null}
        label="TCKN"
      >
        <Input
          {...getFieldProps("tckn")}
          placeholder="TCKN"
          name="tckn"
        ></Input>
      </Form.Item>
      <Form.Item
        required
        // validateStatus={touched.surname && errors.surname ? "error" : ""}
        // help={touched.surname && errors.surname ? errors.surname : null}
        label="E-Posta"
      >
        <Input
          {...getFieldProps("eMail")}
          placeholder="E-Posta"
          name="eMail"
        ></Input>
      </Form.Item>
      <Form.Item
        required
        // validateStatus={touched.active && errors.active ? "error" : ""}
        // help={touched.active && errors.active ? errors.active : null}
        label="Telefon"
      >
        <Input
          {...getFieldProps("phone")}
          placeholder="Telefon"
          name="phone"
        ></Input>
      </Form.Item>
      <Form.Item
        required
        // validateStatus={touched.active && errors.active ? "error" : ""}
        // help={touched.active && errors.active ? errors.active : null}
        label="Araç Plakası"
      >
        <Input
          {...getFieldProps("carPlate")}
          placeholder="Araç Plakası"
          name="carPlate"
        ></Input>
      </Form.Item>
      <Form.Item style={{ textAlign: "center" }}>
        <Button type="primary" htmlType="submit">
          {add ? "Ekle" : "Güncelle"}
        </Button>
      </Form.Item>
    </Form>
  );
};

export default UserForm;
