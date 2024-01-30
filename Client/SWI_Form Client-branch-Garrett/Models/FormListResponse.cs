namespace SWI_Form_Client.Models
{
    public class FormListResponse : Response
    {
        public List<FormModel> forms { get; set; }

        public int pageLength { get; set; }
        public int page { get; set; }
        public int results { get; set; }
    }
}
