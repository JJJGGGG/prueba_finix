import { Link, NavLink } from "react-router-dom";

export default function TopNavBar() {
    return (
        <div className="app-padding py-4">
            <div className="flex gap-4">
                <NavLink to="/" className="nav-link">Inicio</NavLink>
            </div>
        </div>
    );
}