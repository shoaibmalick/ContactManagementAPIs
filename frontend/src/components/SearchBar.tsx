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
      sx={{
        width: 800, // Wider for typing
        height: "56px", // Match MUI default height
        mb: 0, // Remove bottom margin
        "& .MuiInputBase-root": {
          height: "56px", // Ensures consistent input height
        },
      }}
    />
  );
};

export default SearchBar;
