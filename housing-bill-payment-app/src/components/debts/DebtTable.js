import React from "react";
import moment from "moment";
import MaterialTable from "material-table";
import DebtForm from "./DebtForm";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import OpenDialog from "../helpers/dialog/OpenDialog";
import ConfirmModalButton from "../helpers/confirm/ConfirmModalButton";

const DebtTable = ({ debts, onAdd, onBulkAdd, onUpdate, onDelete }) => {
  const columns = [
    {
      title: "Tarih",
      field: "date",
      render: (debt) =>
        moment(debt.date, "YYYY-MM-DD HH:mm:ss").format("DD/MM/YYYY HH:mm:ss"),
    },
    {
      title: "Tutar",
      field: "amount",
    },
    {
      title: "Fatura Tipi",
      field: "debtType",
    },
    {
      title: "Fatura Sahibi",
      field: "user",
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
          <DebtForm debt={rowData} onUpdate={onUpdate} />
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
          question="Faturayı silmek istediğinize emin misiniz?"
          title="Fatura Silme İşlemi"
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
        <div style={{ marginLeft: 20, marginTop: 10 }}>
          <div style={{ marginBottom: 5 }}>
            {" "}
            <OpenDialog
              icon={<PlusOutlined />}
              title={"Ekle"}
              withButton
              buttonText={"Fatura Ekle"}
            >
              <DebtForm add onAdd={onAdd} />
            </OpenDialog>
          </div>
          <div>
            {" "}
            <OpenDialog
              icon={<PlusOutlined />}
              title={"Toplu Ekle"}
              withButton
              buttonText={"Toplu Fatura Ekle"}
            >
              <DebtForm bulkAdd onBulkAdd={onBulkAdd} />
            </OpenDialog>
          </div>
        </div>
      );
    },
  };
  return (
    <MaterialTable
      title="Faturalar"
      columns={columns}
      data={debts}
      components={components}
    />
  );
};

export default DebtTable;
