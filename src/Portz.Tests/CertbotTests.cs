using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Portz.Services.Services;

namespace Portz.Tests
{
    public class CertbotTests : TestBase
    {
        [Test]
        public void BuildCertBot()
        {
            var certService = Resolve<ILetsEncryptService>();

            certService.Build(_config);
        }
    }
}
