using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    public class UpdateEmployeeDto
    {
        [MaxLength(255, ErrorMessage = "Name can't exceed 255 characters.")] //we have keep the length same as table length in DB
        public string? Name { get; set; }

        [MaxLength(255, ErrorMessage = "Job title can't exceed 255 characters.")]
        public string? JobTitle { get; set; }

        [MaxLength(50, ErrorMessage = "Phone number can't exceed 50 characters.")]
        [RegularExpression(@"^\+?[0-9\s\-()]{7,50}$", ErrorMessage = "Invalid phone number format.")] //Avoid limiting to strictly digits
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(255, ErrorMessage = "Email can't exceed 255 characters.")]
        public string? Email { get; set; }
        public int? CompanyID { get; set; }

        public bool? IsActive { get; set; }
    }
}
