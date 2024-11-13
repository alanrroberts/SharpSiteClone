using Microsoft.Extensions.Hosting;

namespace SharpSiteClone.Abstractions;

public interface IRegisterServices
{
    IHostApplicationBuilder RegisterServices(IHostApplicationBuilder services);
}