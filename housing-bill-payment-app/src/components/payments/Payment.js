import React, { useEffect, useState } from "react";
import { getPaymentDetails } from "../../store/paymentService";
import PaymentTable from "./PaymentTable";

const Payment = () => {
  const [payments, setPayments] = useState([]);

  useEffect(() => {
    getPaymentDetails().then((res) => setPayments(res.data));
  }, []);

  let component = (
    <div>
      <PaymentTable payments={payments} />
    </div>
  );
  return component;
};

export default Payment;
