using System.Net;

namespace CSGO.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Response { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IPAddress Ip { get; set; }
    }
}