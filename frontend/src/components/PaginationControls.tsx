// src/components/PaginationControls.tsx

import React from "react";
import {
  Box,
  Pagination,
  Select,
  MenuItem,
  Typography,
  SelectChangeEvent,
} from "@mui/material";

interface PaginationControlsProps {
  currentPage: number;
  totalCount: number;
  pageSize: number;
  onPageChange: (page: number) => void;
  onPageSizeChange: (event: SelectChangeEvent<number>) => void;
}

const PaginationControls: React.FC<PaginationControlsProps> = ({
  currentPage,
  totalCount,
  pageSize,
  onPageChange,
  onPageSizeChange,
}) => {
  const totalPages = Math.ceil(totalCount / pageSize);

  if (totalCount === 0) return null;

  return (
    <Box
      display="flex"
      justifyContent="space-between"
      alignItems="center"
      flexWrap="wrap"
      mt={2}
      gap={2}
    >
      <Box display="flex" alignItems="center">
        <Typography variant="body2" mr={1}>
          Rows per page:
        </Typography>
        <Select<number>
          value={pageSize}
          onChange={onPageSizeChange}
          size="small"
        >
          {[10, 25, 50].map((size) => (
            <MenuItem key={size} value={size}>
              {size}
            </MenuItem>
          ))}
        </Select>
      </Box>

      <Pagination
        count={totalPages}
        page={currentPage}
        onChange={(_, page) => onPageChange(page)}
        color="primary"
      />
    </Box>
  );
};

export default PaginationControls;
