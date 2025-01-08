using BLL.DAL;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        public string UserName => Record.UserName;
        
        [Required(ErrorMessage = "Password is required")]
        public string Password => Record.Password;
        
        public string IsActive => Record.IsActive ? "Yes" : "No";
        public string Role => Record.Role?.Name;
    }
}
