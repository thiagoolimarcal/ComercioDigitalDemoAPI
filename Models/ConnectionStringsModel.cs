
namespace ComercioDigitalDemoAPI.Models
{
    public class ConnectionStringsModel
    {
        public DefaultConnection DefaultConnection { get; set; }
    }

    public class DefaultConnection
    {
        public string ConnectionString { get; set; }
    }
}