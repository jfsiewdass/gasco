using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Common.Helpers
{
    public static class ServiceProviderHelper
    {
        private static IServiceProvider? _provider;

        public static void Initialize(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException();
        }

        public static T? GetService<T>()
        {
            return _provider!.GetService<T>();
        }

        public static T? GetScopedService<T>() where T : class
        {
            var scoped = _provider!.CreateScope();
            return scoped.ServiceProvider.GetRequiredService<T>();
        }
    }
}
