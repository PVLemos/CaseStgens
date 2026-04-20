using GoodHamburger.Web.Models;

namespace GoodHamburger.Web.Services;

public class CarrinhoState
{
    public List<ItemCardapio> Itens { get; private set; } = new();
    public List<int> ItemIds => Itens.Select(i => i.Id).ToList();
    
    private readonly NotificationService _notificationService;

    public CarrinhoState(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public event Action? OnChange;

    public void AdicionarItem(ItemCardapio item)
    {
        if (item.Categoria == CategoriaItem.Sanduiche && Itens.Any(i => i.Id == item.Id))
        {
            _notificationService.Notify($"Ops! Você já adicionou o {item.Nome}. Não é permitido repetir sanduíches.");
            return;
        }

        Itens.Add(item);
        _notificationService.Notify($"{item.Nome} adicionado ao carrinho!");
        NotificarEstadoAlterado();
    }

    public void RemoverItem(ItemCardapio item)
    {
        Itens.Remove(item);
        NotificarEstadoAlterado();
    }

    public void LimparCarrinho()
    {
        Itens.Clear();
        NotificarEstadoAlterado();
    }

    private void NotificarEstadoAlterado() => OnChange?.Invoke();
}
