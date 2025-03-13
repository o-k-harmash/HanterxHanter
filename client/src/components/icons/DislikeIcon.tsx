import Icon from "./Icon";
import { CertainIconProps } from "./models";

function DislikeIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="dislike" iconProps={{ stroke: color }}></Icon>;
}

export default DislikeIcon;
