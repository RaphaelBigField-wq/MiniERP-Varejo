
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Data;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly MiniERPContext _context;

    public ProdutosController(MiniERPContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .ToListAsync();

        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(
        [FromBody] Produto produto)
    {
        produto.Nome = produto.Nome.Trim();
        produto.Descricao = produto.Descricao.Trim();
        produto.CodigoBarras = produto.CodigoBarras?.Trim();

        if (string.IsNullOrWhiteSpace(produto.Nome))
            return BadRequest("O nome do produto é obrigatório.");

        if (string.IsNullOrWhiteSpace(produto.Descricao))
            return BadRequest("A descrição do produto é obrigatória.");

        if (produto.PrecoVenda <= 0)
            return BadRequest("O preço de venda deve ser maior que zero.");

        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == produto.CategoriaId);

        if (categoria == null || !categoria.Ativa)
            return BadRequest("Selecione uma categoria ativa.");

        if (!string.IsNullOrWhiteSpace(produto.CodigoBarras))
        {
            var existe = await _context.Produtos.AnyAsync(p =>
                p.CodigoBarras == produto.CodigoBarras);

            if (existe)
                return Conflict("Já existe um produto com esse código de barras.");
        }
        else
        {
            produto.CodigoBarras = null;
        }

        produto.Ativo = true;
        produto.DisponivelVenda = true;

        _context.Produtos.Add(produto);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Consultar),
            new { id = produto.Id },
            produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] Produto produto)
    {
        var produtoExistente = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produtoExistente == null)
            return NotFound("Produto não encontrado.");

        produto.Nome = produto.Nome.Trim();
        produto.Descricao = produto.Descricao.Trim();
        produto.CodigoBarras = produto.CodigoBarras?.Trim();

        if (string.IsNullOrWhiteSpace(produto.Nome))
            return BadRequest("O nome do produto é obrigatório.");

        if (string.IsNullOrWhiteSpace(produto.Descricao))
            return BadRequest("A descrição do produto é obrigatória.");

        if (produto.PrecoVenda <= 0)
            return BadRequest("O preço de venda deve ser maior que zero.");

        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == produto.CategoriaId);

        if (categoria == null || !categoria.Ativa)
            return BadRequest("Selecione uma categoria ativa.");

        if (!string.IsNullOrWhiteSpace(produto.CodigoBarras))
        {
            var existe = await _context.Produtos.AnyAsync(p =>
                p.Id != id &&
                p.CodigoBarras == produto.CodigoBarras);

            if (existe)
                return Conflict("Já existe um produto com esse código de barras.");
        }
        else
        {
            produto.CodigoBarras = null;
        }

        produtoExistente.Nome = produto.Nome;
        produtoExistente.Descricao = produto.Descricao;
        produtoExistente.CodigoBarras = produto.CodigoBarras;
        produtoExistente.PrecoVenda = produto.PrecoVenda;
        produtoExistente.CategoriaId = produto.CategoriaId;
        produtoExistente.DisponivelVenda = produto.DisponivelVenda;

        await _context.SaveChangesAsync();

        return Ok(produtoExistente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Inativar(int id)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        produto.Ativo = false;
        produto.DisponivelVenda = false;

        await _context.SaveChangesAsync();

        return Ok(produto);
    }
}
