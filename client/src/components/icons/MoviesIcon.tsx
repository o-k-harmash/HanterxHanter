import { CertainIconProps } from "./models";
import Icon from "./Icon";

function MoviesIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="movies" iconProps={{ stroke: color }}></Icon>;
}

export default MoviesIcon;
