import LikeIcon from "./icons/LikeIcon"; // Импортируем компоненты иконок
import DislikeIcon from "./icons/DislikeIcon";
import SettingsIcon from "./icons/SettingsIcon";
import PencilIcon from "./icons/PencilIcon";
import SuperlikeIcon from "./icons/SuperLikeIcon";
import PlusIcon from "./icons/PlusIcon";
import PersonIcon from "./icons/PersonIcon";
import HomeIcon from "./icons/HomeIcon";

const icons = {
  like: <LikeIcon color="#FF50A2" />,
  dislike: <DislikeIcon color="#ff4a5c" />,
  superlike: <SuperlikeIcon color="#4cb1ff" />,
  settings: <SettingsIcon color="#697089" />,
  pencil: <PencilIcon color="#697089" />,
  plus: <PlusIcon color="#697089" />,
  person: <PersonIcon color="#ff4a5c" />,
  home: <HomeIcon color="#4cb1ff" />,
};

export type Types =
  | "like"
  | "dislike"
  | "superlike"
  | "settings"
  | "pencil"
  | "plus"
  | "person"
  | "home";

type IconButtonProps = {
  type: Types;
  style: "style-0" | "style-1" | "style-2" | "style-3" | "style-4";
  onClick?: (args: any) => void;
  isActive?: boolean;
};

function IconButton({ type, style, onClick, isActive }: IconButtonProps) {
  return (
    <div
      onClick={onClick}
      className={`icon-button icon-button_${style} vertical-center ${
        isActive ? "active" : ""
      }`}
    >
      {icons[type]}
    </div>
  );
}

export default IconButton;
