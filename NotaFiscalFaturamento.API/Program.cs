using NotaFiscalFaturamento.CrossCutting;
using System.Text.Json.Serialization;
using System.Text.Json;
using NotaFiscalFaturamento.API.Services;
using NotaFiscalFaturamento.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureAPI();

builder.Services.AddSingleton<IFaturamentoService, FaturamentoService>();
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularCors",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:4200",
                "https://notafiscal.jasmim.dev")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

var consumer = app.Services.GetRequiredService<IKafkaConsumerService>();

_ = Task.Run(consumer.ConsumirNotas);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AngularCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
