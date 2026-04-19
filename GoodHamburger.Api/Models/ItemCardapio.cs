namespace GoodHamburger.Api.Models;

public class ItemCardapio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public CategoriaItem Categoria { get; set; }
}
