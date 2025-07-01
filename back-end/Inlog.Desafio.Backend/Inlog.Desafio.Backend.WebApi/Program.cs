using Inlog.Desafio.Backend.CrossCutting.Configuracao;
using Inlog.Desafio.Backend.Infra.Database;
using Inlog.Desafio.Backend.Application;

using Supabase;

var builder = WebApplication.CreateBuilder(args);
    
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var settings = configuration.Get<ConfiguracaoAplicacao>();


var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = false
};

var supabase = new Client(settings!.SupaBase.Url, settings.SupaBase.Key, options);
await supabase.InitializeAsync();

builder.Services.AddSingleton(settings!);
builder.Services.AddSingleton(supabase);
builder.Services.AdicionarRepositorios();
builder.Services.AdicionarServicos();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
