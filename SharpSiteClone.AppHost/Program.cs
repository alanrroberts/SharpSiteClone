var builder = DistributedApplication.CreateBuilder(args);

// aspire code to add docker containers for postgres and pgadmin
var dbServer = builder.AddPostgres("database")
    .WithPgAdmin(c => c.WithLifetime(ContainerLifetime.Persistent))
    .WithLifetime(ContainerLifetime.Persistent);

// create the db in postgres
var db = dbServer.AddDatabase(SharpSiteClone.Data.Postgres.Constants.DBNAME);

var migrationSvc = builder.AddProject<Projects.SharpSiteClone_Data_Postgres_Migration>("migrationsvc")
    .WithReference(db)
    .WaitFor(dbServer);

builder.AddProject<Projects.SharpSiteClone_Web>("webfrontend")
    .WithReference(db)
    // .WaitFor(migrationSvc)
    .WithExternalHttpEndpoints();

builder.Build().Run();
