using Microsoft.EntityFrameworkCore;
using GoodHamburger.Api.Data;
using GoodHamburger.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de dados em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GoodHamburgerDb"));

// Regra de negócio
builder.Services.AddScoped<ICalculadoraDescontoService, CalculadoraDescontoService>();

var app = builder.Build();

// Garantir que a base de dados tem as sementes (seeding)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
