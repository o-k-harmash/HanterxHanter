import Icon from "./Icon";
import { CertainIconProps } from "./models";

function StudyIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="study" iconProps={{ stroke: color }}></Icon>;
}

export default StudyIcon;
