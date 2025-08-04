// src/components/DataTable.tsx
import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Box,
} from "@mui/material";

export interface DataTableColumn<T> {
  key: keyof T | "actions";
  label: string;
  render?: (item: T) => React.ReactNode;
}

interface DataTableProps<T> {
  data: T[];
  columns: DataTableColumn<T>[];
  getRowKey?: (item: T) => string | number;
}

const DataTable = <T,>({
  data = [],
  columns = [],
  getRowKey,
}: DataTableProps<T>) => {
  return (
    <Box sx={{ overflowX: "auto", width: "100%" }}>
      <TableContainer component={Paper} elevation={3}>
        <Table
          sx={{ tableLayout: "auto", width: "100%" }}
          aria-label="data table"
        >
          <TableHead>
            <TableRow>
              {columns.map((col) => (
                <TableCell key={`head-${col.key as string}`}>
                  <strong>{col.label}</strong>
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {data.length === 0 ? (
              <TableRow>
                <TableCell colSpan={columns.length} align="center">
                  No records found.
                </TableCell>
              </TableRow>
            ) : (
              data.map((item) => {
                const rowKey = getRowKey ? getRowKey(item) : (item as any).id;
                return (
                  <TableRow
                    key={`row-${rowKey}`}
                    sx={{
                      "&:nth-of-type(odd)": { backgroundColor: "#f9f9f9" },
                      "&:hover": { backgroundColor: "#e3f2fd" },
                    }}
                  >
                    {columns.map((col) => (
                      <TableCell key={`cell-${rowKey}-${col.key as string}`}>
                        {col.render ? col.render(item) : (item as any)[col.key]}
                      </TableCell>
                    ))}
                  </TableRow>
                );
              })
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};

export default DataTable;
