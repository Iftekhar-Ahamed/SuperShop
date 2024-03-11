namespace SuperShop.Model
{
    public class MessageHelperModel
    {
        public dynamic? data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
