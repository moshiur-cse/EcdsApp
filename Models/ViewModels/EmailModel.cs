namespace EcdsApp.Models.ViewModels
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Msg { get; set; }
    }

    public class EmailConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Sender { get; set; }

    }
}
