import React, { useEffect, useState } from "react";
import moment from "moment";
import MaterialTable from "material-table";
import { getUserDebt } from "../../store/debtServices";
import { PlusOutlined } from "@ant-design/icons";
import Auth from "../../authentication/Authentication";
import { addPayment } from "../../store/paymentService";
import ConfirmModalButton from "../helpers/confirm/ConfirmModalButton";

const UserDebtTable = () => {
  const auth = new Auth();
  const userId = auth.getCurrentUser();

  const [debts, setDebts] = useState([]);
  useEffect(() => {
    getUserDebt(userId).then((res) => setDebts(res.data));
  }, []);
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
      title: "",
      cellStyle: {
        textAlign: "center",
      },
      render: (rowData) => (
        <ConfirmModalButton
          buttonIcon={<PlusOutlined style={{ color: "red" }} />}
          question="Ödeme işlemi yapmak istediğinize emin misiniz?"
          title="Ödeme İşlemi"
          onYes={() => {
            let values = {
              userId: parseInt(userId),
              date: moment().format("YYYY-MM-DDTHH:mm:ss.Z"),
              debtId: rowData.id,
            };
            addPayment(values);
          }}
        />
      ),
    },
  ];

  return <MaterialTable title="Faturalar" columns={columns} data={debts} />;
};

export default UserDebtTable;
