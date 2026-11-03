namespace MiniERP.Api.Domain.Entities;

public class Categoria
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public bool Ativa { get; set; } = true;
}

