import React from "react";
import EmployeeForm from "../components/EmployeeForm";
import LayoutWrapper from "../components/LayoutWrapper";
import axios from "../api/axios";
import { useNavigate } from "react-router-dom";

const AddEmployee: React.FC = () => {
  const navigate = useNavigate();

  const handleAdd = async (data: any) => {
    try {
      await axios.post("/employees", data);
      alert("Employee added successfully!");
      navigate("/"); // or navigate("/employees") if you have a dedicated route
    } catch (error) {
      console.error("Error adding employee:", error);
      alert("Something went wrong while adding employee.");
    }
  };

  return (
    <LayoutWrapper title="Add Employee">
      <EmployeeForm mode="add" onSubmit={handleAdd} />
    </LayoutWrapper>
  );
};

export default AddEmployee;