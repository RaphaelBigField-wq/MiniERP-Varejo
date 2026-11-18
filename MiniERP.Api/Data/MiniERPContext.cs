
using Microsoft.EntityFrameworkCore;
using MiniERP.Api.Domain.Entities;

namespace MiniERP.Api.Data;

public class MiniERPContext : DbContext
{
    public MiniERPContext(DbContextOptions<MiniERPContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias => Set<Categoria>();

    public DbSet<Produto> Produtos => Set<Produto>();
    
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();

    public DbSet<MotivoAjusteEstoque> MotivosAjusteEstoque => Set<MotivoAjusteEstoque>();

    public DbSet<Venda> Vendas => Set<Venda>();

    public DbSet<ItemVenda> ItensVenda => Set<ItemVenda>();
}
