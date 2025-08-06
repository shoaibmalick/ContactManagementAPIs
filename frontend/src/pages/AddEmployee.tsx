import React from "react";
import EmployeeForm from "../components/EmployeeForm";
import LayoutWrapper from "../components/LayoutWrapper";
import axios from "../api/axios";
import { useNavigate } from "react-router-dom";
import { useNotification } from "../context/NotificationContext";

const AddEmployee: React.FC = () => {
  const navigate = useNavigate();
  const { showNotification } = useNotification();

  const handleAdd = async (data: any) => {
    try {
      await axios.post("/employees", data);
      showNotification("Employee added successfully!", "success");
      navigate("/"); // or navigate("/employees") if you have a dedicated route
    } catch (error) {
      console.error("Error adding employee:", error);
      showNotification("Failed to add employee. Please try again.", "error");
    }
  };

  return (
    <LayoutWrapper title="">
      <EmployeeForm mode="add" onSubmit={handleAdd} />
    </LayoutWrapper>
  );
};

export default AddEmployee;