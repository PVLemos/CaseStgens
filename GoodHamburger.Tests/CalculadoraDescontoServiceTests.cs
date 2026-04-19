using GoodHamburger.Api.Models;
using GoodHamburger.Api.Services;

namespace GoodHamburger.Tests;

public class CalculadoraDescontoServiceTests
{
    private readonly CalculadoraDescontoService _service;

    public CalculadoraDescontoServiceTests()
    {
        _service = new CalculadoraDescontoService();
    }

    [Fact]
    public void Calcular_ComSanduicheBatataEBebida_AplicaDesconto20Porcento()
    {
        // Arrange
        var itens = new List<ItemCardapio>
        {
            new ItemCardapio { Preco = 5.0m, Categoria = CategoriaItem.Sanduiche }, // XBurger
            new ItemCardapio { Preco = 2.0m, Categoria = CategoriaItem.BatataFrita },
            new ItemCardapio { Preco = 2.5m, Categoria = CategoriaItem.Bebida }
        };
        // Total esperado: 9.5 -> Menos 20% = 7.6

        // Act
        var resultado = _service.Calcular(itens);

        // Assert
        Assert.Equal(9.5m, resultado.Subtotal);
        Assert.Equal(20m, resultado.PorcentagemDesconto);
        Assert.Equal(1.9m, resultado.ValorDesconto);
        Assert.Equal(7.6m, resultado.Total);
    }

    [Fact]
    public void Calcular_ComSanduicheEBebida_AplicaDesconto15Porcento()
    {
        // Arrange
        var itens = new List<ItemCardapio>
        {
            new ItemCardapio { Preco = 4.5m, Categoria = CategoriaItem.Sanduiche }, // XEgg
            new ItemCardapio { Preco = 2.5m, Categoria = CategoriaItem.Bebida }
        };
        // Total esperado: 7.0 -> Menos 15% = 5.95

        // Act
        var resultado = _service.Calcular(itens);

        // Assert
        Assert.Equal(7.0m, resultado.Subtotal);
        Assert.Equal(15m, resultado.PorcentagemDesconto);
        Assert.Equal(1.05m, resultado.ValorDesconto);
        Assert.Equal(5.95m, resultado.Total);
    }

    [Fact]
    public void Calcular_SemSanduiche_NaoAplicaDesconto()
    {
        // Arrange
        var itens = new List<ItemCardapio>
        {
            new ItemCardapio { Preco = 2.0m, Categoria = CategoriaItem.BatataFrita },
            new ItemCardapio { Preco = 2.5m, Categoria = CategoriaItem.Bebida }
        };
        
        // Act
        var resultado = _service.Calcular(itens);

        // Assert
        Assert.Equal(4.5m, resultado.Subtotal);
        Assert.Equal(0m, resultado.PorcentagemDesconto);
        Assert.Equal(0m, resultado.ValorDesconto);
        Assert.Equal(4.5m, resultado.Total);
    }

    [Fact]
    public void Calcular_MaisDeUmSanduiche_LancaExcecao()
    {
        // Arrange
        var itens = new List<ItemCardapio>
        {
            new ItemCardapio { Categoria = CategoriaItem.Sanduiche },
            new ItemCardapio { Categoria = CategoriaItem.Sanduiche }
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _service.Calcular(itens));
        Assert.Equal("Cada pedido pode conter apenas um sanduíche.", ex.Message);
    }
}
