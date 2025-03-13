import Icon from "./Icon";
import { CertainIconProps } from "./models";

function PlusIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="plus" iconProps={{ fill: color }}></Icon>;
}

export default PlusIcon;
