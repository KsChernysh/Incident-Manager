using System.ComponentModel.DataAnnotations;

namespace Test_task.Profiles
{
    public class AccountProfile
    {
        [Required(ErrorMessage = "Field Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Field Key Value is required.")]
        public int keyValue { get; set; }
    }
}
