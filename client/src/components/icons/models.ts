export type IconType =
  | "photography"
  | "music"
  | "study"
  | "movies"
  | "instagram"
  | "traveling"
  | "like"
  | "dislike"
  | "superlike"
  | "settings"
  | "pencil"
  | "rocket"
  | "bell"
  | "plus"
  | "person"
  | "home";

export type SvgProps = {
  fill: string;
  stroke: string;
};

export type IconProps = {
  iconType: IconType;
  iconProps: Partial<SvgProps>;
};

export type CertainIconProps = {
  color: string;
};
