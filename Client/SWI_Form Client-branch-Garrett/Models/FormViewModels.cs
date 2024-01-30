namespace SWI_Form_Client.Models
{
    public class FormViewModels
    {
        public Form Form { get; set; }
        public string date { get; set; }

        public FormCauseView equipment_failure_description { get; set; }

        public FormCauseView process_description { get; set; }
        public FormCauseView tools_description { get; set; }
        public FormCauseView housekeeping_description { get; set; }
        public FormCauseView instructions_description { get; set; }
        public FormCauseView management_description { get; set; }
        public FormCauseView training_description { get; set; }
        public FormCauseView other_description { get; set; }
        public FormRootCauseView Inattention { get; set; }
        public FormRootCauseView Procedure { get; set; }
        public FormRootCauseView Other { get; set; }

        public FormViewModels()
        {
            Form = new Form();
            equipment_failure_description = new FormCauseView();
            process_description = new FormCauseView();
            tools_description = new FormCauseView();
            housekeeping_description = new FormCauseView();
            instructions_description = new FormCauseView();
            management_description = new FormCauseView();
            training_description = new FormCauseView();
            other_description = new FormCauseView();
            Inattention = new FormRootCauseView();
            Procedure = new FormRootCauseView();
            Other = new FormRootCauseView();
        }
    }
}
