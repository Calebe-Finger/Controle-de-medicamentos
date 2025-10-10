using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFornecedor;
using ControleDeMedicamentos.Infraestrutura.Arquivos.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFuncionarios.WebApp.Controllers;

public class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    //Inversão de Controle
    public  FuncionarioController(RepositorioFuncionarioEmArquivo repositorioFuncionario, 
            RepositorioFornecedorEmArquivo repositorioFornecedor)
    {
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var medicamentos = repositorioFuncionario.SelecionarRegistros();

        var visualizarVm = new VisualizarFuncionariosViewModel(medicamentos);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var fornecedoresDisponiveis = repositorioFornecedor.SelecionarRegistros();

        var cadastrarVm = new CadastrarFuncionarioViewModels();

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarFuncionarioViewModels cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        var entidade = new Funcionario(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf
        );

        repositorioFuncionario.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioFuncionario.SelecionarRegistroPorId(id);

        var fornecedoresDisponiveis = repositorioFornecedor.SelecionarRegistros();

        var editarVm = new EditarFuncionarioViewModel(
            registro.Id,
            registro.Nome,
            registro.Telefone,
            registro.Cpf
        );

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var medicamentoEditado = new Funcionario(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf
        );

        repositorioFuncionario.EditarRegistro(editarVm.Id, medicamentoEditado);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioFuncionario.SelecionarRegistroPorId(id);

        var excluirVm = new ExcluirFuncionarioViewModel(
            registro.Id,
            registro.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirFuncionarioViewModel excluirVm)
    {
        repositorioFuncionario.ExcluirRegistro(excluirVm.Id);

        return RedirectToAction(nameof(Index));
    }
}

