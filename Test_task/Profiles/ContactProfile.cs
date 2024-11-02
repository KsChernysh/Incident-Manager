using System.ComponentModel.DataAnnotations;

namespace Test_task.Profiles
{
    public class ContactProfile
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "First name must start with an uppercase letter.")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "Last name must start with an uppercase letter.")]
        public string lastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
         public string email { get; set; }

    }
}
