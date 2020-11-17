using Infrastructure.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProMaker.Arch.Helpers;
using ProMaker.Arch.ITokenServices;
using ProMaker.Arch.Middleware;
using ProMaker.Arch.TokenServices;

namespace SignInAPI
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlServer(_config.GetConnectionString("FraktonIdentityConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            JWTExtension.SetupAuth(services, _config);
            services.AddIdentityServices();
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
