using System.ComponentModel.DataAnnotations;

namespace SWI_Form_API.Models
{
    public class RootCause
    {
        [Key]
        public int root_cause_id { get; set; }
        public string root_cause_name { get; set; }
    }
}
