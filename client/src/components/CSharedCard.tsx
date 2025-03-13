import { classname } from "../utils/styles";
import { CardHeader, CardClose, CardBody } from "./common/Card";

type CardWithCloseProps = {
  style?: React.CSSProperties;
  title: string;
  children: React.ReactNode;
  onClose?: () => void;
};

function CSharedCard({ style, title, children, onClose }: CardWithCloseProps) {
  const className = classname("c-card", "c-card_shared");

  const customStyle = {
    ...style,
  };

  return (
    <div className={className} style={customStyle}>
      <CardHeader title={title}>
        <CardClose onClose={onClose} />
      </CardHeader>
      <CardBody>{children}</CardBody>
    </div>
  );
}

export default CSharedCard;
