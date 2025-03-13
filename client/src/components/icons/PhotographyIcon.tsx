import Icon from "./Icon";
import { CertainIconProps } from "./models";

function PhotographyIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="photography" iconProps={{ stroke: color }}></Icon>;
}

export default PhotographyIcon;
