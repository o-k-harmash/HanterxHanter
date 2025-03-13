import Icon from "./Icon";
import { CertainIconProps } from "./models";

function PencilIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="pencil" iconProps={{ stroke: color }}></Icon>;
}

export default PencilIcon;
