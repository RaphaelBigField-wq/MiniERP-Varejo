
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly MiniERPContext _context;

    public VendasController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var vendas = await _context.Vendas
            .ToListAsync();

        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var venda = await _context.Vendas
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venda == null)
            return NotFound("Venda não encontrada.");

        return Ok(venda);
    }

    [HttpPost]
    public async Task<IActionResult> Iniciar()
    {
        var venda = new Venda
        {
            DataHora = DateTime.Now,
            ValorTotal = 0,
            Status = "Em andamento"
        };

        _context.Vendas.Add(venda);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Consultar),
            new { id = venda.Id },
            venda);
    }
}
