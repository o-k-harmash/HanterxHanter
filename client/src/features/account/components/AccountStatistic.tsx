import Card from "../../../components/Card";
import BellIcon from "../../../components/icons/BellIcon";
import RocketIcon from "../../../components/icons/RocketIcon";
import SuperlikeIcon from "../../../components/icons/SuperLikeIcon";

const cardData = [
  {
    icon: <RocketIcon color="#FF50A2"></RocketIcon>,
    text: "My Boosts",
  },
  {
    icon: <BellIcon color="#ff586e"></BellIcon>,
    text: "Subscriptions",
  },
];

type AccountStatisticProps = {
  superLikes: number;
};

const AccountStatistic = ({ superLikes }: AccountStatisticProps) => (
  <div className="account-statistic">
    <Card
      icon={<SuperlikeIcon color="#00aeff"></SuperlikeIcon>}
      content={`${superLikes} Super Likes`}
    ></Card>
    {cardData.map((data, i) => (
      <Card key={i} icon={data.icon} content={data.text}></Card>
    ))}
  </div>
);

export default AccountStatistic;
