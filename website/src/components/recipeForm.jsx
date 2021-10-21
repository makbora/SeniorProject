import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getRecipe, saveRecipe } from "../services/fakeRecipeService";
import { getGenres } from "../services/fakeGenreService";

class RecipeForm extends Form {
  state = {
    data: {
      title: "",
      genreId: "",
      servings: "",
      ingredients: "",
      rating: "",
    },
    genres: [],
    errors: {},
  };

  schema = {
    _id: Joi.string(),
    title: Joi.string().required().label("Title"),
    genreId: Joi.string().required().label("Type"),
    servings: Joi.string()
      .required()
      .min(0)
      .max(100)
      .label("Number of Servings"),
    ingredients: Joi.string().required().label("Ingredients"),
    rating: Joi.number().required().min(1).max(5).label("Rating"),
  };

  componentDidMount() {
    const genres = getGenres();
    this.setState({ genres });

    const recipeId = this.props.match.params.id;
    if (recipeId === "new") return;

    const recipe = getRecipe(recipeId);
    if (!recipe) return this.props.history.replace("/not-found");

    this.setState({ data: this.mapToViewModel(recipe) });
  }

  mapToViewModel(recipe) {
    return {
      _id: recipe._id,
      title: recipe.title,
      genreId: recipe.genre._id,
      servings: recipe.servings,
      ingredients: recipe.ingredients,
      rating: recipe.rating,
    };
  }

  doSubmit = () => {
    saveRecipe(this.state.data);

    this.props.history.push("/recipes");
  };

  render() {
    return (
      <div>
        <h1>Recipe Form</h1>
        <form onSubmit={this.handleSubmit}>
          {this.renderInput("title", "Title")}
          {this.renderSelect("genreId", "Type", this.state.genres)}
          {this.renderInput("servings", "Servings", "number")}
          {this.renderInput("ingredients", "Ingredients")}
          {this.renderInput("rating", "Rating")}
          <p />
          {this.renderButton("Save")}
        </form>
      </div>
    );
  }
}

export default RecipeForm;
