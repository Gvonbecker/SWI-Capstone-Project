using System.Text.Json.Serialization;

namespace SWI_Form_Client.Models
{
    public class FormModel : Form
    {
        public List<FormCause> form_cause { get; set; }
        public List<FormRootCause> form_root_cause { get; set; }

        [JsonConstructor]
        public FormModel() { }

        public FormModel(Form form, List<FormCause> cause, List<FormRootCause> rootcause)
        {
            form_id = form.form_id;
            log_num = form.log_num;
            staff_first = form.staff_first;
            staff_last = form.staff_last;
            staff_job_title = form.staff_job_title;
            event_date = form.event_date;
            event_location = form.event_location;
            hourly_check = form.hourly_check;
            production_stopped = form.production_stopped;
            date_last_trained = form.date_last_trained;
            procedures_followed = form.procedures_followed;
            procedures_description = form.procedures_description;
            event_description = form.event_description;
            shop_order_num = form.shop_order_num;
            billing_unit = form.billing_unit;
            billing_cost_per = form.billing_cost_per;
            supervisor_ct = form.supervisor_ct;
            supervisor_time = form.supervisor_time;
            employee_ct = form.employee_ct;
            employee_time = form.employee_time;
            inspected_qty = form.inspected_qty;
            reworked_qty = form.reworked_qty;
            additional_rules_needed = form.additional_rules_needed;
            additional_rules_description = form.additional_rules_description;
            quality_control_sig = form.quality_control_sig;
            production_manager_sig = form.production_manager_sig;
            management_notified = form.management_notified;
            form_cause = cause;
            form_root_cause = rootcause;
        }
    }
}
