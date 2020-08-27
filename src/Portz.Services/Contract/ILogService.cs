using System;

namespace Portz.Services.Services
{
    public interface ILogService
    {
        void Log(string text);
        void Log(Exception ex);
    }
}