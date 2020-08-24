using System;
using System.Collections.Generic;
using System.Text;
using Portz.Services.Services;

namespace Portz.Tests
{
    public class TestBase
    {
        public SystemService SystemService { get; set; }

        public TestBase()
        {
            SystemService = new SystemService();
            SystemService.Boot();
        }

        public T Resolve<T>()
        {
            return SystemService.Resolve<T>();
        }
    }
}
