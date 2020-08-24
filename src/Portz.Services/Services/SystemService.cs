using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Portz.Services.Contract;
using Portz.Services.Model;

namespace Portz.Services.Services
{
    public class SystemService
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IServiceCollection ServiceCollection { get; set; }

        private PortzSettings _settings;

        public void Boot()
        {
            DotNetEnv.Env.Load();

            var builder = new ConfigurationBuilder();

            // tell the builder to look for the appsettings.json file
            builder.AddEnvironmentVariables();

            var configuration = builder.Build();


            ServiceCollection = new ServiceCollection();


            ServiceCollection.Configure<PortzSettings>(configuration.GetSection(nameof(PortzSettings)));
            ServiceCollection.AddSingleton(_ => _.GetRequiredService<IOptions<PortzSettings>>().Value);
            ServiceCollection.AddSingleton<IDocumentService, DocumentService>();

            ServiceProvider = ServiceCollection.BuildServiceProvider();

            _settings = Resolve<IOptions<PortzSettings>>().Value;
            
        }

        public T Resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
