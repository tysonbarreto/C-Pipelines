import { NavLink } from "react-router-dom";
import { Menu, X } from "lucide-react";
import { useState } from "react";

const Navbar = () => {
  const [open, setOpen] = useState(false);

  const linkClass = ({ isActive }) =>
    isActive ? "font-bold text-blue-600" : "text-gray-700 hover:text-blue-600";

  return (
    <header>
      <div className="max-w-7xl mx-auto px-4 h-16 flex items-center justify-between">
        <h1 className="text-xl font-bold"> Person CRUD </h1>
        <nav className="hidden md:flex gap-8">
          <NavLink to="/" className={linkClass}>
            Home
          </NavLink>
          <NavLink to="/about" className={linkClass}>
            About
          </NavLink>
          <NavLink to="/person" className={linkClass}>
            Person
          </NavLink>
        </nav>
        {/* Mobile Button */}
        <button className="md:hidden" onClick={() => setOpen(!open)}>
          {open ? <X size={24} /> : <Menu size={24} />}
        </button>
      </div>
      {/* Mobile dropdown */}
      {open && (
        <div className="md:hidden bg-gray-50 border-t p-4 flex flex-col gap-4">
          <NavLink to="/" className={linkClass} onClick={() => setOpen(false)}>
            Home
          </NavLink>
          <NavLink
            to="/about"
            className={linkClass}
            onClick={() => setOpen(false)}
          >
            About
          </NavLink>
          <NavLink
            to="/person"
            className={linkClass}
            onClick={() => setOpen(false)}
          >
            Person
          </NavLink>
        </div>
      )}
    </header>
  );
};

export default Navbar;
