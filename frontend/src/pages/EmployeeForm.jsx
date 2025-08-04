import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
  Typography,
  Box,
  Alert,
} from "@mui/material";
import axios from "../utils/axios";

const EmployeeList = () => {
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const res = await axios.get("/employees");
        setEmployees(res.data);
      } catch (err) {
        setError("Failed to load employee data.");
      } finally {
        setLoading(false);
      }
    };
    fetchEmployees();
  }, []);

  if (loading)
    return (
      <Box display="flex" justifyContent="center" mt={5}>
        <CircularProgress />
      </Box>
    );

  if (error)
    return (
      <Box mt={5}>
        <Alert severity="error">{error}</Alert>
      </Box>
    );

  return (
    <Box mt={5}>
      <Typography variant="h5" gutterBottom>
        Employee List
      </Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>
                <b>Name</b>
              </TableCell>
              <TableCell>
                <b>Phone</b>
              </TableCell>
              <TableCell>
                <b>Job Title</b>
              </TableCell>
              <TableCell>
                <b>Company</b>
              </TableCell>
              <TableCell>
                <b>Status</b>
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {employees.map((emp) => (
              <TableRow key={emp.id}>
                <TableCell>{emp.name}</TableCell>
                <TableCell>{emp.phone}</TableCell>
                <TableCell>{emp.jobTitle}</TableCell>
                <TableCell>{emp.company?.name || "N/A"}</TableCell>
                <TableCell>
                  {emp.isActive ? "✅ Active" : "❌ Inactive"}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};

export default EmployeeList;
