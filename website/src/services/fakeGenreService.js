export const genres = [
  { _id: "entree", name: "Entree" },
  { _id: "appetizer", name: "Appetizer" },
  { _id: "dessert", name: "Dessert" },
];

export function getGenres() {
  return genres.filter((g) => g);
}
