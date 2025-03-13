import Icon from "./Icon";
import { CertainIconProps } from "./models";

function BellIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="bell" iconProps={{ fill: color }}></Icon>;
}

export default BellIcon;
