namespace GoodHamburger.Api.DTOs;

public class PedidoRequestDto
{
    // resposta apenas com id´s dos produtos
    public List<int> ItemIds { get; set; } = new();
}

public class PedidoResponseDto // resposta com os dados do pedido
{
    public int Id { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
    public List<ItemCardapioDto> Itens { get; set; } = new();
}

public class ItemCardapioDto // resposta com os dados dos itens do cardápio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Categoria { get; set; } = string.Empty;
}
