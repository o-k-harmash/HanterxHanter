import { useAppSelector } from "../../../app/hooks";
import IconTag from "../../../components/IconTag";

type ProfileDetailsProps = {
  bio: string;
  interests: string[];
  languages: string[];
};

const ProfileDetails = ({ interests, languages, bio }: ProfileDetailsProps) => {
  const { dropdownList, locale } = useAppSelector((state) => state.app);

  if (!dropdownList) return;

  const listInterestItems = interests.map((interest, index) => (
    <li key={index}>
      <IconTag type={dropdownList[locale][interest]} />
    </li>
  ));

  const listLanguageItems = languages.map((language, index) => (
    <li key={index}>
      <IconTag type={dropdownList[locale][language]} />
    </li>
  ));

  return (
    <div className="profile-details">
      <h6>{dropdownList[locale]["basic_information"]}</h6>
      <p>{bio}</p>
      <div className="profile-details__interests">
        <h6>{dropdownList[locale]["interests"]}</h6>
        <ul className="profile-details__interests-list flex-row">
          {listInterestItems}
        </ul>
      </div>
      <div className="profile-details__interests">
        <h6>{dropdownList[locale]["languages"]}</h6>
        <ul className="profile-details__interests-list flex-row">
          {listLanguageItems}
        </ul>
      </div>
    </div>
  );
};

export default ProfileDetails;
