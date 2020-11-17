using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CoinCapAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();
                var certSettings = config.GetSection("certificateSettings");
                string certPath = certSettings.GetValue<string>("CertPath");
                string certPass = certSettings.GetValue<string>("CertPass");
                var host = Dns.GetHostEntry("192.168.1.140");
                var certificate = new X509Certificate2(certPath, certPass);
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(host.AddressList[0], 6006);
                    serverOptions.Listen(host.AddressList[0], 6007, listOpt =>
                    {
                        listOpt.UseHttps(certificate);
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
            });
    }
}
