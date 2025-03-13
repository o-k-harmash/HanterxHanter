import { CertainIconProps } from "./models";
import Icon from "./Icon";

function MusicIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="music" iconProps={{ stroke: color }}></Icon>;
}

export default MusicIcon;
