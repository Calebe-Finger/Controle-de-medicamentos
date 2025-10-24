using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloPaciente;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

public class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    //Inversão de Controle
    public PacienteController(RepositorioPacienteEmArquivo repositorioPaciente,
            RepositorioFornecedorEmArquivo repositorioFornecedor)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var medicamentos = repositorioPaciente.SelecionarRegistros();

        var visualizarVm = new VisualizarPacientesViewModel(medicamentos);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var fornecedoresDisponiveis = repositorioFornecedor.SelecionarRegistros();

        var cadastrarVm = new CadastrarPacienteViewModels();

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarPacienteViewModels cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var entidade = new Paciente(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.CartaoSus,
            cadastrarVm.Cpf
        );

        repositorioPaciente.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioPaciente.SelecionarRegistroPorId(id);

        var fornecedoresDisponiveis = repositorioFornecedor.SelecionarRegistros();

        var editarVm = new EditarPacienteViewModel(
            registro.Id,
            registro.Nome,
            registro.Telefone,
            registro.CartaoSus,
            registro.Cpf
        );

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarPacienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var medicamentoEditado = new Paciente(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.CartaoSus,
            editarVm.Cpf
        );

        repositorioPaciente.EditarRegistro(editarVm.Id, medicamentoEditado);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioPaciente.SelecionarRegistroPorId(id);

        var excluirVm = new ExcluirPacienteViewModel(
            registro.Id,
            registro.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        repositorioPaciente.ExcluirRegistro(excluirVm.Id);

        return RedirectToAction(nameof(Index));
    }
}
