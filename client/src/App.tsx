import { Route, Routes } from "react-router";
import Account from "./features/account/Account";
import Login from "./features/auth/Login";
import Register from "./features/auth/Register";
import ProfileList from "./features/profiles/ProfileList";
import EditProfile from "./features/profiles/EditProfile";

function App() {
  return (
    <Routes>
      <Route path="/" element={<ProfileList />}></Route>
      <Route path="/account" element={<Account />}></Route>
      <Route path="/login" element={<Login />}></Route>
      <Route path="/register" element={<Register />}></Route>
      <Route path="/edit" element={<EditProfile />}></Route>
    </Routes>
  );
}

export default App;
