import { Link } from "react-router";
import IconButton from "../../../components/IconButton";
import AccountMedia from "./AccountMedia";
import { User } from "../../../api/constants/usersApiResponse";

type AccountProfileProps = {
  profile: User;
};

const AccountProfile = ({ profile }: AccountProfileProps) => (
  <div className="account-profile horizontal-center">
    <div className="account-profile__about vertical-center">
      <IconButton type="settings" style="style-3"></IconButton>
      <AccountMedia
        profilePictureUrl={profile.avatar}
        progressPercentage={40}
      ></AccountMedia>
      <Link to={"/edit"}>
        <IconButton type="pencil" style="style-3"></IconButton>
      </Link>
    </div>
    <div className="account-profile__details">
      <h5>
        {profile.firstName}, {profile.age}
      </h5>
      <p>
        {profile.address.countryId}, {profile.address.cityId}
      </p>
    </div>
  </div>
);

export default AccountProfile;
