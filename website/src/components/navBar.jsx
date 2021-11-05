import React from "react";
import { Link, NavLink } from "react-router-dom";
import logo from "../images/logo.jpg";

const NavBar = () => {
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <Link
        className="navbar-brand"
        to="/"
        style={{
          paddingLeft: "15px",
        }}
      >
        <img src={logo} alt="Logo" />
      </Link>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNavAltMarkup"
        aria-controls="navbarNavAltMarkup"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon" />
      </button>
      <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
        <div className="navbar-nav">
          <NavLink className="nav-item nav-link" to="/">
            Home
          </NavLink>
          <NavLink className="nav-item nav-link" to="/recipes">
            Recipes
          </NavLink>
          <NavLink className="nav-item nav-link" to="/grocerylist">
            Grocery List
          </NavLink>
          <NavLink className="nav-item nav-link" to="/login">
            Login
          </NavLink>
          <NavLink className="nav-item nav-link" to="/register">
            Register
          </NavLink>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
