import React from "react";
import MaterialTable from "material-table";
import UserForm from "./UserForm";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import OpenDialog from "../helpers/dialog/OpenDialog";
import ConfirmModalButton from "../helpers/confirm/ConfirmModalButton";

const UserTable = ({ users, onAdd, onUpdate, onDelete }) => {
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
    {
      title: "",
      cellStyle: {
        textAlign: "center",
      },
      render: (rowData) => (
        <OpenDialog
          icon={<EditOutlined style={{ color: "green" }} />}
          title={"Güncelle"}
        >
          <UserForm user={rowData} onUpdate={onUpdate} />
        </OpenDialog>
      ),
    },
    {
      title: "",
      cellStyle: {
        textAlign: "center",
      },
      render: (rowData) => (
        <ConfirmModalButton
          buttonIcon={<DeleteOutlined style={{ color: "red" }} />}
          question="Kullanıcıyı silmek istediğinize emin misiniz?"
          title="Kullanıcı Silme İşlemi"
          onYes={() => {
            onDelete(rowData.id);
          }}
        />
      ),
    },
  ];

  const components = {
    Actions: () => {
      return (
        <div style={{ marginLeft: 20 }}>
          <OpenDialog
            icon={<PlusOutlined />}
            title={"Ekle"}
            withButton
            buttonText={"Kullanıcı Ekle"}
          >
            <UserForm add onAdd={onAdd} />
          </OpenDialog>
        </div>
      );
    },
  };

  return (
    <MaterialTable
      title="Kullanıcılar"
      columns={columns}
      data={users}
      components={components}
    />
  );
};

export default UserTable;
