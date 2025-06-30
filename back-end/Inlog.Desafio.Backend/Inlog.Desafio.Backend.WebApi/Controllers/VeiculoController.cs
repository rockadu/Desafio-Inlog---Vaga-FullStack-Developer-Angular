using Inlog.Desafio.Backend.Application.Veiculo;
using Inlog.Desafio.Backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inlog.Desafio.Backend.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly ILogger<VeiculoController> _logger;
    private readonly IVeiculoServico _veiculoServico;

    public VeiculoController(ILogger<VeiculoController> logger, IVeiculoServico veiculoServico)
    {
        _logger = logger;
        _veiculoServico = veiculoServico;
    }

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] Veiculo dadosDoVeiculo)
    {
        var veiculo = await _veiculoServico.CadastrarAsync(dadosDoVeiculo);

        return Ok(veiculo);
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarVeiculosAsync()
    {
        var veiculos = await _veiculoServico.ListarVeiculosAsync();

        return Ok(veiculos);
    }
}

