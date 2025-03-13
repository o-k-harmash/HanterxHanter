import Icon from "./Icon";

import { CertainIconProps } from "./models";

function LikeIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="like" iconProps={{ fill: color }}></Icon>;
}

export default LikeIcon;
