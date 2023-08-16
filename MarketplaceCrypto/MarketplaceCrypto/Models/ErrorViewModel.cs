namespace MarketplaceCrypto.Models
{
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string RequestId { get; set; }
    }
}