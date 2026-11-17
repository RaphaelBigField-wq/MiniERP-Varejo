
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

    

[HttpPost]
public async Task<IActionResult> Registrar(MovimentacaoEstoque movimentacao)
{
    if (movimentacao.Quantidade <= 0)
        return BadRequest("A quantidade deve ser maior que zero.");

    var produto = await _context.Produtos
        .FirstOrDefaultAsync(p => p.Id == movimentacao.ProdutoId);

    if (produto == null)
        return NotFound("Produto não encontrado.");

    if (movimentacao.Tipo is not ("Entrada" or "Saída" or "Ajuste"))
        return BadRequest("Tipo de movimentação inválido.");

    decimal variacaoEstoque;

    if (movimentacao.Tipo == "Entrada")
    {
        if (movimentacao.DirecaoAjuste != null ||
            movimentacao.MotivoAjusteEstoqueId.HasValue)
            return BadRequest("Entrada não deve possuir direção ou motivo de ajuste.");

        variacaoEstoque = movimentacao.Quantidade;
    }
    else if (movimentacao.Tipo == "Saída")
    {
        if (movimentacao.DirecaoAjuste != null ||
            movimentacao.MotivoAjusteEstoqueId.HasValue)
            return BadRequest("Saída não deve possuir direção ou motivo de ajuste.");

        variacaoEstoque = -movimentacao.Quantidade;
    }
    else
    {
        if (movimentacao.DirecaoAjuste == "Aumentar")
            variacaoEstoque = movimentacao.Quantidade;
        else if (movimentacao.DirecaoAjuste == "Diminuir")
            variacaoEstoque = -movimentacao.Quantidade;
        else
            return BadRequest("Direção do ajuste inválida.");

        if (!movimentacao.MotivoAjusteEstoqueId.HasValue)
            return BadRequest("O motivo do ajuste é obrigatório.");

        var motivo = await _context.MotivosAjusteEstoque
            .FirstOrDefaultAsync(m =>
                m.Id == movimentacao.MotivoAjusteEstoqueId &&
                m.Ativo);

        if (motivo == null)
            return BadRequest("Motivo de ajuste não encontrado ou inativo.");
    }

    if (produto.EstoqueAtual + variacaoEstoque < 0)
        return BadRequest("Estoque insuficiente para realizar a movimentação.");

    produto.EstoqueAtual += variacaoEstoque;

    movimentacao.DataHora = DateTime.Now;
    movimentacao.Produto = null;

    _context.MovimentacoesEstoque.Add(movimentacao);

    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(Consultar),
        new { id = movimentacao.Id },
        movimentacao
    );
}




 }

