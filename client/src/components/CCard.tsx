import { classname } from "../utils/styles";
import { CardHeader, CardBody } from "./common/Card";

type CardProps = {
  style?: React.CSSProperties;
  title: string;
  children: React.ReactNode;
  onClick?: () => void;
};

function CCard({ style, title, children, onClick }: CardProps) {
  const customStyle = {
    ...style,
  };

  const className = classname("c-card", onClick && "c-card_clickbait");

  return (
    <div className={className} style={customStyle} onClick={onClick}>
      <CardHeader title={title} />
      <CardBody>{children}</CardBody>
    </div>
  );
}

export default CCard;
