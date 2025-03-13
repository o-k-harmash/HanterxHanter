import React from "react";

import "./menu.css";

const MenuBlur = ({ onClick }: { onClick: () => void }) => {
  return <div onClick={onClick} className="menu__blur"></div>;
};

type MenuProps = {
  blurClick: () => void;
  children: React.ReactElement;
};

function Menu({ children, blurClick }: MenuProps) {
  return (
    <div className="menu">
      <MenuBlur onClick={blurClick} />
      <div className="menu__child">{children}</div>
    </div>
  );
}

export default Menu;
