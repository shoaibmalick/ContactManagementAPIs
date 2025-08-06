// src/components/EmployeeForm.tsx
import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  FormControl,
  Select,
  InputLabel,
  MenuItem,
  TextField,
  Typography,
} from "@mui/material";
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "../api/axios";
import { EmployeePayload, Employee, company } from "../types";
import CompanyDropdown from "./CompanyDropdown";
import { validateEmail } from "../utils/emailValidation";
import { useNotification } from "../context/NotificationContext";
//import InputMask from "react-input-mask-next";

interface EmployeeFormProps {
  mode: "add" | "edit";
  initialValues?: Employee;
  onSubmit: (data: EmployeePayload) => void;
  isSubmitting?: boolean;
}

interface Company {
  id: number;
  companyName: string;
}

const EmployeeForm: React.FC<EmployeeFormProps> = ({
  mode,
  initialValues,
  onSubmit,
  isSubmitting = false,

}) => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const { showNotification } = useNotification();

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

  const formik = useFormik({
    initialValues: initialValues || {
      name: "",
      email: "",
      phone: "",
      jobTitle: "",
      companyID: "",
      isActive: true,
    },
    enableReinitialize: true,
    validationSchema: Yup.object({
      name: Yup.string().required("Name is required"),
      email: Yup.string()
        .email("Invalid email address")
        .required("Email is required"),
      //phone: Yup.string().required("Phone is required"), //As per Db Schema Phone and Jobtitle are not required
      //jobTitle: Yup.string().required("Job Title is required"),
      companyID: Yup.string().required("Company is required"),
    }),
    //onSubmit: (values) => onSubmit(values),
    onSubmit: async (values) => {
      try {
        const validationResult = await validateEmail(values.email);

        if (validationResult) {
          const emailStatus = validationResult.deliverability;

          console.log("Email deliverability status:", emailStatus);

          if (!validationResult || validationResult.deliverability === "UNDELIVERABLE") {
            showNotification("Invalid email", "error");
            return; // Prevents form submission
          }

          const payloadWithStatus = {
            ...values,
            emailStatus,
          };

          onSubmit(payloadWithStatus);
        } else {
          //alert("Failed to validate email. Please try again later.");
          showNotification("Failed to validate email. Please try again later.", "error");
        }
      } catch (error) {
        console.error("Email validation error:", error);
        //alert("Unexpected error during email validation.");
        showNotification("Unexpected error during email validation.", "error");
      }
    },
  });

  return (
    <Box component="form" onSubmit={formik.handleSubmit} sx={{ maxWidth: 600, mx: "auto" }}>
      <Typography variant="h5" gutterBottom>
        {mode === "add" ? "Add New Employee" : "Edit Employee"}
      </Typography>

      <TextField
        fullWidth
        label="Name"
        name="name"
        value={formik.values.name}
        onChange={formik.handleChange}
        onBlur={formik.handleBlur}
        error={formik.touched.name && Boolean(formik.errors.name)}
        //helperText={formik.touched.name && formik.errors.name}
        helperText={formik.touched.name && formik.errors.name ? formik.errors.name as string : ""}
        margin="normal"
      />

      <TextField
        fullWidth
        label="Email"
        name="email"
        value={formik.values.email}
        onChange={formik.handleChange}
        onBlur={formik.handleBlur}
        error={formik.touched.email && Boolean(formik.errors.email)}
        //helperText={formik.touched.email && formik.errors.email}
        helperText={formik.touched.email && formik.errors.email ? formik.errors.email as string : ""}
        margin="normal"
      />

      <TextField
        fullWidth
        label="Phone"
        name="phone"
        placeholder="___-___-____"
        value={formik.values.phone}
        onChange={(e) => {
          const formatted = e.target.value
            .replace(/\D/g, "")                            // Remove non-numeric
            .slice(0, 10)                                  // Limit to 10 digits
            .replace(/(\d{3})(\d{3})(\d{0,4})/, (_, a, b, c) =>
              [a, b, c].filter(Boolean).join("-")          // Format as XXX-XXX-XXXX
            );

          formik.setFieldValue("phone", formatted);
        }}
        onBlur={formik.handleBlur}
        error={formik.touched.phone && Boolean(formik.errors.phone)}
        helperText={
          formik.touched.phone && formik.errors.phone
            ? formik.errors.phone as string
            : ""
        }
        margin="normal"
      />

      <TextField
        fullWidth
        label="Job Title"
        name="jobTitle"
        value={formik.values.jobTitle}
        onChange={formik.handleChange}
        onBlur={formik.handleBlur}
        error={formik.touched.jobTitle && Boolean(formik.errors.jobTitle)}
        //helperText={formik.touched.jobTitle && formik.errors.jobTitle}
        helperText={formik.touched.jobTitle && formik.errors.jobTitle ? formik.errors.jobTitle as string : ""}
        margin="normal"
      />
      <TextField
        fullWidth
        select
        label="Status"
        name="isActive"
        value={formik.values.isActive.toString()} // convert boolean to string
        onChange={(e) => formik.setFieldValue("isActive", e.target.value === "true")}
        margin="normal"
      >
        <MenuItem value="true">Active</MenuItem>
        <MenuItem value="false">Inactive</MenuItem>
      </TextField>

      <CompanyDropdown
        value={formik.values.companyID}
        onChange={formik.handleChange}
        onBlur={formik.handleBlur}
        error={formik.touched.companyID && Boolean(formik.errors.companyID)}
        //helperText={formik.touched.companyID && formik.errors.companyID}
        helperText={formik.touched.companyID && formik.errors.companyID ? formik.errors.companyID as string : ""}
      />

      <Box display="flex" justifyContent="flex-end" mt={2}>
        <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
          {mode === "add" ? "Add Employee" : "Update Employee"}
        </Button>
      </Box>
    </Box>
  );
};

export default EmployeeForm;
