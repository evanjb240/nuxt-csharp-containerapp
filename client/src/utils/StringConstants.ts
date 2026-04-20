export const months = ["All", "January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];

export const years = [...Array(new Date().getFullYear() - 2021 + 1)].map((_, i) => 2021 + i).reverse();
