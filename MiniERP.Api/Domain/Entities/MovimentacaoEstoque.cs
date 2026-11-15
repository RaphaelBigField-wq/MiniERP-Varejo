using Microsoft.EntityFrameworkCore;

namespace MiniERP.Api.Domain.Entities;

public class MovimentacaoEstoque
{
    public int Id { get; set; }

    public int ProdutoId { get; set; }

    public Produto? Produto { get; set; }

    public string Tipo { get; set; } = string.Empty;
    
    [Precision(18,3)]
    public decimal Quantidade { get; set; }

    public string? DirecaoAjuste { get; set; }

    public DateTime DataHora { get; set; }

    public int UsuarioResponsavelId { get; set; }

    public int? MotivoAjusteEstoqueId { get; set; }
}