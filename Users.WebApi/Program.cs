using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System.Reflection;
using Users.Domain;
using Users.Infrastructure;
using Users.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(b =>
{
    string connStr = builder.Configuration.GetConnectionString("Default");
    b.UseSqlServer(connStr);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost",
    options.InstanceName = "UsersDemo_";
});
builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add<UnitOfWorkFilter>();
});
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();
builder.Services.AddScoped<IUserDomainRepository, UserDomainRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
