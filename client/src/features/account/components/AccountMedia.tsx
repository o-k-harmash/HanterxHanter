type AccountMediaProps = {
  profilePictureUrl: string;
  progressPercentage: number;
};

const AccountMedia = ({
  profilePictureUrl,
  progressPercentage,
}: AccountMediaProps) => {
  return (
    <div className="account-profile__media">
      <img
        className="responsive-image"
        width="145px"
        height="145px"
        style={{
          backgroundSize: "cover",
          backgroundImage: `url("data:image/;base64,${profilePictureUrl}")`,
        }}
      />
      <svg
        className="account-profile__radial-progress"
        data-percentage={progressPercentage}
        viewBox="0 0 80 80"
      >
        <circle
          className="account-profile__radial-progress-part account-profile__radial-progress-part_complete"
          cx="40"
          cy="40"
          r="35"
        ></circle>
        <circle
          className="account-profile__radial-progress-part account-profile__radial-progress-part_incomplete"
          cx="40"
          cy="40"
          r="35"
        ></circle>
      </svg>
      <div className="account-profile__fill">
        <span>{progressPercentage}%</span>
      </div>
    </div>
  );
};

export default AccountMedia;
