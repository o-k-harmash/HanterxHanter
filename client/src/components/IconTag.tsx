import { capitalize } from "../utils/pipes";

// Импортируем компоненты для иконок
import PhotographyIcon from "./icons/PhotographyIcon";
import MusicIcon from "./icons/MusicIcon";
import StudyIcon from "./icons/StudyIcon";
import MoviesIcon from "./icons/MoviesIcon";
import InstagramIcon from "./icons/InstagramIcon";
import TravelingIcon from "./icons/TravelingIcon";

const icons: any = {
  photography: <PhotographyIcon />,
  music: <MusicIcon />,
  study: <StudyIcon />,
  movies: <MoviesIcon />,
  instagram: <InstagramIcon />,
  traveling: <TravelingIcon />,
};

type IconTagProps = {
  type: string;
};

function IconTag({ type }: IconTagProps) {
  return (
    <div className="tag tag_icon vertical-center">
      {icons[type] ?? null}
      <span>{capitalize(type)}</span>
    </div>
  );
}

export default IconTag;
