using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portz.Services.Model
{
    public class ConfigDocument
    {
        
        [Required]
        public IList<Tunnel> tunnels { get; set; }

    }

    public class Tunnel
    {
        [Required]
        public string addr { get; set; }

        [Required]
        public TunnelType tunnel_type { get; set; }
        
        [Required]
        public string subdomain { get; set; }
       
        [Required]
        public string proto { get; set; } = Protocols.HTTP;

        public string host_header { get; set; }
    }

    public enum TunnelType
    {
        Ngrok,
        Other
    }

    public static class Protocols
    {
        public static string TCP = "TCP";
        public static string UDP = "UDP";
        public static string HTTP =  "HTTP";
    }
}
