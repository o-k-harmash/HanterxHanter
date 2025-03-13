import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { useApiStatus } from "../../../hooks/useApiStatus";
import {
  fetchProfiles,
  setProfilesPage,
  setProfilesPosition,
} from "../profilesSlice";

const useProfiles = () => {
  const { isIdle, isError, isPending, isSuccess } = useApiStatus(
    (state) => state.profiles.status
  );

  const dispatch = useAppDispatch();
  const {
    profiles,
    profilesPosition,
    profilesPage,
    profilesLength,
    totalPages,
  } = useAppSelector((state) => state.profiles);

  useEffect(() => {
    if (isIdle) {
      dispatch(fetchProfiles());
    }
  }, [isIdle, dispatch]);

  const handleNextProfile = () => {
    //todo: refactor
    if (!isSuccess) return;
    const newProfilePosition = (profilesPosition + 1) % profilesLength;
    if (!newProfilePosition) {
      if (totalPages === profilesPage) return;
      dispatch(setProfilesPage(profilesPage + 1));
      dispatch(fetchProfiles());
    }
    dispatch(setProfilesPosition(newProfilePosition));
  };

  return {
    isEnd:
      profilesPosition + 1 === profilesLength && totalPages === profilesPage,
    isIdle,
    isError,
    isPending,
    profile: profiles[profilesPosition],
    nextProfile: handleNextProfile,
  };
};

export default useProfiles;
