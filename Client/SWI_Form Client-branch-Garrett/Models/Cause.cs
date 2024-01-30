using System.ComponentModel.DataAnnotations;

namespace SWI_Form_API.Models
{
    public class Cause
    {
        [Key]
        public int cause_id { get; set; }
        public string cause_name { get; set; }
    }
}
