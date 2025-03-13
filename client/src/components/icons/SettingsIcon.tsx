import Icon from "./Icon";
import { CertainIconProps } from "./models";

function SettingsIcon({ color }: Partial<CertainIconProps>) {
  return <Icon iconType="settings" iconProps={{ fill: color }}></Icon>;
}

export default SettingsIcon;
