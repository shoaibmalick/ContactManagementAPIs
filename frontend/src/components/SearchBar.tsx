import React from "react";
import { TextField } from "@mui/material";

interface SearchBarProps {
  searchTerm: string;
  onSearchChange: (value: string) => void;
  placeholder?: string;
}

const SearchBar: React.FC<SearchBarProps> = ({
  searchTerm,
  onSearchChange,
  placeholder,
}) => {
  return (
    <TextField
      label={placeholder || "Search"}
      variant="outlined"
      value={searchTerm}
      onChange={(e) => onSearchChange(e.target.value)}
      fullWidth
      sx={{ mb: 2 }}
    />
  );
};

export default SearchBar;
