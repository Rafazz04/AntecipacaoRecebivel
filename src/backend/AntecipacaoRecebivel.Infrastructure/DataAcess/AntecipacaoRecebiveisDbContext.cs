using AntecipacaoRecebivel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntecipacaoRecebivel.Infrastructure.DataAcess;

public class AntecipacaoRecebiveisDbContext : DbContext
{
    public AntecipacaoRecebiveisDbContext(DbContextOptions opt) : base(opt){}

    public DbSet<Empresa> EMPRESA { get; set; }
    public DbSet<NotaFiscal> NOTAFISCAL { get; set; }
    public DbSet<Carrinho> CARRINHO { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(AntecipacaoRecebiveisDbContext).Assembly);
	}
}
