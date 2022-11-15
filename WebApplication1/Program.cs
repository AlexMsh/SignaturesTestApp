using Microsoft.AspNetCore.Http.Features;
using Signatures.Domain.Dependencies;
using Signatures.Repositories.Extensions;
using Signatures.Signant.Client.Proxy.Extensions;
using Sigtatures.Web.Dependencies;
using Sigtatures.Web.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ErrorHandlerFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidators();

builder.Services.RegisterRepositories();
builder.Services.RegisterRepositoryConfigurations(builder.Configuration);

builder.Services.AddHelpersConfiguration(builder.Configuration);
builder.Services.AddHelpers();

builder.Services.AddDomainServices();
builder.Services.AddDomainConfiguration(builder.Configuration);

builder.Services.AddAutoMapper();
builder.Services.AddSignantClient();
builder.Services.AddSignantConfiguration(builder.Configuration);

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


var app = builder.Build();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
