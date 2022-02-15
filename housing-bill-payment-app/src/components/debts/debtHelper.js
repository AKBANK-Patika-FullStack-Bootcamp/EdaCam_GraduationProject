export const getInitialValues = (debt) => {
  let initialValues = {
    date: "",
    amount: "",
    userId: null,
    debtTypeId: "",
  };
  if (debt) {
    initialValues = {
      id: debt.id,
      date: debt.date,
      amount: debt.amount,
      userId: debt.userId,
      debtTypeId: debt.debtTypeId,
    };
  }
  return initialValues;
};
