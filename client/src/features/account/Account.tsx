import { ReactNode } from "react";
import Navbar from "../../layouts/Navbar";
import { Spinner } from "../../components/Spinner";
import AccountStatistic from "./components/AccountStatistic";
import AccountProfile from "./components/AccountProfile";
import AccountAdvertesment from "./components/AccountAdvertesment";
import useAccount from "./useAccount";

function Account() {
  const { isPending, isError, account } = useAccount();

  let content: ReactNode;

  const spiner = <Spinner></Spinner>;
  if (isPending) {
    content = spiner;
  } else if (isError) {
    content = spiner;
  } else if (!account) {
    content = spiner;
  } else {
    content = (
      <div className="account__container container">
        <div className="account__profile-area">
          <AccountProfile profile={account}></AccountProfile>
          <AccountStatistic superLikes={0}></AccountStatistic>
          <AccountAdvertesment></AccountAdvertesment>
        </div>
      </div>
    );
  }

  return (
    <>
      <Navbar></Navbar>
      <div className="account">{content}</div>
    </>
  );
}

export default Account;
