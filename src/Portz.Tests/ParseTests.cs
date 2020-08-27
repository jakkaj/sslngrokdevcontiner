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