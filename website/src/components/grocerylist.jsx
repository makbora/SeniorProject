import React, { Component } from "react";
import GroceriesTable from "./groceriesTable";
import ListGroup from "./common/listGroup";
import Pagination from "./common/pagination";
import { getGroceries } from "../services/fakeGroceryService";
import { getIngredientCategories } from "../services/ingredientService";
import { paginate } from "../utils/paginate";
import _ from "lodash";
import { Link } from "react-router-dom";

class GroceryList extends Component {
  state = {
    groceries: [],
    ingredientCategories: [],
    currentPage: 1,
    pageSize: 10,
    sortColumn: { path: "title", order: "asc" },
  };

  componentDidMount() {
    const ingredientCategories = [
      { _id: "", name: "All Categories" },
      ...getIngredientCategories(),
    ];

    this.setState({ groceries: getGroceries(), ingredientCategories });
  }

  handleDelete = (grocery) => {
    const groceries = this.state.groceries.filter((m) => m._id !== grocery._id);
    this.setState({ groceries });
  };

  handlePageChange = (page) => {
    this.setState({ currentPage: page });
  };

  handleCategorySelect = (category) => {
    this.setState({ selectedCategory: category, currentPage: 1 });
  };

  handleSort = (sortColumn) => {
    this.setState({ sortColumn });
  };

  getPagedData = () => {
    const {
      pageSize,
      currentPage,
      sortColumn,
      selectedCategory,
      groceries: allGroceries,
    } = this.state;

    const filtered =
      selectedCategory && selectedCategory._id
        ? allGroceries.filter((m) => m.category._id === selectedCategory._id)
        : allGroceries;

    const sorted = _.orderBy(filtered, [sortColumn.path], [sortColumn.order]);

    const groceries = paginate(sorted, currentPage, pageSize);

    return { totalCount: filtered.length, data: groceries };
  };

  render() {
    const { length: count } = this.state.groceries;
    const { pageSize, currentPage, sortColumn } = this.state;

    //if (count === 0) return <p>There are no groceries on your list!</p>;

    const { totalCount, data: groceries } = this.getPagedData();

    return (
      <div className="row">
        {/* <div className="col-3">
          <ListGroup
            items={this.state.ingredientCategories}
            selectedItem={this.state.selectedCategory}
            onItemSelect={this.handleGenreSelect}
          />
        </div> */}
        <div className="col">
          <Link
            to="grocerylist"
            className="btn btn-primary"
            style={{ marginBottom: 20 }}
          >
            Add Item
          </Link>
          <p>Your list contains {totalCount} items.</p>
          <GroceriesTable
            groceries={groceries}
            sortColumn={sortColumn}
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

export default GroceryList;
