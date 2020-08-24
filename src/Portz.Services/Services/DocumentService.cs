using System;
using System.Collections.Generic;
using System.Text;
using Portz.Services.Contract;
using Portz.Services.Model;
using YamlDotNet.Serialization;

namespace Portz.Services.Services
{
    public class DocumentService : IDocumentService
    {
        public string Serialise(ConfigDocument doc)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(doc);
            return yaml;
        }

        public string GetNgrok(ConfigDocument doc)
        {
            var ngrok = new NgrokDocument() {tunnels = new Dictionary<string, Tunnel>()};

            for (var i = 0; i < doc.tunnels.Count; i++)
            {
                ngrok.tunnels.Add($"tunnel_{i}", doc.tunnels[i]);
            }
            
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(ngrok);
            return yaml;
        }
    }
}
