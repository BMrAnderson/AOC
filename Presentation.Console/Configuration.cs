using Core.Configuration;
using Microsoft.Extensions.Hosting;


namespace Presentation.Console
{
    public static class Configuration
    {
        public static IHost Build(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddCore();

            return builder.Build();
        }
    }
}
