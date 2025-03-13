import Icon from "./Icon";
import { CertainIconProps } from "./models";

function RocketIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="rocket" iconProps={{ fill: color }}></Icon>;
}

export default RocketIcon;
