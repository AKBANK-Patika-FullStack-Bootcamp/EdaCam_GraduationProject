import AppLayout from "./hoc/AppLayout";
import Login from "./components/login/Login";
import { useEffect, useState } from "react";
import Auth from "./authentication/Authentication";

const App = () => {
  const auth = new Auth();
  const [logged, setLogged] = useState(auth.loggedIn());
  const [authority, setAuthority] = useState("");

  useEffect(() => {
    setAuthority(auth.getAuthority());
  }, [logged]);
  let page = "";
  if (logged) {
    page = <AppLayout setLogin={setLogged} authority={authority} />;
  } else {
    page = <Login setLogin={setLogged} setAuthority={setAuthority} />;
  }

  return <div className="App">{page}</div>;
};

export default App;
