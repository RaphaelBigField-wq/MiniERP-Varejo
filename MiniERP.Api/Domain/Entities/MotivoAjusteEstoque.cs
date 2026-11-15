
namespace MiniERP.Api.Domain.Entities;

public class MotivoAjusteEstoque
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
}
