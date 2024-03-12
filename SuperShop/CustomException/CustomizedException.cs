namespace SuperShop.CustomException
{
    public class CustomizedException:Exception
    {
        public string message { get; set; } = string.Empty;
        public long statusCode { get; set; }
        public CustomizedException(string message,long statusCode) : base(message)
        {
            this.message = message;
            this.statusCode = statusCode;
        }
    }
}
