import React, { useEffect, useState } from "react";
import {
  getUsers,
  deleteUser,
  addUser,
  updateUser,
} from "../../store/userServices";
import UserTable from "./UserTable";

const User = () => {
  const [users, setUsers] = useState([]);
  const [refresh, setRefresh] = useState(false);
  useEffect(() => {
    if (refresh) {
      getUsers().then((res) => setUsers(res.data));
      setRefresh(false);
    }
  }, [refresh]);

  useEffect(() => {
    getUsers().then((res) => setUsers(res.data));
  }, []);

  const onDelete = (id) => {
    deleteUser(id);
    setRefresh(true);
  };

  const onAdd = (user) => {
    addUser(user);
    setRefresh(true);
  };

  const onUpdate = (user) => {
    updateUser(user);
    setRefresh(true);
  };
  let component = (
    <div>
      <UserTable
        users={users}
        onDelete={onDelete}
        onAdd={onAdd}
        onUpdate={onUpdate}
      />
    </div>
  );
  return component;
};

export default User;
