using Inlog.Desafio.Backend.Application.Veiculo;
using Inlog.Desafio.Backend.Domain.Dtos;
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
    public async Task<IActionResult> Cadastrar([FromBody] VeiculoDto input)
    {
        await _veiculoServico.CadastrarAsync(input);

        return Ok();
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarVeiculosAsync()
    {
        var veiculos = (await _veiculoServico.ListarVeiculosAsync()).ToList();

        return Ok(veiculos);
    }
}

