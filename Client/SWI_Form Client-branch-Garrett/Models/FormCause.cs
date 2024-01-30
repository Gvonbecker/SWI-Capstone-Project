
using System.ComponentModel.DataAnnotations;

namespace SWI_Form_Client.Models
{
    public class FormCause
    {

        public int form_id { get; set; }
        public int cause_id { get; set; }
        [Key]
        public int form_cause_id { get; set; }
        public string? form_cause_description { get; set; }


    }
}
