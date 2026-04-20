namespace GoodHamburger.Web.DTOs;

public class PedidoRequestDto
{
    public List<int> ItemIds { get; set; } = new();
}

public class PedidoResponseDto
{
    public int Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
    public List<ItemCardapioDto> Itens { get; set; } = new();
}

public class ItemCardapioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Categoria { get; set; } = string.Empty;
}
