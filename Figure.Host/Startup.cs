using Figure.Business;
using Figure.Contracts;
using Figure.Contracts.Db;
using Figure.Host.Middleware;
using Figure.SqliteDb;
using Figure.SqliteDb.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FigureApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers( o => {
                o.Filters.Add<ApiExceptionFilterAttribute>();
            } );
            services.AddDb();
            services.AddSingleton<IFigureRepository, FigureRepository>();
            services.AddTransient<IFigureService, FigureService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
