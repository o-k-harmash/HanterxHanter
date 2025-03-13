import { JSX } from "react";
import IconButton from "./IconButton";

type CardProps = {
  icon: JSX.Element;
  content: string;
};

function Card({ icon, content }: CardProps) {
  return (
    <div className="card horizontal">
      <div className="card__body horizontal-center">
        <div className="card__body-icon">
          {/* <SuperlikeIcon color='#00aeff'></SuperlikeIcon> */}
          {icon}
        </div>
        <div className="card__body-content">
          <p>{content}</p>
        </div>
        <IconButton type={"plus"} style="style-2"></IconButton>
      </div>
    </div>
  );
}

export default Card;
