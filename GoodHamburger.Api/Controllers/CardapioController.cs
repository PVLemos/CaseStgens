using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Api.Data;
using GoodHamburger.Api.DTOs; // data transfer objects

namespace GoodHamburger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardapioController : ControllerBase // Puxa o controller base para ter acesso aos métodos HTTP
{
    private readonly AppDbContext _context; 

    public CardapioController(AppDbContext context) 
    {
        _context = context; // Injeção de dependência
    }

    [HttpGet] // Método HTTP GET
    public async Task<IActionResult> GetCardapio()
    { // Método assíncrono que retorna uma lista de itens do cardápio
        var itens = await _context.Cardapio.ToListAsync();
        
        var dto = itens.Select(i => new ItemCardapioDto // Transforma os itens do cardápio em DTOs
        {
            Id = i.Id,
            Nome = i.Nome,
            Preco = i.Preco,
            Categoria = i.Categoria.ToString()
        });

        return Ok(dto);
    }
}
