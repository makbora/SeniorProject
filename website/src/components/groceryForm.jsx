import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getGrocery, saveGrocery } from "../services/fakeGroceryService";
import { getIngredientCategories } from "../services/ingredientService";

class GroceryForm extends Form {
  state = {
    data: {
      name: "",
      categoryId: "",
      quantity: "",
      price: "",
    },
    categories: [],
    errors: {},
  };

  schema = {
    _id: Joi.string(),
    name: Joi.string().required().label("Name"),
    categoryId: Joi.string().required().label("Category"),
    quantity: Joi.string().required().min(0).max(100).label("Quantity"),
    price: Joi.number().required().min(0.01).max(50).label("Price"),
  };

  componentDidMount() {
    const categories = getIngredientCategories();
    this.setState({ categories });

    const groceryId = this.props.match.params.id;
    if (groceryId === "new") return;

    const grocery = getGrocery(groceryId);
    if (!grocery) return this.props.history.replace("/not-found");

    this.setState({ data: this.mapToViewModel(grocery) });
  }

  mapToViewModel(grocery) {
    return {
      _id: grocery._id,
      title: grocery.title,
      categoryId: grocery.category._id,
      quantity: grocery.quantity,
      price: grocery.price,
    };
  }

  doSubmit = () => {
    saveGrocery(this.state.data);

    this.props.history.push("/grocerylist");
  };

  render() {
    return (
      <div>
        <h1>Grocery Form</h1>
        <form onSubmit={this.handleSubmit}>
          {this.renderInput("title", "Title")}
          {this.renderSelect("category", "Type", this.state.categories)}
          {this.renderInput("quantity", "Quantity", "number")}
          {this.renderInput("price", "Price")}
          {this.renderButton("Save")}
        </form>
      </div>
    );
  }
}

export default GroceryForm;
