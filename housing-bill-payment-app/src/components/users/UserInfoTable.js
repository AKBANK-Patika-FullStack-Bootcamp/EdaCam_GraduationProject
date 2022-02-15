import React, { useState, useEffect } from "react";
import MaterialTable from "material-table";
import { getUserById } from "../../store/userServices";
import Auth from "../../authentication/Authentication";

const UserInfoTable = () => {
  const auth = new Auth();
  const userId = auth.getCurrentUser();

  const [users, setUsers] = useState([]);
  useEffect(() => {
    getUserById(userId).then((res) => setUsers(res.data));
  }, []);
  const columns = [
    {
      title: "Ad",
      field: "name",
    },
    {
      title: "Soyad",
      field: "surname",
    },
    {
      title: "TCKN",
      field: "tckn",
    },
    {
      title: "E-Posta",
      field: "eMail",
    },
    {
      title: "Telefon",
      field: "phone",
    },
    {
      title: "Araç Bilgisi",
      field: "carPlate",
    },
  ];
  return (
    <MaterialTable
      title="Kullanıcı Bilgileri"
      columns={columns}
      data={[users]}
    />
  );
};

export default UserInfoTable;
