import React, { Component } from "react";
import RecipesTable from "./recipesTable";
import ListGroup from "./common/listGroup";
import Pagination from "./common/pagination";
import { getRecipes } from "../services/fakeRecipeService";
import { getGenres } from "../services/fakeGenreService";
import { paginate } from "../utils/paginate";
import _ from "lodash";
import { Link } from "react-router-dom";

class Recipes extends Component {
  state = {
    recipes: [],
    genre: [],
    currentPage: 1,
    pageSize: 4,
    sortColumn: { path: "title", order: "asc" },
  };

  componentDidMount() {
    const genres = [{ _id: "", name: "All Genres" }, ...getGenres()];

    this.setState({ recipes: getRecipes(), genres });
  }

  handleDelete = (recipe) => {
    const recipes = this.state.recipes.filter((m) => m._id !== recipe._id);
    this.setState({ recipes });
  };

  handleLike = (recipe) => {
    const recipes = [...this.state.recipes];
    const index = recipes.indexOf(recipe);
    recipes[index] = { ...recipes[index] };
    recipes[index].liked = !recipes[index].liked;
    this.setState({ recipes });
  };

  handlePageChange = (page) => {
    this.setState({ currentPage: page });
  };

  handleGenreSelect = (genre) => {
    this.setState({ selectedGenre: genre, currentPage: 1 });
  };

  handleSort = (sortColumn) => {
    this.setState({ sortColumn });
  };

  getPagedData = () => {
    const {
      pageSize,
      currentPage,
      sortColumn,
      selectedGenre,
      recipes: allRecipes,
    } = this.state;

    const filtered =
      selectedGenre && selectedGenre._id
        ? allRecipes.filter((m) => m.genre._id === selectedGenre._id)
        : allRecipes;

    const sorted = _.orderBy(filtered, [sortColumn.path], [sortColumn.order]);

    const recipes = paginate(sorted, currentPage, pageSize);

    return { totalCount: filtered.length, data: recipes };
  };

  render() {
    const { length: count } = this.state.recipes;
    const { pageSize, currentPage, sortColumn } = this.state;

    if (count === 0) return <p>There are no recipes in the database.</p>;

    const { totalCount, data: recipes } = this.getPagedData();

    return (
      <div className="row">
        <div className="col-3">
          <ListGroup
            items={this.state.genres}
            selectedItem={this.state.selectedGenre}
            onItemSelect={this.handleGenreSelect}
          />
        </div>
        <div className="col">
          <Link
            to="recipes/new"
            className="btn btn-primary"
            style={{ marginBottom: 20 }}
          >
            New Recipe
          </Link>
          <p>Showing {totalCount} recipes in the database.</p>
          <RecipesTable
            recipes={recipes}
            sortColumn={sortColumn}
            onLike={this.handleLike}
            onDelete={this.handleDelete}
            onSort={this.handleSort}
          />
          <Pagination
            itemsCount={totalCount}
            pageSize={pageSize}
            currentPage={currentPage}
            onPageChange={this.handlePageChange}
          />
        </div>
      </div>
    );
  }
}

export default Recipes;
