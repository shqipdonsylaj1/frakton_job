using CoinCapAPI.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProMaker.Arch.Helpers;
using ProMaker.Arch.ITokenServices;
using ProMaker.Arch.Middleware;
using ProMaker.Arch.TokenServices;

namespace CoinCapAPI
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<AssetsExtensions>();
            JWTExtension.SetupAuth(services, Config);
            services.AddMvc(x =>
            {
                x.SslPort = 6007;
                x.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddAntiforgery(x =>
            {
                x.Cookie.Name = "_af";
                x.Cookie.HttpOnly = true;
                x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                x.HeaderName = "X-XSRF-TOKEN";
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
