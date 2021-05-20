using Microsoft.Extensions.DependencyInjection;

namespace Module.Demo.Api.Processor.Demo.Extensions
{
    public static class ServiceExt
    {
        public static void AddProcessorDemoScope(this IServiceCollection services)
        {
            if (services != null)
            {
                services.AddTransient<Controller.Analytics.Processor>();
                services.AddTransient<Controller.Config.Processor>();
                services.AddTransient<Controller.Execute.Processor>();
            }
        }
    }
}
