import Icon from "./Icon";
import { CertainIconProps } from "./models";

function HomeIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="home" iconProps={{ fill: color }}></Icon>;
}

export default HomeIcon;
