using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Funcionario>
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contextoDados) : base(contextoDados) { }

    protected override List<Funcionario> ObterRegistros()
    {
        return contextoDados.Funcionarios;
    }
}