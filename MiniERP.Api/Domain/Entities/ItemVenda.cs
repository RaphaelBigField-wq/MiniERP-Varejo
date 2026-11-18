
using Microsoft.EntityFrameworkCore;

namespace MiniERP.Api.Domain.Entities;

public class ItemVenda
{
    public int Id { get; set; }

    public int VendaId { get; set; }

    public Venda? Venda { get; set; }

    public int ProdutoId { get; set; }

    public Produto? Produto { get; set; }

    [Precision(18, 3)]
    public decimal Quantidade { get; set; }

    [Precision(18, 2)]
    public decimal PrecoUnitario { get; set; }

    [Precision(18, 2)]
    public decimal Subtotal { get; set; }
}
