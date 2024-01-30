
using SWI_Form_Client.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWI_Form_Client.Models
{
    public class Form
    {
        [Key]
        public int form_id { get; set; }
        public int log_num { get; set; }
        public string? staff_first { get; set; }
        public string? staff_last { get; set; }
        public string? staff_job_title { get; set; }
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly event_date { get; set; }
        [Required]
        public string event_location { get; set; }
        public bool hourly_check { get; set; }
        public bool production_stopped { get; set; }
        public string? date_last_trained { get; set; }
        public bool procedures_followed { get; set; }
        public string? procedures_description { get; set; }
        public string? event_description { get; set; }
        public int? shop_order_num { get; set; }
        public int? billing_unit { get; set; }
        public double? billing_cost_per { get; set; }
        public int? supervisor_ct { get; set; }
        public int? supervisor_time { get; set; }
        public int? employee_ct { get; set; }
        public int? employee_time { get; set; }
        public int? inspected_qty { get; set; }
        public int? reworked_qty { get; set; }
        public bool additional_rules_needed { get; set; }
        public string? additional_rules_description { get; set; }
        public bool quality_control_sig { get; set; }
        public bool production_manager_sig { get; set; }
        public bool management_notified { get; set; }
    }
}
