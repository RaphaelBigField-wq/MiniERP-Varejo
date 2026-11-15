using Microsoft.EntityFrameworkCore;

namespace MiniERP.Api.Domain.Entities;

public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string? CodigoBarras { get; set; }

    [Precision(18, 2)]
    public decimal PrecoVenda { get; set; }
   
    [Precision(18,3)]
    public decimal EstoqueAtual {get; set; } = 0;

    public bool Ativo { get; set; } = true;

    public bool DisponivelVenda { get; set; } = true;

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}