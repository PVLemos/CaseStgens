using System.Text.Json.Serialization;

namespace GoodHamburger.Web.Models;

public class ItemCardapio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoriaItem Categoria { get; set; }
}
