using SWI_Form_Client.Models;
using System.Globalization;

namespace SWI_Form_Client.Utility
{
    public static class FormHelper
    {
        private const string format = "yyyy-MM-dd";

        /// <summary>
        /// Converts a FormModel recieved from the API to a FormViewModels used by the client.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static FormViewModels ParseToView(FormModel form)
        {
            FormViewModels model = new FormViewModels();

            #region Form
            model.Form.form_id = form.form_id;
            model.Form.log_num = form.log_num;
            model.Form.staff_first = form.staff_first;
            model.Form.staff_last = form.staff_last;
            model.Form.staff_job_title = form.staff_job_title;
            model.date = form.event_date.ToString(format, CultureInfo.InvariantCulture);
            model.Form.event_location = form.event_location;
            model.Form.hourly_check = form.hourly_check;
            model.Form.production_stopped = form.production_stopped;
            model.Form.date_last_trained = form.date_last_trained;
            model.Form.procedures_followed = form.procedures_followed;
            model.Form.procedures_description = form.procedures_description;
            model.Form.event_description = form.event_description;
            model.Form.shop_order_num = form.shop_order_num;
            model.Form.billing_unit = form.billing_unit;
            model.Form.billing_cost_per = form.billing_cost_per;
            model.Form.supervisor_ct = form.supervisor_ct;
            model.Form.supervisor_time = form.supervisor_time;
            model.Form.employee_ct = form.employee_ct;
            model.Form.employee_time = form.employee_time;
            model.Form.inspected_qty = form.inspected_qty;
            model.Form.reworked_qty = form.reworked_qty;
            model.Form.additional_rules_needed = form.additional_rules_needed;
            model.Form.additional_rules_description = form.additional_rules_description;
            model.Form.quality_control_sig = form.quality_control_sig;
            model.Form.production_manager_sig = form.production_manager_sig;
            model.Form.management_notified = form.management_notified;

            model.equipment_failure_description.check = false;
            model.process_description.check = false;
            model.tools_description.check = false;
            model.housekeeping_description.check = false;
            model.instructions_description.check = false;
            model.management_description.check = false;
            model.training_description.check = false;
            model.other_description.check = false;

            model.Inattention.check = false;
            model.Procedure.check = false;
            model.Other.check = false;
            #endregion

            #region FormCause
            if (form.form_cause != null)
            {
                foreach (FormCause x in form.form_cause)
                {
                    if (x.cause_id == 1)
                    {
                        model.equipment_failure_description.check = true;
                        model.equipment_failure_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 2)
                    {
                        model.process_description.check = true;
                        model.process_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 3)
                    {
                        model.tools_description.check = true;
                        model.tools_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 4)
                    {
                        model.housekeeping_description.check = true;
                        model.housekeeping_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 5)
                    {
                        model.instructions_description.check = true;
                        model.instructions_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 6)
                    {
                        model.management_description.check = true;
                        model.management_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 7)
                    {
                        model.training_description.check = true;
                        model.training_description.form_cause_description = x.form_cause_description;
                    }
                    else if (x.cause_id == 8)
                    {
                        model.other_description.check = true;
                        model.other_description.form_cause_description = x.form_cause_description;
                    }
                }
            }
            #endregion

            #region FormRootCause
            if (form.form_root_cause != null)
            {
                foreach (FormRootCause x in form.form_root_cause)
                {
                    if (x.root_cause_id == 1)
                    {
                        model.Inattention.check = true;
                        model.Inattention.root_cause_description = x.root_cause_description;
                    }
                    else if (x.root_cause_id == 2)
                    {
                        model.Procedure.check = true;
                        model.Procedure.root_cause_description = x.root_cause_description;
                    }
                    else if (x.root_cause_id == 3)
                    {
                        model.Other.check = true;
                        model.Other.root_cause_description = x.root_cause_description;
                    }
                }
            }
            #endregion

            return model;
        }

        /// <summary>
        /// Converts a FormViewModels used by the client to a FormModel used by the API.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static FormModel ParseToServer(FormViewModels form)
        {
            FormModel model = new FormModel();
            List<FormCause> causeList = new List<FormCause>();
            List<FormRootCause> rootCauseList = new List<FormRootCause>();

            #region Form
            model.form_id = form.Form.form_id;
            model.log_num = form.Form.log_num;
            model.staff_first = form.Form.staff_first;
            model.staff_last = form.Form.staff_last;
            model.staff_job_title = form.Form.staff_job_title;
            model.event_date = DateOnly.ParseExact(form.date, format, CultureInfo.InvariantCulture);
            model.event_location = form.Form.event_location;
            model.hourly_check = form.Form.hourly_check;
            model.production_stopped = form.Form.production_stopped;
            model.date_last_trained = form.Form.date_last_trained;
            model.procedures_followed = form.Form.procedures_followed;
            model.procedures_description = form.Form.procedures_description;
            model.event_description = form.Form.event_description;
            model.shop_order_num = form.Form.shop_order_num;
            model.billing_unit = form.Form.billing_unit;
            model.billing_cost_per = form.Form.billing_cost_per;
            model.supervisor_ct = form.Form.supervisor_ct;
            model.supervisor_time = form.Form.supervisor_time;
            model.employee_ct = form.Form.employee_ct;
            model.employee_time = form.Form.employee_time;
            model.inspected_qty = form.Form.inspected_qty;
            model.reworked_qty = form.Form.reworked_qty;
            model.additional_rules_needed = form.Form.additional_rules_needed;
            model.additional_rules_description = form.Form.additional_rules_description;
            model.quality_control_sig = form.Form.quality_control_sig;
            model.production_manager_sig = form.Form.production_manager_sig;
            model.management_notified = form.Form.management_notified;
            model.form_cause = causeList;
            model.form_root_cause = rootCauseList;
            #endregion

            #region FormCause
            if (form.equipment_failure_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 1,
                    form_cause_description = form.equipment_failure_description.form_cause_description
                });
            }
            if (form.process_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 2,
                    form_cause_description = form.process_description.form_cause_description
                });
            }
            if (form.tools_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 3,
                    form_cause_description = form.tools_description.form_cause_description
                });
            }
            if (form.housekeeping_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 4,
                    form_cause_description = form.housekeeping_description.form_cause_description
                });
            }
            if (form.instructions_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 5,
                    form_cause_description = form.instructions_description.form_cause_description
                });
            }
            if (form.management_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 6,
                    form_cause_description = form.management_description.form_cause_description
                });
            }
            if (form.training_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 7,
                    form_cause_description = form.training_description.form_cause_description
                });
            }
            if (form.other_description.check)
            {
                causeList.Add(new FormCause
                {
                    form_id = model.form_id,
                    cause_id = 8,
                    form_cause_description = form.other_description.form_cause_description
                });
            }
            #endregion

            #region FormRootCause
            if (form.Inattention.check)
            {
                rootCauseList.Add(new FormRootCause
                {
                    form_id = model.form_id,
                    root_cause_id = 1,
                    root_cause_description = form.Inattention.root_cause_description
                });
            }
            if (form.Procedure.check)
            {
                rootCauseList.Add(new FormRootCause
                {
                    form_id = model.form_id,
                    root_cause_id = 2,
                    root_cause_description = form.Procedure.root_cause_description
                });
            }
            if (form.Other.check)
            {
                rootCauseList.Add(new FormRootCause
                {
                    form_id = model.form_id,
                    root_cause_id = 3,
                    root_cause_description = form.Other.root_cause_description
                });
            }
            #endregion

            return model;
        }
    }
}
