import React from "react";
import MaterialTable from "material-table";
import ApartmentForm from "./ApartmentForm";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import OpenDialog from "../helpers/dialog/OpenDialog";
import ConfirmModalButton from "../helpers/confirm/ConfirmModalButton";

const ApartmentTable = ({ apartments, onAdd, onUpdate, onDelete }) => {
  const columns = [
    {
      title: "Blok",
      field: "block",
    },
    {
      title: "Kat",
      field: "floor",
    },
    {
      title: "Daire No",
      field: "apartmentNo",
    },
    {
      title: "Daire Sahibi",
      field: "user",
    },
    {
      title: "Durum",
      dataIndex: "isEmpty",
      key: "Durum",
      render: (apartment) =>
        apartment.isEmpty ? (
          <p style={{ color: "red" }}>Boş</p>
        ) : (
          <p style={{ color: "green" }}>Dolu</p>
        ),
    },
    {
      title: "Tipi",
      field: "type",
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
          <ApartmentForm apartment={rowData} onUpdate={onUpdate} />
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
          question="Daireyi silmek istediğinize emin misiniz?"
          title="Daire Silme İşlemi"
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
            buttonText={"Daire Ekle"}
          >
            <ApartmentForm add onAdd={onAdd} />
          </OpenDialog>
        </div>
      );
    },
  };

  return (
    <MaterialTable
      title={"Daireler"}
      columns={columns}
      data={apartments}
      components={components}
    />
  );
};

export default ApartmentTable;
