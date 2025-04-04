using Microsoft.AspNetCore.Mvc;
using NotaFiscalFaturamento.API.Interfaces;
using NotaFiscalFaturamento.Application.DTOs;
using NotaFiscalFaturamento.Application.Interfaces;
using NotaFiscalFaturamento.Domain.Enums;

namespace NotaFiscalFaturamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotaController(INotaService notaService, IKafkaProducerService kafkaProducerService, ILogger<NotaController> logger) : ControllerBase
{
    private readonly IKafkaProducerService _kafkaProducerService = kafkaProducerService;
    private readonly INotaService _notaService = notaService;
    private readonly ILogger<NotaController> _logger = logger;

    [HttpGet]
    public IActionResult GetNotas()
    {
        try
        {
            IEnumerable<NotaDTO> notas = _notaService.GetNotas();

            return Ok(notas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetNotas_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetNota([FromRoute] int Id)
    {
        try
        {
            NotaDTO? nota = _notaService.GetById(Id);

            if (nota == null)
            {
                return NotFound();
            }

            return Ok(nota);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetNota_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult PostNota(NotaDTO notaDTO)
    {
        try
        {
            NotaDTO? novoNotaDTO = _notaService.Create(notaDTO);

            if (novoNotaDTO == null)
                return BadRequest("Nota não foi criado.");

            return CreatedAtAction(nameof(GetNota), new { Id = novoNotaDTO.Id }, novoNotaDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PostNota_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("{Id:int}")]
    public IActionResult PatchNota([FromRoute] int Id, NotaDTO notaDTO)
    {
        try
        {
            NotaDTO? notaDbDTO = _notaService.GetById(Id);

            if (notaDbDTO == null)
                return NotFound();

            if (notaDbDTO.Id != notaDTO.Id)
                return BadRequest("Nota não condiz com o id do objeto.");

            notaDbDTO = _notaService.Update(Id, notaDTO);

            if (notaDbDTO == null)
                return BadRequest("Nota não foi atualizado");

            return Ok(notaDbDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PatchNota_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("Imprimir/{Id:int}")]
    public async Task<IActionResult> ImprimirNota([FromRoute] int Id)
    {
        try
        {
            NotaDTO? notaDbDTO = _notaService.GetById(Id);

            if (notaDbDTO == null)
                return NotFound();

            await _kafkaProducerService.EnviarNota(notaDbDTO);

            return Ok(notaDbDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ImprimirNota_Exception");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{Id:int}")]
    public IActionResult DeleteNota([FromRoute] int Id)
    {
        try
        {
            NotaDTO? notaDTO = _notaService.GetById(Id);

            if (notaDTO == null)
                return NotFound();

            bool deletado = _notaService.Remove(Id);

            if (deletado)
                return Ok("Deletado com sucesso!");

            return StatusCode(500, "N�o foi possivel deletar!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteNota_Exception");
            return StatusCode(500, ex.Message);
        }
    }
}
