using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;

namespace ControleDeMedicamentos.Dominio.ModuloRequisicaoMedicamento;

public class RequisicaoEntrada
{
    public Guid Id { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public Funcionario Funcionario { get; set; }
    public Medicamento Medicamento { get; set; }
    public int QuantidadeRequisitada { get; set; }

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(Funcionario funcionario, Medicamento medicamento, int quantidadeRequisitada)
    {
        Id = Guid.NewGuid();
        DataOcorrencia = DateTime.Now;
        Funcionario =  funcionario;
        Medicamento = medicamento;
        QuantidadeRequisitada = quantidadeRequisitada;

    }
}
