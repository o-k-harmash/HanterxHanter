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
  return (
    <div className="profile-card vertical-center">
      <div className="profile-card__body">
        <div className="profile-card__media">
          <img
            className="profile-card__media-image responsive-image"
            src={profilePicture}
            alt="Profile picture"
          />
        </div>
        <div className="profile-card__content vertical-center">
          <div>
            <h4>{`${name}, ${age}`}</h4>
            <p>{`${regionName}`}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfileCard;
