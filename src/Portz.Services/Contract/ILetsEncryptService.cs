using Portz.Services.Model;

namespace Portz.Services.Services
{
    public interface ILetsEncryptService
    {
        void Build(ConfigDocument doc);
    }
}