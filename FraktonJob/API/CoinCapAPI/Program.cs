using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProMaker.Arch.Helpers;
using System.IO;
using System.Net;

namespace CoinCapAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                HostConfigExtensions.CertPath = context.Configuration["CertPath"];
                HostConfigExtensions.CertPassword = context.Configuration["CertPassword"];
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var host = Dns.GetHostEntry("coincap.io");
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(host.AddressList[0], 6006);
                    serverOptions.Listen(host.AddressList[0], 6007, listOpt =>
                    {
                        listOpt.UseHttps(HostConfigExtensions.CertPath, HostConfigExtensions.CertPassword);
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>();
            });
    }
}
