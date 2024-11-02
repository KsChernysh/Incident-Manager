using DataLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_task.Profiles
{
    public class IncidentProfile
    {
        
        public string accountName { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "First name must start with an uppercase letter.")]
        public string contactFirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "Last name must start with an uppercase letter.")]
        public string contactLastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string contactEmail { get; set; }
        [Required(ErrorMessage = "Incident description is required.")]
        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string incidentDescription { get; set; }

    }
}
