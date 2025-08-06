// src/pages/EditEmployee.tsx

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "../api/axios";
import LayoutWrapper from "../components/LayoutWrapper";
import EmployeeForm from "../components/EmployeeForm";
import { Employee } from "../types";
import { useNavigate } from "react-router-dom";
import { useNotification } from "../context/NotificationContext";

const EditEmployee: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [initialValues, setInitialValues] = useState<Employee | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();
  const { showNotification } = useNotification();

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

  const handleSubmit = async (data: any) => {
    try {
      await axios.put(`/employees/${id}`, data);
      showNotification("Employee updated successfully!", "success");
      navigate("/");
    } catch (err) {
      console.error("Error updating employee:", err);
      showNotification("Failed to update employee", "error");
    }
  };

  return (
    <LayoutWrapper title="">
      {loading && <p>Loading...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}
      {!loading && initialValues && (
        <EmployeeForm mode="edit" initialValues={initialValues} onSubmit={handleSubmit} />
      )}
    </LayoutWrapper>
  );
};

export default EditEmployee;
