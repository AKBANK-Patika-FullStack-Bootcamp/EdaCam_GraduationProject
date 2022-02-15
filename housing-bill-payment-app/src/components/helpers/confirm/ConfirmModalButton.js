import React from "react";
import { Button, Modal } from "antd";

const ConfirmModalButton = (props) => {
  const handleButtonClick = () => {
    Modal.confirm({
      title: props.title,
      centered: true,
      cancelText: "Hayır",
      content: <p>{props.question}</p>,
      okText: "Evet",
      onOk: () => props.onYes(),
    });
  };

  return (
    <Button
      type={props.buttonType}
      size={props.buttonSize}
      icon={props.buttonIcon}
      onClick={handleButtonClick}
    >
      {props.buttonText}
    </Button>
  );
};

export default ConfirmModalButton;
