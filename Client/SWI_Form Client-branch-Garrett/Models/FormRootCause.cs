
using System.ComponentModel.DataAnnotations;

namespace SWI_Form_Client.Models
{
    public class FormRootCause
    {
        public int form_id { get; set; }
        public int root_cause_id { get; set; }
        [Key]
        public int form_root_cause_id { get; set; }
        public string? root_cause_description { get; set; }
    }
}
