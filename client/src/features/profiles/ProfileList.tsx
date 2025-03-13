import { ReactNode } from "react";
import { Spinner } from "../../components/Spinner";
import Navbar from "../../layouts/Navbar";
import ActionFooter from "./components/ActionFooter";
import ProfileCard from "./components/ProfileCard";
import ProfileDetails from "./components/ProfileDetails";
import useProfiles from "./hooks/useProfiles";

const ProfileList = () => {
  const { isEnd, isPending, isError, profile, nextProfile } = useProfiles();

  let content: ReactNode;

  if (isPending) {
    content = <Spinner text="Loading..." />;
  } else if (isError) {
    content = <Spinner text="Something Wrong!" />;
  } else if (isEnd || !profile) {
    content = <Spinner text="No more profiles for you." />;
  } else {
    content = (
      <div className="profiles__container container">
        <div className="profiles__profile-area">
          <ProfileCard
            profilePicture={profile.fileStrings[0]}
            regionName={profile.cityString}
            name={profile.name}
            age={profile.age}
          />
          <ProfileDetails
            bio={"dsadasda"}
            interests={profile.interestStrings}
            languages={profile.languagesStrings}
          />
        </div>
      </div>
    );
  }

  return (
    <>
      <Navbar type="outlined"></Navbar>
      <div className="profiles">{content}</div>
      <ActionFooter
        like={nextProfile}
        dislike={nextProfile}
        superlike={nextProfile}
      ></ActionFooter>
    </>
  );
};

export default ProfileList;
