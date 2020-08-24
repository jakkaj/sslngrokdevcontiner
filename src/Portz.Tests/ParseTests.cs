using System.Collections.Generic;
using NUnit.Framework;
using Portz.Services.Contract;
using Portz.Services.Model;
using Portz.Services.Services;
using Portz.Tests;

namespace portz.tests
{
    public class Tests : TestBase
    {
        private ConfigDocument _config;
        private IDocumentService _docService;
        [SetUp]
        public void Setup()
        {
            _docService = Resolve<IDocumentService>();

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
        public void SerialiseConfigTest()
        {

            var serialised = _docService.Serialise(_config);
            TestContext.Out.WriteLine(serialised);
        }

        [Test]
        public void SerialiseNgrokConfigTest()
        {

            var serialised = _docService.GetNgrok(_config);
            TestContext.Out.WriteLine(serialised);
        }
    }
}