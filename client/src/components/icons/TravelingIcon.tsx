import Icon from "./Icon";
import { CertainIconProps } from "./models";

function TravelingIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="traveling" iconProps={{ stroke: color }}></Icon>;
}

export default TravelingIcon;
