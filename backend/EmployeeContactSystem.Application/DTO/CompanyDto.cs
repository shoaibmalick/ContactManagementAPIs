using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.DTO
{
    /// <summary>
    /// DTO for returning company information.
    /// </summary>
    public class CompanyDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(255, ErrorMessage = "Company name can't exceed 255 characters.")]  //we have keep the length same as table length in DB
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Domain is required.")]
        [MaxLength(255, ErrorMessage = "Domain can't exceed 255 characters.")]
        public string Domain { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Industry can't exceed 255 characters.")]
        public string Industry { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Website can't exceed 255 characters.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string Website { get; set; } = string.Empty;
    }
}
