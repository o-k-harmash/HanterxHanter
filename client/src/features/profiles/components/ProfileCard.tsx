import { useAppSelector } from "../../../app/hooks";

type PeopleCardProps = {
  name: string;
  age: number;
  profilePicture: string;
  regionName: string;
};

const ProfileCard = ({
  name,
  age,
  profilePicture,
  regionName,
}: PeopleCardProps) => {
  const { dropdownList, locale } = useAppSelector((state) => state.app);

  if (!dropdownList) return;

  return (
    <div className="profile-card">
      <div className="profile-card__body">
        <div className="profile-card__media">
          <img
            className="profile-card__media-image responsive-image"
            src={`http://localhost:5010/uploads/${profilePicture}`}
            alt="Profile picture"
          />
        </div>
        <div className="profile-card__content vertical-center">
          <div>
            <h4>{`${name}, ${age}`}</h4>
            <p>{dropdownList[locale][regionName]}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfileCard;
