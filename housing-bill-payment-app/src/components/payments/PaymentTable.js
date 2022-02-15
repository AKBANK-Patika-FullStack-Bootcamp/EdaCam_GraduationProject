import React from "react";
import moment from "moment";
import MaterialTable from "material-table";

const PaymentTable = ({ payments }) => {
  const columns = [
    {
      title: "Ödeme Tarihi",
      field: "paymentDate",
      render: (payment) =>
        moment(payment.paymentDate, "YYYY-MM-DD HH:mm:ss").format(
          "DD/MM/YYYY HH:mm:ss"
        ),
    },
    {
      title: "Fatura Kesim Tarihi",
      field: "debtDate",
      render: (payment) =>
        moment(payment.debtDate, "YYYY-MM-DD HH:mm:ss").format(
          "DD/MM/YYYY HH:mm:ss"
        ),
    },
    {
      title: "Tutar",
      field: "amount",
    },
    {
      title: "Ödeme Sahibi",
      field: "user",
    },
    {
      title: "Fatura Tipi",
      field: "debtType",
    },
  ];
  return <MaterialTable title="Ödemeler" columns={columns} data={payments} />;
};

export default PaymentTable;
