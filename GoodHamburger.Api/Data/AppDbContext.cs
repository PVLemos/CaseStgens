using Microsoft.EntityFrameworkCore;
using GoodHamburger.Api.Models;

namespace GoodHamburger.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // construtor dbcontext = banco de dados em memória

    public DbSet<ItemCardapio> Cardapio { get; set; }
    public DbSet<Pedido> Pedidos { get; set; } // setando os pedidos no db

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // populando o cardápio
        modelBuilder.Entity<ItemCardapio>().HasData(
            new ItemCardapio { Id = 1, Nome = "X Burger", Preco = 5.00m, Categoria = CategoriaItem.Sanduiche }, // preço em reais
            new ItemCardapio { Id = 2, Nome = "X Egg", Preco = 4.50m, Categoria = CategoriaItem.Sanduiche },
            new ItemCardapio { Id = 3, Nome = "X Bacon", Preco = 7.00m, Categoria = CategoriaItem.Sanduiche },
            new ItemCardapio { Id = 4, Nome = "Batata frita", Preco = 2.00m, Categoria = CategoriaItem.BatataFrita },
            new ItemCardapio { Id = 5, Nome = "Refrigerante", Preco = 2.50m, Categoria = CategoriaItem.Bebida }
        );
    }
}
