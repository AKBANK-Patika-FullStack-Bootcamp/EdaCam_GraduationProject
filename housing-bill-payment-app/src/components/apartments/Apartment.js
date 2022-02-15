import React, { useState, useEffect } from "react";
import {
  getApartmentDetails,
  addApartment,
  updateApartment,
  deleteApartment,
} from "../../store/apatmentServices";
import ApartmentTable from "./ApartmentTable";

const Apartment = () => {
  const [apartments, setApartments] = useState([]);

  const [refresh, setRefresh] = useState(false);

  useEffect(() => {
    if (refresh) {
      getApartmentDetails().then((res) => setApartments(res.data));
      setRefresh(false);
    }
  }, [refresh]);

  useEffect(() => {
    getApartmentDetails().then((res) => setApartments(res.data));
  }, []);

  const onDelete = (id) => {
    deleteApartment(id);
    setRefresh(true);
  };

  const onAdd = (apartment) => {
    addApartment(apartment);
    setRefresh(true);
  };

  const onUpdate = (apartment) => {
    updateApartment(apartment);
    setRefresh(true);
  };
  let component = (
    <div>
      <ApartmentTable
        apartments={apartments}
        onDelete={onDelete}
        onAdd={onAdd}
        onUpdate={onUpdate}
      />
    </div>
  );
  return component;
};

export default Apartment;
