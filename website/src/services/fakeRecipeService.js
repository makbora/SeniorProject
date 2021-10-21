import * as genresAPI from "./fakeGenreService";

const recipes = [
  {
    _id: "1",
    title: "Buffalo Wings",
    genre: { _id: "appetizer", name: "Appetizer" },
    servings: 6,
    ingredients: "Party-Style Wings, Buffalo Sauce",
    rating: 2.5,
    publishDate: "2018-01-03T19:04:28.809Z",
  },
  {
    _id: "2",
    title: "Chocolate Cake",
    genre: { _id: "dessert", name: "Dessert" },
    servings: 5,
    ingredients:
      "Chocolate, Flour, Eggs, Sugar, Coccoa Powder, Baking Powder, Milk",
    rating: 2.5,
  },
  {
    _id: "3",
    title: "Roasted Chicken",
    genre: { _id: "entree", name: "Entree" },
    servings: 8,
    ingredients: "Chicken, Salt, Pepper, Thyme",
    rating: 4.3,
  },
  {
    _id: "4",
    title: "Chips & Dip",
    genre: { _id: "appetizer", name: "Appetizer" },
    servings: 7,
    ingredients: "Chips, Advocado, Salsa",
    rating: 3.5,
  },
  {
    _id: "5",
    title: "Cheesecake",
    genre: { _id: "dessert", name: "Dessert" },
    servings: 7,
    ingredients: "Cheese, Eggs, Sugar",
    rating: 3.2,
  },
  {
    _id: "6",
    title: "Grilled Steak",
    genre: { _id: "entree", name: "Entree" },
    servings: 7,
    ingredients: "Steak, Salt, Pepper",
    rating: 5,
  },
];

export function getRecipes() {
  return recipes;
}

export function getRecipe(id) {
  return recipes.find((m) => m._id === id);
}

export function saveRecipe(recipe) {
  let recipeInDb = recipes.find((m) => m._id === recipe._id) || {};
  recipeInDb.title = recipe.title;
  recipeInDb.genre = genresAPI.genres.find((g) => g._id === recipe.genreId);
  recipeInDb.servings = recipe.servings;
  recipeInDb.ingredients = recipe.ingredients;
  recipeInDb.rating = recipe.rating;

  if (!recipeInDb._id) {
    recipeInDb._id = Date.now().toString();
    recipes.push(recipeInDb);
  }

  return recipeInDb;
}

export function deleteRecipe(id) {
  let recipeInDb = recipes.find((m) => m._id === id);
  recipes.splice(recipes.indexOf(recipeInDb), 1);
  return recipeInDb;
}
