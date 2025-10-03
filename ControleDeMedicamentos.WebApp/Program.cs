using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Injeção de Dependência criada por nós
        builder.Services.AddScoped((_) => new ContextoDados(true));
        builder.Services.AddScoped<RepositorioFuncionarioEmArquivo>();    // Injeta um serviço por requisição HTTP (ação)
        builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
        //builder.Services.AddSingleton(); // Injeta uma instancia unica do serviço globalmente
        //builder.Services.AddTransient(); // Intancia o serviço TODA VEZ que for chamado em uma requisição

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.log");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "controle-de-medicamentos",
                licenseKey: "b4e6361ee88e578da1a5dc12fcf34c7dFFFFNRAL"
            )
            .CreateLogger();

        builder.Logging.ClearProviders();

        builder.Services.AddSerilog();

        //Injeção de Depencências da Microsoft
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    //private static ContextoDados ConfigurarContextoDados(IServiceProvider serviceProvider) //Substituido pela linha 14
    //{ return new ContextoDados(true); }
}

