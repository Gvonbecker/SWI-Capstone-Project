using System.ComponentModel.DataAnnotations;

namespace SWI_Form_Client.Models
{
    public class Login_View_Model
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool loginReminder { get; set; } = false;
    }
}
