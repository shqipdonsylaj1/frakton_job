using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProMaker.Arch.Helpers;

namespace SignInAPI
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
                var host = Dns.GetHostEntry("signin.io");
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(host.AddressList[0], 6004);
                    serverOptions.Listen(host.AddressList[0], 6005, listOpt =>
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
