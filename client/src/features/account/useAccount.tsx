import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { useApiStatus } from "../../hooks/useApiStatus";
import { getAccount } from "./accountSlice";

const useAccount = () => {
  const { isIdle, isError, isPending } = useApiStatus(
    (state) => state.account.status
  );
  const dispatch = useAppDispatch();
  const { account } = useAppSelector((state) => state.account);

  useEffect(() => {
    if (isIdle) {
      dispatch(getAccount());
    }
  }, [isIdle, dispatch]);

  return {
    isIdle,
    isError,
    isPending,
    account,
  };
};

export default useAccount;
