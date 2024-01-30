using System.ComponentModel.DataAnnotations;

namespace SWI_Form_Client.Models
{
    public class Login
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Key]
        public int user_id { get; set; }
    }
}
