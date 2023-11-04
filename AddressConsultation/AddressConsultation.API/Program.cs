using AddressConsultation.API.Endpoints;
using AddressConsultation.API.Swagger;
using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using AddressConsultation.Application.Service;
using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Infra.Cache.Implements;
using AddressConsultation.Infra.Security;
using AddressConsultation.Infra.Security.Interface;
using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using AddressConsultation.Persistence.SqlServer.Context;
using AddressConsultation.Persistence.SqlServer.DataAccess;
using AddressConsultation.Persistence.SqlServer.Infra;
using AddressConsultation.Infra.Cache;
using AddressConsultation.Infra.Cache.Redis;
using AddressConsultation.API.Midwares.BlackList;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis").Value;
});

builder.Services.AddScoped<IDapperDbContext, DapperDbContext>();
builder.Services.AddScoped(typeof(IAddressRepository<AddressModel>), typeof(AddressRepository));
builder.Services.AddScoped(typeof(IUserRepository<UserModel>), typeof(UserRepository));
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
builder.Services.AddSingleton<ICacheManager, RedisCacheService>();
builder.Services.AddSingleton<IAddressCache<AddressDTO>, AddressCache<AddressDTO>>();
builder.Services.AddSingleton<ITokenCache<TokenDTO>, TokenCache<TokenDTO>>();
builder.Services.AddSingleton<IBlackListCache<TokenDTO>, BlackListCache<TokenDTO>>();

builder.Services.AddSwaggerConfiguration();

var jwtSettings = new JwtSettings();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{app.Environment.ApplicationName} v1"));
}

app.UseHttpsRedirection();

app.UseMiddleware<BlacklistCheckMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.ConfigureAddressEndpoints();
app.ConfigureUserEndpoints();
app.ConfigureAuthenticationEndpoints();

app.Run();


