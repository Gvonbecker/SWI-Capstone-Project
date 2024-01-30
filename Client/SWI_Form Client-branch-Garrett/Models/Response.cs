namespace SWI_Form_Client.Models
{
    public class Response
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = null;
    }
}
