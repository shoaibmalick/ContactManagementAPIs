import React, { useEffect, useState } from "react";
import { TextField, MenuItem } from "@mui/material";
import axios from "../api/axios";

interface Company {
  id: number;
  companyName: string;
}

interface CompanyDropdownProps {
  value: string | number;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onBlur?: (event: React.FocusEvent<HTMLInputElement>) => void;
  error?: boolean;
  helperText?: React.ReactNode;
  label?: string;
  name?: string;
}

const CompanyDropdown: React.FC<CompanyDropdownProps> = ({
  value,
  onChange,
  onBlur,
  error,
  helperText,
  label = "Company",
  name = "companyID",
}) => {
  const [companies, setCompanies] = useState<Company[]>([]);

  useEffect(() => {
    const fetchCompanies = async () => {
      try {
        const response = await axios.get<Company[]>("/companies");
        setCompanies(response.data);
      } catch (error) {
        console.error("Error fetching companies:", error);
      }
    };

    fetchCompanies();
  }, []);

  return (
    <TextField
      fullWidth
      select
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      onBlur={onBlur}
      error={error}
      helperText={helperText}
      margin="normal"
    >
      {companies.map((c) => (
        <MenuItem key={c.id} value={c.id}>
          {c.companyName}
        </MenuItem>
      ))}
    </TextField>
  );
};

export default CompanyDropdown;
