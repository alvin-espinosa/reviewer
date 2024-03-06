namespace AppSettings.Models
{
    public class Auth0Settings
    {
        public string Domain { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public KeyValue? KeyValue { get; set; }
    }

    public class KeyValue
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
