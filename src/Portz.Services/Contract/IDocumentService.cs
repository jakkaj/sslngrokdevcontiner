using Portz.Services.Model;

namespace Portz.Services.Contract
{
    public interface IDocumentService
    {
        string Serialise(ConfigDocument doc);
        string GetNgrok(ConfigDocument doc);
    }
}