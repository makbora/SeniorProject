import React, { Component } from "react";
import { Link } from "react-router-dom";
import Table from "./common/table";
import Like from "./common/like";

class RecipesTable extends Component {
  columns = [
    {
      path: "title",
      label: "Title",
      content: (recipe) => (
        <Link to={`/recipes/${recipe._id}`}>{recipe.title}</Link>
      ),
    },
    { path: "genre.name", label: "Type" },
    { path: "servings", label: "Servings" },
    { path: "rating", label: "Rating" },
    {
      key: "like",
      content: (recipe) => (
        <Like
          liked={recipe.liked}
          onClick={() => {
            this.props.onLike(recipe);
          }}
        />
      ),
    },
    {
      key: "delete",
      content: (recipe) => (
        <button
          onClick={() => this.props.onDelete(recipe)}
          className="btn btn-danger btn-sm"
        >
          Delete
        </button>
      ),
    },
  ];

  render() {
    const { recipes, onSort, sortColumn } = this.props;

    return (
      <Table
        columns={this.columns}
        data={recipes}
        sortColumn={sortColumn}
        onSort={onSort}
      />
    );
  }
}

export default RecipesTable;
