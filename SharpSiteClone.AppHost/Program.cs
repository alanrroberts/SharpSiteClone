var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SharpSiteClone_Web>("webfrontend")
    .WithExternalHttpEndpoints();

builder.Build().Run();
