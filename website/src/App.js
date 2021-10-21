import React, { Component } from "react";
import { Route, Redirect, Switch } from "react-router-dom";
import Recipes from "./components/recipes";
import RecipeForm from "./components/recipeForm";
import GroceryList from "./components/grocerylist";
import Home from "./components/home";
import NotFound from "./components/notFound";
import NavBar from "./components/navBar";
import LoginForm from "./components/loginForm";
import RegisterForm from "./components/registerForm";
import "./App.css";

class App extends Component {
  render() {
    return (
      <React.Fragment>
        <NavBar />
        <main className="container">
          <Switch>
            <Route path="/register" component={RegisterForm}></Route>
            <Route path="/login" component={LoginForm}></Route>
            <Route path="/recipes/:id" component={RecipeForm}></Route>
            <Route path="/recipes" component={Recipes}></Route>
            <Route path="/grocerylist" component={GroceryList}></Route>
            <Route path="/" component={Home}></Route>
            <Route path="/not-found" component={NotFound}></Route>
            <Redirect from="/" exact to="/home" />
            <Redirect to="/not-found" />
          </Switch>
        </main>
      </React.Fragment>
    );
  }
}

export default App;
