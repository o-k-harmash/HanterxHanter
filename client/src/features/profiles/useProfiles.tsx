import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { useApiStatus } from "../../hooks/useApiStatus";
import { fetchProfiles, setProfilesPosition } from "./profilesSlice";

const useProfiles = () => {
  const { isIdle, isError, isPending, isSuccess } = useApiStatus(
    (state) => state.profiles.status
  );

  const dispatch = useAppDispatch();

  const { profileList, profilesPosition } = useAppSelector(
    (state) => state.profiles
  );

  useEffect(() => {
    if (isIdle) {
      dispatch(fetchProfiles());
    }
  }, [isIdle, dispatch]);

  const handleNextProfile = () => {
    if (profileList == null) return;
    if (!profileList.filterPageData.isEnd && profilesPosition >= profileList.filterPageData.size)
      dispatch(fetchProfiles());
    dispatch(setProfilesPosition(profilesPosition + 1));
  };

  return {
    isEnd: profileList?.profileList[profilesPosition - 1] == null,
    isIdle,
    isError,
    isPending,
    isSuccess,
    profile: profileList?.profileList[profilesPosition - 1],
    nextProfile: handleNextProfile,
  };
};

export default useProfiles;
