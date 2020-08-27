using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Portz.Services.Services
{
    public class ProcessService : IProcessService
    {
        private readonly ILogService _logService;

        public ProcessService(ILogService logService)
        {
            _logService = logService;
        }

        public void WriteScript(string scriptName, string script)
        {
            var scriptFolder = _getScriptFolder();

            var file = Path.Combine(scriptFolder.FullName, scriptName);
            
            File.WriteAllText(file, script);

            _logService.Log($"Wrote: {file}");

            if (file.EndsWith(".sh"))
            {
                _addExecute(scriptName);
            }
        }

        void _addExecute(string scriptName)
        {
            var scriptFolder = _getScriptFolder();

            var file = Path.Combine(scriptFolder.FullName, scriptName);

            Run("chmod", $"+x '{file}'");
        }

        public void RunScript(string scriptName)
        {
            var scriptFile = new FileInfo(Path.Combine(_getScriptFolder().FullName, scriptName));

            if (!scriptFile.Exists)
            {
                _logService.Log($"Could not find script file: {scriptFile.FullName}");
            }
            else
            {
                _logService.Log($"Running script: {scriptFile.FullName}");
                Run(scriptFile.FullName);
            }
        }

        private DirectoryInfo _getScriptFolder()
        {
            var scriptFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "scripts"));

            if (!scriptFolder.Exists)
            {
                scriptFolder.Create();
            }

            return scriptFolder;
        }

        public int Run(string command, string arguments = null)
        {
            try
            {
                using (var p = new Process())
                {
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;

                    p.StartInfo.FileName = command;
                    p.StartInfo.Arguments = arguments;
                    p.Start();
                    var result = p.StandardOutput.ReadToEnd();
                    result += "\n" + p.StandardError.ReadToEnd();
                    _logService.Log(result);
                    return p.ExitCode;
                }
            }
            catch (Exception ex)
            {
                _logService.Log(ex);
            }

            return -1;
        }
    }
}
