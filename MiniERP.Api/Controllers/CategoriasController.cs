
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly MiniERPContext _context;

    public CategoriasController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var categorias = await _context.Categorias.ToListAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(
        [FromBody] Categoria categoria)
    {
        var nome = categoria.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            return BadRequest("O nome da categoria é obrigatório.");
        }

        var nomeNormalizado = nome.ToLower();

        var existe = await _context.Categorias.AnyAsync(c =>
            c.Nome.Trim().ToLower() == nomeNormalizado);

        if (existe)
        {
            return Conflict("Já existe uma categoria com esse nome.");
        }

        categoria.Nome = nome;
        categoria.Ativa = true;

        _context.Categorias.Add(categoria);

        await _context.SaveChangesAsync();

        return Created("api/Categorias", categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] Categoria categoria)
    {
        var categoriaExistente = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoriaExistente == null)
        {
            return NotFound("Categoria não encontrada.");
        }

        var nome = categoria.Nome?.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            return BadRequest("O nome da categoria é obrigatório.");
        }

        var nomeNormalizado = nome.ToLower();

        var existe = await _context.Categorias.AnyAsync(c =>
            c.Id != id &&
            c.Nome.Trim().ToLower() == nomeNormalizado);

        if (existe)
        {
            return Conflict("Já existe uma categoria com esse nome.");
        }

        categoriaExistente.Nome = nome;
        categoriaExistente.Ativa = categoria.Ativa;

        await _context.SaveChangesAsync();

        return Ok(categoriaExistente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Inativar(int id)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada.");
        }

        categoria.Ativa = false;

        await _context.SaveChangesAsync();

        return Ok(categoria);
    }
}
