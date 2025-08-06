import React, { useEffect, useState } from "react";
import axios from "../api/axios";
import { Link } from "react-router-dom";
import {
  Button,
  Chip,
  Typography,
  MenuItem,
  TextField,
  Pagination,
  Box,
} from "@mui/material";
import LayoutWrapper from "../components/LayoutWrapper";
import DataTable from "../components/DataTable";
import { Employee, PagedResult } from "../types";
import SearchBar from "../components/SearchBar";
import PaginationControls from "../components/PaginationControls";
import { SelectChangeEvent } from "@mui/material";
import CompanyDropdown from "../components/CompanyDropdown";

const EmployeeList: React.FC = () => {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [pageSize, setPageSize] = useState<number>(10);
  const [totalCount, setTotalCount] = useState<number>(0);
  const [statusFilter, setStatusFilter] = useState<string>("");
  const [companyFilter, setCompanyFilter] = useState<string>("");
  const handlePageSizeChange = (event: SelectChangeEvent<number>) => {
    const newSize = parseInt(event.target.value as string); // Always comes as string
    setPageSize(newSize);
    setCurrentPage(1); // Reset to page 1
  };

  const fetchEmployees = async () => {
    try {
      const response = await axios.get<PagedResult<Employee>>("/employees", {
        params: {
          search: searchTerm,
          page: currentPage,
          pageSize: pageSize,
          status: statusFilter !== "All" ? statusFilter : undefined,
          companyID: companyFilter !== "" ? companyFilter : undefined,
        },
      });

      setEmployees(response.data.items);
      setTotalCount(response.data.totalCount);
    } catch (err) {
      console.error("Error fetching employees:", err);
      setError("Failed to load employee data.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    console.log("PageSize changed to:", pageSize);
    fetchEmployees();
  }, [currentPage, searchTerm, statusFilter, companyFilter, pageSize]);

  return (
    <LayoutWrapper title="Employee List">
      {loading && <Typography>Loading...</Typography>}
      {error && <Typography color="error">{error}</Typography>}

      <Box
        display="flex"
        alignItems="center"
        justifyContent="space-between"
        sx={{ mb: 2 }}
      >
        {/* Left side: Search and Status */}
        <Box display="flex" alignItems="center" gap={2}>
          <SearchBar
            searchTerm={searchTerm}
            onSearchChange={(value) => {
              setSearchTerm(value);
              setCurrentPage(1);
            }}
            placeholder="Search by name, title, phone or email"
          />

          <TextField
            select
            label="Status"
            value={statusFilter}
            onChange={(e) => {
              setStatusFilter(e.target.value);
              setCurrentPage(1);
            }}
            SelectProps={{
              renderValue: (selected) => {
                if (selected === "All") return "All";
                if (selected === "true") return "Active";
                if (selected === "false") return "Inactive";
                return "";
              },
            }}
            sx={{
              width: 200,
              height: "56px",
              "& .MuiInputBase-root": {
                height: "56px",
              },
            }}
          >
            <MenuItem value="All">All</MenuItem>
            <MenuItem value="true">Active</MenuItem>
            <MenuItem value="false">Inactive</MenuItem>
          </TextField>

          <Box
            sx={{
              width: 200,
              height: "56px",
              "& .MuiInputBase-root": {
                height: "56px",
              },
              "& .MuiFormControl-root": {
                margin: 0,
              },
            }}
          >
            <CompanyDropdown
              value={companyFilter}
              onChange={(e) => {
                setCompanyFilter(e.target.value);
                setCurrentPage(1);
              }}
              label="Company"
              name="companyID"
            />
          </Box>
        </Box>

        {/* Right side: Add Employee button */}
        <Link to="/add" style={{ textDecoration: "none" }}>
          <Button
            variant="contained"
            color="primary"
            sx={{
              whiteSpace: "nowrap",
              height: "56px", // Match height
            }}
          >
            Add Employee
          </Button>
        </Link>
      </Box>

      <Box sx={{ overflowX: "auto", width: "100%" }}>
        <DataTable
          data={employees}
          getRowKey={(emp) => emp.id}
          columns={[
            { key: "id", label: "ID" },
            { key: "name", label: "Name" },
            { key: "jobTitle", label: "Job Title" },
            { key: "email", label: "Email" },
            { key: "phone", label: "Phone" },
            {
              key: "isActive",
              label: "Status",
              render: (emp) => (
                <Chip
                  label={emp.isActive ? "Active" : "Inactive"}
                  color={emp.isActive ? "success" : "default"}
                  size="small"
                />
              ),
            },
            { key: "companyName", label: "Company" },
            {
              key: "actions",
              label: "Actions",
              render: (emp) => (
                <Link to={`/edit/${emp.id}`} style={{ textDecoration: "none" }}>
                  <Button variant="outlined" size="small">
                    Edit
                  </Button>
                </Link>
              ),
            },
          ]}
        />
      </Box>

      <PaginationControls
        currentPage={currentPage}
        totalCount={totalCount}
        pageSize={pageSize}
        onPageChange={setCurrentPage}
        onPageSizeChange={handlePageSizeChange}
      />

    </LayoutWrapper>
  );
};

export default EmployeeList;
