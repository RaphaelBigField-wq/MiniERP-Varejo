
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesEstoqueController : ControllerBase
{
    private readonly MiniERPContext _context;

    public MovimentacoesEstoqueController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var movimentacoes = await _context.MovimentacoesEstoque
            .Include(m => m.Produto)
            .ToListAsync();

        return Ok(movimentacoes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var movimentacao = await _context.MovimentacoesEstoque
            .Include(m => m.Produto)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movimentacao == null)
            return NotFound("Movimentação de estoque não encontrada.");

        return Ok(movimentacao);
    }
}
