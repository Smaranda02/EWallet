namespace EWallet.Code
{
    public class ErrorsResult
    {
        public List<ErrorResultItem> Errors { get; set; } 
    }
    public class ErrorResultItem
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
