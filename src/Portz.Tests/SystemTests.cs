using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Portz.Services.Model;

namespace Portz.Tests
{
    public class SystemTests : TestBase
    {
        [Test]
        public void TestConfig()
        {
            var config = Resolve<PortzSettings>();
            Assert.IsNotNull(config.NgrokToken); //in .env
        }
    }
}
