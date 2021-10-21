export const ingredientCategories = [
  { _id: "meat", name: "Meat" },
  { _id: "vegetable", name: "Vegetable" },
  { _id: "fruit", name: "Fruit" },
  { _id: "baked_good", name: "Baked Good" },
  { _id: "dairy", name: "Dairy" },
  { _id: "dry_food", name: "Dry Food" },
];

export function getIngredientCategories() {
  return ingredientCategories.filter((g) => g);
}
