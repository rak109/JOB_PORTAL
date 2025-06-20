namespace JOB_PORTAL.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public int? num;
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
