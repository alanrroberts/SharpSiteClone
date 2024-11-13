namespace SharpSiteClone.AppHost;

public static class PostgresExtensions
{
    public static (IResourceBuilder<PostgresDatabaseResource> db, IResourceBuilder<ProjectResource> migrationSvc) AddPostgresServices(
        this IDistributedApplicationBuilder builder,
        string dbName)
    {
        var dbServer = builder.AddPostgres("database")
            .WithDataVolume($"{dbName}-data")
            .WithPgAdmin(c => c.WithLifetime(ContainerLifetime.Persistent))
            .WithLifetime(ContainerLifetime.Persistent);

        // create the db in postgres
        var db = dbServer.AddDatabase(dbName);

        var migrationSvc = builder.AddProject<Projects.SharpSiteClone_Data_Postgres_Migration>($"{dbName}-migrationsvc")
            .WithReference(db)
            .WaitFor(dbServer);

        return (db, migrationSvc);
    }
}