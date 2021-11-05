import * as ingredientCategoriesAPI from "./ingredientService";

const groceries = [
  {
    _id: "1",
    name: "Bread Crumbs",
    ingredientCategory: { _id: "dry_food", name: "Dry Food" },
    quantity: "16oz container",
    price: "$3.49",
    publishDate: "2018-01-03T19:04:28.809Z",
  },
  {
    _id: "2",
    name: "Yogurt",
    ingredientCategory: { _id: "dairy", name: "Dairy" },
    quantity: "8-pack",
    price: "$4.50",
  },
  {
    _id: "3",
    name: "Green Pepper",
    ingredientCategory: { _id: "vegetable", name: "Vegetable" },
    quantity: "8",
    price: "$1.50",
  },
  {
    _id: "4",
    name: "Rice",
    ingredientCategory: { _id: "dry_food", name: "Dry Food" },
    quantity: "1lb bag",
    price: "$5.75",
  },
  {
    _id: "5",
    name: "Milk",
    ingredientCategory: { _id: "dairy", name: "Dairy" },
    quantity: "1 gallon",
    price: "$3.50",
  },
];

export function getGroceries() {
  return groceries;
}

export function getGrocery(id) {
  return groceries.find((g) => g._id === id);
}

export function saveGrocery(grocery) {
  let groceryInDb = groceries.find((g) => g._id === grocery._id) || {};
  groceryInDb.name = grocery.name;
  groceryInDb.ingredientCategory =
    ingredientCategoriesAPI.ingredientCategories.find(
      (g) => g._id === grocery.ingredientCategoryId
    );
  groceryInDb.quantity = grocery.quantity;
  groceryInDb.price = grocery.price;

  if (!groceryInDb._id) {
    groceryInDb._id = Date.now().toString();
    groceries.push(groceryInDb);
  }

  return groceryInDb;
}

export function deleteGrocery(id) {
  let groceryInDb = groceries.find((m) => m._id === id);
  groceries.splice(groceries.indexOf(groceryInDb), 1);
  return groceryInDb;
}
