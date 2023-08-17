using AutoMapper;

using BizStream.Migrations;
using BizStream.Migrations.Extensions;
using BizStream.Migrations.Mappings;
using BizStream.Migrations.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((hostingContext, configuration) =>
     configuration.AddUserSecrets<MigratorService>()
);

builder.ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();
        services.Configure<ExportOptions>(hostContext.Configuration.GetSection("ConnectionStrings"));
        services.AddMigrationServices();
        services.AddAutoMapper(typeof(Profile));
        services.AddAutoMapper(typeof(FolderMappingProfile));
        services.AddSingleton(provider => provider);
        services.AddHostedService<MigratorService>();
    }
);

var build = builder.Build();
build.Run();
