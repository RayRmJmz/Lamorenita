import { Link, Outlet } from "react-router-dom";
import { ROUTES } from "../constants/routes";

export default function Header() {
  return (
    <>
      <nav className="navbar navbar-expand-lg bg-body-tertiary">
        <div className="container-fluid">
          <Link to={ROUTES.ROOT} className="navbar-brand">
            La Morenita
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarTogglerDemo02"
            aria-controls="navbarTogglerDemo02"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarTogglerDemo02">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <Link to={ROUTES.CLIENTES} className="nav-link">
                  Productos
                </Link>
              </li>
              <li className="nav-item">
                <Link to={ROUTES.CLIENTES} className="nav-link">
                  Clientes
                </Link>
              </li>
              <li className="nav-item">
                <Link to={ROUTES.CLIENTES} className="nav-link">
                  Productos
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>
      <Outlet />
    </>
  );
}
