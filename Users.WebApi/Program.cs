using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain;
using Users.Infrastructure;
using Users.WebApi;

// see https://www.cnblogs.com/xieweikang/p/16714565.html
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(b =>
{
    string connStr = builder.Configuration.GetConnectionString("WindowsSql");
    b.UseSqlServer(connStr);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
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

// app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
