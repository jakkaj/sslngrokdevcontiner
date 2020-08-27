using System;
using System.Collections.Generic;
using System.Text;
using Portz.Services.Model;

namespace Portz.Services.Services
{
    public class LetsEncryptService : ILetsEncryptService
    {
        private readonly PortzSettings _settings;
        private readonly IProcessService _processService;

        public LetsEncryptService(PortzSettings settings, IProcessService processService)
        {
            _settings = settings;
            _processService = processService;
        }
        public void Build(ConfigDocument doc)
        {
            //build the config.ini

            var ini = $"email={_settings.Email}\ndomain={doc.domain}\n";

            _processService.WriteScript("certbot_config.ini", ini);

            var certbot =
                "killall ngrok\n" +
                $"ngrok http -host-header=\"{doc.domain}\" -subdomain=\"{doc.ngrok_subdomain}\" 80 > /dev/null &\n" +
                "#wait for ngrok\n" +
                "sleep 5s\n" +
                "certbot certonly --config ./scripts/certbot_config.ini --standalone --preferred-challenges http\n" +
                "cp -r /etc/letsencrypt ./\n" +
                "openssl pkcs12 -export \\\n" +
                "    -out certificate.pfx \\\n" +
                $"    -inkey ./letsencrypt/archive/{doc.domain}/privkey1.pem \\\n" +
                $"    -in ./letsencrypt/archive/{doc.domain}/cert1.pem \\\n" +
                $"    -certfile ./letsencrypt/archive/{doc.domain}/chain1.pem \\\n" +
                $"    -passout pass:{_settings.CertPassword}";

            _processService.WriteScript("certbot.sh", certbot);
           
        }
    }
}
