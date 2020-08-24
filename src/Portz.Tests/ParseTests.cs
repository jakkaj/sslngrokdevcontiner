using System.Collections.Generic;
using NUnit.Framework;
using Portz.Services.Model;
using Portz.Services.Services;

namespace portz.tests
{
    public class Tests
    {
        private ConfigDocument _config;
        private DocumentService _docService;
        [SetUp]
        public void Setup()
        {
            _docService = new DocumentService();

            _config = new ConfigDocument();
            _config.tunnels = new List<Tunnel>();

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

        [Test]
        public void SerialiseTest()
        {

            var serialised = _docService.Serialise(_config);
            TestContext.Out.WriteLine(serialised);
        }
    }
}