using LxDp.Application.Interfaces;
using LxDp.Infrastructure;
using LxDp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ILogService = LxDp.Application.Interfaces.ILogger;
var builder = WebApplication.CreateBuilder(args);

// Mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Service registrations
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IScriptService, ScriptService>();
builder.Services.AddScoped<ISshService, SshService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IPublishService, PublishService>();

//Db Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
