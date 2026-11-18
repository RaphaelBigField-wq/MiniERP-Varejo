using Microsoft.EntityFrameworkCore;

namespace MiniERP.Api.Domain.Entities;

public class Venda
{
    public int Id { get; set; }

    public DateTime DataHora { get; set; }

    [Precision(18, 2)]
    public decimal ValorTotal { get; set; }

    public string Status { get; set; } = "Em andamento";
}