import IconButton, { Types } from "../components/IconButton";
import { Link, useLocation } from "react-router";

const navbarData: { type: Types; link: string }[] = [
  {
    type: "home",
    link: "/",
  },
  {
    type: "person",
    link: "/account",
  },
];

type NavbarProps = {
  type: "outlined";
};

function Navbar({ type }: Partial<NavbarProps>) {
  const location = useLocation();

  return (
    <nav
      className={`navbar fixed${type === "outlined" ? " navbar_outlined" : ""}`}
    >
      <div className="medium-container">
        {navbarData.map((data, i) => (
          <Link key={i} className="navbar__link" to={data.link}>
            <IconButton
              type={data.type}
              style={"style-4"}
              isActive={
                i === navbarData.findIndex((d) => d.link === location.pathname)
              }
            ></IconButton>
          </Link>
        ))}
      </div>
    </nav>
  );
}

export default Navbar;
