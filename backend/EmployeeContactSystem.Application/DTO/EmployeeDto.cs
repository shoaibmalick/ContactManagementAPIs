using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    /// <summary>
    /// DTO for returning employee data in API responses.
    /// </summary>
    public class EmployeeDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name can't exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(255, ErrorMessage = "Email can't exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Phone number can't exceed 50 characters.")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Job title can't exceed 255 characters.")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company ID is required.")]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
