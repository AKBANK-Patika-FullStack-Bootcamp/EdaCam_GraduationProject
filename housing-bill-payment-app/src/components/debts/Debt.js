import React, { useEffect, useState } from "react";
import {
  getDebtDetails,
  addDebt,
  addDebts,
  updateDebt,
  deleteDebt,
} from "../../store/debtServices";
import DebtTable from "./DebtTable";

const Debt = () => {
  const [debts, setDebts] = useState([]);
  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    if (refresh) {
      getDebtDetails().then((res) => setDebts(res.data));
      setRefresh(false);
    }
  }, [refresh]);

  useEffect(() => {
    getDebtDetails().then((res) => setDebts(res.data));
  }, []);

  const onDelete = (id) => {
    deleteDebt(id);
    setRefresh(true);
  };

  const onAdd = (debt) => {
    addDebt(debt);
    setRefresh(true);
  };
  const onBulkAdd = (debt) => {
    addDebts(debt);
    setRefresh(true);
  };

  const onUpdate = (debt) => {
    updateDebt(debt);
    setRefresh(true);
  };

  let component = (
    <div>
      <DebtTable
        debts={debts}
        onDelete={onDelete}
        onAdd={onAdd}
        onUpdate={onUpdate}
        onBulkAdd={onBulkAdd}
      />
    </div>
  );

  return component;
};

export default Debt;
