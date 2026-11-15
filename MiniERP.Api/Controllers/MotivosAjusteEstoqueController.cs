
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace dotMiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotivosAjusteEstoqueController : ControllerBase
{
    private readonly MiniERPContext _context;

    public MotivosAjusteEstoqueController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var motivos = await _context.MotivosAjusteEstoque.ToListAsync();
        return Ok(motivos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var motivo = await _context.MotivosAjusteEstoque.FindAsync(id);

        if (motivo == null)
            return NotFound("Motivo de ajuste não encontrado.");

        return Ok(motivo);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(MotivoAjusteEstoque motivo)
    {
        motivo.Nome = motivo.Nome.Trim();

        if (string.IsNullOrWhiteSpace(motivo.Nome))
            return BadRequest("O nome do motivo é obrigatório.");

        var duplicado = await _context.MotivosAjusteEstoque
            .AnyAsync(m => m.Nome.ToLower() == motivo.Nome.ToLower());

        if (duplicado)
            return Conflict("Já existe um motivo com esse nome.");

        motivo.Ativo = true;

        _context.MotivosAjusteEstoque.Add(motivo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = motivo.Id },
            motivo
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, MotivoAjusteEstoque dados)
    {
        var motivo = await _context.MotivosAjusteEstoque.FindAsync(id);

        if (motivo == null)
            return NotFound("Motivo de ajuste não encontrado.");

        dados.Nome = dados.Nome.Trim();

        if (string.IsNullOrWhiteSpace(dados.Nome))
            return BadRequest("O nome do motivo é obrigatório.");

        var duplicado = await _context.MotivosAjusteEstoque
            .AnyAsync(m => m.Id != id &&
                           m.Nome.ToLower() == dados.Nome.ToLower());

        if (duplicado)
            return Conflict("Já existe um motivo com esse nome.");

        motivo.Nome = dados.Nome;

        await _context.SaveChangesAsync();

        return Ok(motivo);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Inativar(int id)
    {
        var motivo = await _context.MotivosAjusteEstoque.FindAsync(id);

        if (motivo == null)
            return NotFound("Motivo de ajuste não encontrado.");

        motivo.Ativo = false;

        await _context.SaveChangesAsync();

        return Ok(motivo);
    }
}
