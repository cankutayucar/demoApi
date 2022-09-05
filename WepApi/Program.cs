using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb => { cb.RegisterModule(new AutofacBusinessModule()); });

// http isteklerine izin mekanizması
// dışarıdan erişileceği siteyi belirlemeye yarıyor
// site bazlı izin verilmek isteniyorsa bu yöntem kullanılır
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("AllowOrigin", builder => builder.WithOrigins("site1", "site2", "site3"));
//});


//tüm istekler karşılanması isteniyorsa bu yöntem kullanılır
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});





builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});














var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMidleware();

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
