using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Api.Data;
using GoodHamburger.Api.DTOs;
using GoodHamburger.Api.Models;
using GoodHamburger.Api.Services;

namespace GoodHamburger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context; // métpdo privado de acesso ao db
    private readonly ICalculadoraDescontoService _calculadora;

    public PedidosController(AppDbContext context, ICalculadoraDescontoService calculadora)
    {
        _context = context;
        _calculadora = calculadora;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePedido([FromBody] PedidoRequestDto request)
    {
        if (request.ItemIds == null || !request.ItemIds.Any())
            return BadRequest("O pedido deve conter pelo menos um item.");

        // Buscar itens do banco de dados
        var itens = await _context.Cardapio
            .Where(i => request.ItemIds.Contains(i.Id))
            .ToListAsync();

        if (itens.Count != request.ItemIds.Count)
            return BadRequest("Um ou mais itens informados não existem no cardápio.");

        try
        {
            // Calcula o pedido e valida regras
            var calculo = _calculadora.Calcular(itens);

            var pedido = new Pedido
            {
                Itens = itens,
                Subtotal = calculo.Subtotal,
                Desconto = calculo.ValorDesconto,
                Total = calculo.Total
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, MapToDto(pedido)); // retorno do pedido criado
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet] // método http get
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _context.Pedidos // lista de pedidos
            .Include(p => p.Itens) // inclui os itens do pedido
            .ToListAsync();

        return Ok(pedidos.Select(MapToDto));
    }

    [HttpGet("{id}")] // método http get com parâmetro
    public async Task<IActionResult> GetPedidoById(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id); // busca o pedido pelo id

        if (pedido == null)
            return NotFound("Pedido não encontrado.");

        return Ok(MapToDto(pedido));
    }

    [HttpPut("{id}")] // método http put com parâmetro
    public async Task<IActionResult> UpdatePedido(int id, [FromBody] PedidoRequestDto request)
    {
         var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id); // busca o pedido pelo id

        if (pedido == null)
            return NotFound("Pedido não encontrado.");
            
        var itens = await _context.Cardapio
            .Where(i => request.ItemIds.Contains(i.Id))
            .ToListAsync();

        if (itens.Count != request.ItemIds.Count)
            return BadRequest("Um ou mais itens informados não existem no cardápio."); // verifica se os itens existem no cardápio

        try 
        {
            var calculo = _calculadora.Calcular(itens);

            // Atualizando dados
            pedido.Itens = itens;
            pedido.Subtotal = calculo.Subtotal;
            pedido.Desconto = calculo.ValorDesconto;
            pedido.Total = calculo.Total;

            await _context.SaveChangesAsync();
            return Ok(MapToDto(pedido));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePedido(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null)
            return NotFound("Pedido não encontrado.");

        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PedidoResponseDto MapToDto(Pedido pedido)
    {
        return new PedidoResponseDto
        {
            Id = pedido.Id,
            Subtotal = pedido.Subtotal,
            Desconto = pedido.Desconto,
            Total = pedido.Total,
            Itens = pedido.Itens.Select(i => new ItemCardapioDto
            {
                Id = i.Id,
                Nome = i.Nome,
                Preco = i.Preco,
                Categoria = i.Categoria.ToString()
            }).ToList()
        };
    }
}
