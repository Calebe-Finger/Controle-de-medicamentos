using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloMedicamento;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPrescricao;

namespace ControleDeMedicamentos.WebApp.DependencyInjection;

public static class InfraestruturaConfig
{

    public static void AddCamadaInfraestrutura(this IServiceCollection services)
    {
        services.AddScoped((_) => new ContextoDados(true));
        services.AddScoped<RepositorioMedicamentoEmArquivo>();    // Injeta um serviço por requisição HTTP (ação)
        services.AddScoped<RepositorioFornecedorEmArquivo>();
        services.AddScoped<RepositorioFuncionarioEmArquivo>();
        services.AddScoped<RepositorioPacienteEmArquivo>();
        services.AddScoped<RepositorioPrescricaoEmArquivo>();
        //builder.Services.AddSingleton(); // Injeta uma instancia unica do serviço globalmente
        //builder.Services.AddTransient(); // Intancia o serviço TODA VEZ que for chamado em uma requisição
    }
}
