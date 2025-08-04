// src/pages/EditEmployee.tsx

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "../api/axios";
import LayoutWrapper from "../components/LayoutWrapper";
import EmployeeForm from "../components/EmployeeForm";
import { Employee } from "../types";

const EditEmployee: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [initialValues, setInitialValues] = useState<Employee | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    const fetchEmployee = async () => {
      try {
        const response = await axios.get<Employee>(`/employees/${id}`);
        setInitialValues(response.data);
      } catch (err) {
        console.error("Error fetching employee:", err);
        setError("Failed to load employee data.");
      } finally {
        setLoading(false);
      }
    };

    if (id) {
      fetchEmployee();
    }
  }, [id]);

  return (
    <LayoutWrapper title="Edit Employee">
      {loading && <p>Loading...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}
      {!loading && initialValues && (
        <EmployeeForm mode="edit" initialValues={initialValues} />
      )}
    </LayoutWrapper>
  );
};

export default EditEmployee;
