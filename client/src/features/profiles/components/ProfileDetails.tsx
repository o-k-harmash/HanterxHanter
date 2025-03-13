import IconTag from "../../../components/IconTag";

type ProfileDetailsProps = {
  bio: string;
  interests: string[];
  languages: string[];
};

const ProfileDetails = ({ interests, languages, bio }: ProfileDetailsProps) => {
  const listInterestItems = interests.map((interest, index) => (
    <li key={index}>
      <IconTag type={interest} />
    </li>
  ));

  const listLanguageItems = languages.map((language, index) => (
    <li key={index}>
      <IconTag type={language} />
    </li>
  ));

  return (
    <div className="profile-details">
      <h6>Bio</h6>
      <p>{bio}</p>
      <div className="profile-details__interests">
        <h6>Interests</h6>
        <ul className="profile-details__interests-list flex-row">
          {listInterestItems}
        </ul>
      </div>
      <div className="profile-details__interests">
        <h6>Languages</h6>
        <ul className="profile-details__interests-list flex-row">
          {listLanguageItems}
        </ul>
      </div>
    </div>
  );
};

export default ProfileDetails;
