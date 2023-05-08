namespace ApiMonoLegal.Models
{
    public class ClienteSettings : IClienteSettings
    {
        public string Collection { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }

 
    }


    public interface IClienteSettings
    {
        string Collection { get; set; }
        string Database { get; set; }
        string Server { get; set; }


    }
}
