import React from "react";

import { IconProps } from "./models";
import { icons } from "./consts";

function Icon({ iconType, iconProps }: IconProps) {
  return React.cloneElement(icons[iconType], {
    stroke: iconProps.stroke || "none",
    fill: iconProps.fill || "none",
  });
}

export default Icon;
