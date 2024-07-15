using ApiGateway.Config;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.IO.Compression;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var routes = "Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});
builder.Services.AddOcelot(builder.Configuration).AddPolly();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "abc",
       builder =>
       {
           builder.WithOrigins("http://localhost:3000")
              .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
       });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    //options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "*/*" });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();
RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
string publicPrivateKey = builder.Configuration["PublicPrivateKey"];

//provider.FromXmlString(publicPrivateKey);

RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);


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

app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;

}).UseOcelot().Wait();

app.Run();

