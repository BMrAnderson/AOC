using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Console
{
    public static class Services
    {
        public static T Get<T>(IHost host) where T : class
        {
            using var scope = host.Services.CreateScope();
            
            var provider = scope.ServiceProvider;
            
            var service = provider.GetService<T>();
          
            return service!;
        }
    }
}
