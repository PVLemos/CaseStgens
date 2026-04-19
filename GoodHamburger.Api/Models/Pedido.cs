namespace GoodHamburger.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    
    // relacionamento feito pelo EF Core
    public List<ItemCardapio> Itens { get; set; } = new();
    
    public decimal Subtotal { get; set; }
    public decimal Desconto { get; set; }
    public decimal Total { get; set; }
}
