using GoodHamburger.Api.Models;

namespace GoodHamburger.Api.Services;

public class ResultadoCalculo
{
    public decimal Subtotal { get; set; }
    public decimal PorcentagemDesconto { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal Total { get; set; }
}

public interface ICalculadoraDescontoService
{
    ResultadoCalculo Calcular(List<ItemCardapio> itens);
}

public class CalculadoraDescontoService : ICalculadoraDescontoService
{
    public ResultadoCalculo Calcular(List<ItemCardapio> itens)
    {
        if (itens == null || !itens.Any())
            return new ResultadoCalculo { Subtotal = 0, PorcentagemDesconto = 0, ValorDesconto = 0, Total = 0 };

        // validação de regras de negócio (limite de itens por categoria)
        var qtdSanduiches = itens.Count(i => i.Categoria == CategoriaItem.Sanduiche);
        var qtdBatatas = itens.Count(i => i.Categoria == CategoriaItem.BatataFrita);
        var qtdBebidas = itens.Count(i => i.Categoria == CategoriaItem.Bebida);

        if (qtdSanduiches > 1)
            throw new ArgumentException("Cada pedido pode conter apenas um sanduíche.");
            
        if (qtdBatatas > 1)
            throw new ArgumentException("Cada pedido pode conter apenas uma batata frita.");
            
        if (qtdBebidas > 1)
            throw new ArgumentException("Cada pedido pode conter apenas um refrigerante.");

        // Cálculo do subtotal
        decimal subtotal = itens.Sum(i => i.Preco);

        // Aplicação do desconto com base nas combinações
        decimal porcentagemDesconto = 0m;

        bool temSanduiche = qtdSanduiches == 1;
        bool temBatata = qtdBatatas == 1;
        bool temBebida = qtdBebidas == 1;

        if (temSanduiche && temBatata && temBebida)
        {
            porcentagemDesconto = 0.20m; // 20%
        }
        else if (temSanduiche && temBebida)
        {
            porcentagemDesconto = 0.15m; // 15%
        }
        else if (temSanduiche && temBatata)
        {
            porcentagemDesconto = 0.10m; // 10%
        }

        decimal valorDesconto = subtotal * porcentagemDesconto;
        decimal total = subtotal - valorDesconto;

        return new ResultadoCalculo
        {
            Subtotal = subtotal,
            PorcentagemDesconto = porcentagemDesconto * 100,
            ValorDesconto = valorDesconto,
            Total = total
        };
    }
}
