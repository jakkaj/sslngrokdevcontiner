using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Portz.Services.Contract;
using Portz.Services.Model;
using Portz.Services.Services;

namespace Portz.Tests
{
    public class TestBase
    {
        protected ConfigDocument _config;
        protected IDocumentService _docService;
        public SystemService SystemService { get; set; }


        
        public TestBase()
        {
            SystemService = new SystemService();
            SystemService.Boot();
        }

        [SetUp]
        public void Setup()
        {
            _docService = Resolve<IDocumentService>();

            _config = new ConfigDocument();
            _config.tunnels = new List<Tunnel>();
            _config.domain = "jakkaj.ngrok.com";
            _config.ngrok_subdomain = "jakkaj";
            var tunnel = new Tunnel()
            {
                tunnel_type = TunnelType.Ngrok,
                subdomain = "test",
                host_header = "localhost:9441",
                addr = "http://host.docker.internal:80", //or a port number if just TCP
                proto = Protocols.HTTP
            };

            _config.tunnels.Add(tunnel);
        }

        public T Resolve<T>()
        {
            return SystemService.Resolve<T>();
        }
    }
}
