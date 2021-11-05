import React, { Component } from "react";
import { Link } from "react-router-dom";
import Table from "./common/table";
import Like from "./common/like";

class GroceriesTable extends Component {
  columns = [
    {
      path: "name",
      label: "Name",
      content: (grocery) => (
        <Link to={`/groceries/${grocery._id}`}>{grocery.name}</Link>
      ),
    },
    { path: "ingredientCategory.name", label: "Category" },
    { path: "quantity", label: "Quantity" },
    { path: "price", label: "Price" },
    {
      key: "delete",
      content: (grocery) => (
        <button
          onClick={() => this.props.onDelete(grocery)}
          className="btn btn-danger btn-sm"
        >
          Delete
        </button>
      ),
    },
  ];

  render() {
    const { groceries, onSort, sortColumn } = this.props;

    return (
      <Table
        columns={this.columns}
        data={groceries}
        sortColumn={sortColumn}
        onSort={onSort}
      />
    );
  }
}

export default GroceriesTable;
