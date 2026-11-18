
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItensVendaController : ControllerBase
{
    private readonly MiniERPContext _context;

    public ItensVendaController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var itens = await _context.ItensVenda
            .Include(i => i.Produto)
            .ToListAsync();

        return Ok(itens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var item = await _context.ItensVenda
            .Include(i => i.Produto)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
            return NotFound("Item de venda não encontrado.");

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(ItemVenda item)
    {
        var venda = await _context.Vendas
            .FirstOrDefaultAsync(v => v.Id == item.VendaId);

        if (venda == null)
            return NotFound("Venda não encontrada.");

        if (venda.Status != "Em andamento")
            return BadRequest("Não é possível adicionar itens a uma venda que não está em andamento.");

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == item.ProdutoId);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        if (!produto.Ativo || !produto.DisponivelVenda)
            return BadRequest("Produto inativo ou indisponível para venda.");

        if (item.Quantidade <= 0)
            return BadRequest("A quantidade deve ser maior que zero.");

        if (produto.EstoqueAtual < item.Quantidade)
            return BadRequest("Estoque insuficiente.");

        item.PrecoUnitario = produto.PrecoVenda;
        item.Subtotal = item.Quantidade * item.PrecoUnitario;

        produto.EstoqueAtual -= item.Quantidade;

        venda.ValorTotal += item.Subtotal;

        _context.ItensVenda.Add(item);

        await _context.SaveChangesAsync();

        return Ok(item);
    }
}
