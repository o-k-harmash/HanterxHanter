import Icon from "./Icon";

import { CertainIconProps } from "./models";

function InstagramIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="instagram" iconProps={{ stroke: color }}></Icon>;
}

export default InstagramIcon;
