import React, { useEffect, useState } from "react";
import moment from "moment";
import MaterialTable from "material-table";
import { getUserPayment } from "../../store/paymentService";
import Auth from "../../authentication/Authentication";

const UserPaymentTable = () => {
  const auth = new Auth();
  const userId = auth.getCurrentUser();

  const [payments, setPayments] = useState([]);

  useEffect(() => {
    getUserPayment(userId).then((res) => setPayments(res.data));
  }, []);
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
      title: "Fatura Tipi",
      field: "debtType",
    },
  ];
  return <MaterialTable title="Ödemeler" columns={columns} data={payments} />;
};

export default UserPaymentTable;
