import React from "react";
import { Badge, Button } from "antd";
import { MessageOutlined } from "@ant-design/icons";

const Message = () => {
  const handleMessages = () => {
    return <div>Mesaj</div>;
  };
  return (
    <Badge count={5}>
      <Button
        icon={<MessageOutlined />}
        size="large"
        onClick={handleMessages}
      />
    </Badge>
  );
};

export default Message;
