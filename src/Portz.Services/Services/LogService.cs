using System;
using System.Collections.Generic;
using System.Text;

namespace Portz.Services.Services
{
    public class LogService : ILogService
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public void Log(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
