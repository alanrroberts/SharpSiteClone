using SharpSiteClone.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

// aspire code to add docker containers for postgres and pgadmin
var (db, migrationSvc) = builder.AddPostgresServices(SharpSiteClone.Data.Postgres.Constants.DBNAME);

builder.AddProject<Projects.SharpSiteClone_Web>("webfrontend")
    .WithReference(db)
    // .WaitFor(migrationSvc)
    .WithExternalHttpEndpoints();

builder.Build().Run();