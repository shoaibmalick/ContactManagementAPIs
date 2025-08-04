import React from "react";
import EmployeeForm from "../components/EmployeeForm";
import LayoutWrapper from "../components/LayoutWrapper";

const AddEmployee: React.FC = () => {
  return (
    <LayoutWrapper title="Add Employee">
      <EmployeeForm mode="add" />
    </LayoutWrapper>
  );
};

export default AddEmployee;