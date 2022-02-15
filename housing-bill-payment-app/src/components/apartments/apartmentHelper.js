export const getInitialValues = (apartment) => {
  let initialValues = {
    block: "",
    floor: "",
    apartmentNo: "",
    isEmpty: false,
    type: "",
    userId: null,
  };
  if (apartment) {
    initialValues = {
      id: apartment.id,
      block: apartment.block,
      floor: apartment.floor,
      apartmentNo: apartment.apartmentNo,
      isEmpty: apartment.isEmpty ? true : false,
      type: apartment.type,
      userId: apartment.userId,
    };
  }
  return initialValues;
};
