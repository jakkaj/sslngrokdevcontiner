namespace Portz.Services.Services
{
    public interface IProcessService
    {
        void WriteScript(string scriptName, string script);
        void RunScript(string scriptName);
        int Run(string command, string arguments = null);
    }
}