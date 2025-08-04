// src/components/LayoutWrapper.jsx
import { Container, Typography } from "@mui/material";

const LayoutWrapper = ({ title, children }) => (
  <Container
    maxWidth={false} // Allow full width
    disableGutters // Remove side padding
    sx={{ px: { xs: 2, sm: 4 }, py: 4 }} // Add responsive padding manually
  >
    {title && (
      <Typography variant="h4" gutterBottom>
        {title}
      </Typography>
    )}
    {children}
  </Container>
);

export default LayoutWrapper;
