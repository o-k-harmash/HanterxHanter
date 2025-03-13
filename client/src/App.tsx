import { Route, Routes } from "react-router";
import Account from "./features/account/Account";
import Login from "./features/auth/Login";
import Register from "./features/auth/Register";
import ProfileList from "./features/profiles/ProfileList";
import { useAppDispatch } from "./app/hooks";
import { useEffect } from "react";
import { useApiStatus } from "./hooks/useApiStatus";
import { fetchDropdown } from "./app/appSlice";
import EditProfile from "./features/editProfile/EditProfile";

function App() {
  const { isIdle, isError, isPending, isSuccess } = useApiStatus(
    (state) => state.app.status
  );

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (isIdle) {
      dispatch(fetchDropdown());
    }
  }, [isIdle, dispatch]);

  return (
    <Routes>
      <Route path="/" element={<ProfileList />}></Route>
      <Route path="/account" element={<Account />}></Route>
      <Route path="/login" element={<Login />}></Route>
      <Route path="/register" element={<Register />}></Route>
      {/* <Route path="/edit" element={<EditProfile />}></Route> */}
      <Route path="/edit" element={<EditProfile />}></Route>
    </Routes>
  );
}

export default App;
