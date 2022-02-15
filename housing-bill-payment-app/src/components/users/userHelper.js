export const getInitialValues = (user) => {
  let initialValues = {
    name: "",
    surname: "",
    tckn: "",
    eMail: "",
    phone: "",
    carPlate: "",
  };
  if (user) {
    initialValues = {
      id: user.id,
      name: user.name,
      surname: user.surname,
      tckn: user.tckn,
      eMail: user.eMail,
      phone: user.phone,
      carPlate: user.carPlate,
    };
  }
  return initialValues;
};
