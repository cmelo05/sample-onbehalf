using NewRepositoryAPI.Models;
using NewRepositoryAPI.Repositories;
using NewRepositoryAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<WorkflowDatabaseSettings>(builder.Configuration.GetSection("WorkflowDatabase"));
        builder.Services.Configure<GithubActionsSettings>(builder.Configuration.GetSection("GithubActions"));
        builder.Services.AddSingleton<IBackendService, GithubActionsBackendService>();
        builder.Services.AddSingleton<IRepository, MongoRepository>();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}