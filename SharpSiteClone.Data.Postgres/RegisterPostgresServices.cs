using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpSiteClone.Abstractions;

namespace SharpSiteClone.Data.Postgres;

public class RegisterPostgresServices : IRegisterServices
{
    public IHostApplicationBuilder RegisterServices(IHostApplicationBuilder host)
    {
		
        host.Services.AddScoped<IPostRepository, PgPostRepository>(); // scoped = lifetime of single request
        host.AddNpgsqlDbContext<PgContext>(Constants.DBNAME); // aspire version of pg context so telemetry etc gets hooked up
		
        return host;
    }
}
public static class Constants
{
    // ReSharper disable once InconsistentNaming
    public const string DBNAME = "SharpSite";
}