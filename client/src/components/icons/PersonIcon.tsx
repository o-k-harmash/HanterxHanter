import Icon from "./Icon";
import { CertainIconProps } from "./models";

function PersonIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="person" iconProps={{ fill: color }}></Icon>;
}

export default PersonIcon;
