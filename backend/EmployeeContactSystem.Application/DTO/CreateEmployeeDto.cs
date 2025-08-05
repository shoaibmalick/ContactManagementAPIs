using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    /// <summary>
    /// DTO used to create or update employee data.
    /// </summary>
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name can't exceed 255 characters.")] //we have keep the length same as table length in DB
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(255, ErrorMessage = "Email can't exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Phone number can't exceed 50 characters.")]
        [RegularExpression(@"^\+?[0-9\s\-()]{7,50}$", ErrorMessage = "Invalid phone number format.")] //Avoid limiting to strictly digits
        public string Phone { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Job title can't exceed 255 characters.")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company ID is required.")]
        public int CompanyID { get; set; }
        public bool IsActive { get; set; }
    }
}
