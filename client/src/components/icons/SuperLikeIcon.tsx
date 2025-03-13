import Icon from "./Icon";
import { CertainIconProps } from "./models";

function SuperlikeIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="superlike" iconProps={{ fill: color }}></Icon>;
}

export default SuperlikeIcon;
