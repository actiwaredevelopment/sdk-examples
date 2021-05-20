using Development.SDK.Logging.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;

namespace Module.Demo.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
                Log.Information(AppContext.BaseDirectory);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string baseDirectory = string.Empty;

            if (args != null &&
                args.Length > 0)
            {
                baseDirectory = args.First().TrimEnd('\\');
            }

            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    if (string.IsNullOrWhiteSpace(baseDirectory) == false)
                    {
                        config.SetBasePath(baseDirectory);
                        config.AddJsonFile(System.IO.Path.Combine(baseDirectory.TrimEnd('\\'), "appsettings.json"), true, true);
                    }
                    else
                    {
                        config.AddJsonFile("appsettings.json", true, true);
                    }

                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options => { })
                              .UseStartup<Startup>()
                              .UseSerilog((context, loggerConfiguration) =>
                              {
                                  loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                                  loggerConfiguration.SetLoggerSettings("module-demo", "/module/demo", "1.0.0");
                              });


                });

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                builder = builder.UseWindowsService();
            }

            if (string.IsNullOrWhiteSpace(baseDirectory) == false)
            {
                builder.UseContentRoot(baseDirectory);
            }

            return builder;
        }
    }
}
