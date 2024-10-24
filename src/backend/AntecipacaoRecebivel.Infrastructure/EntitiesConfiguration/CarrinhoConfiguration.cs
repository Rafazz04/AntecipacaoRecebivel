using AntecipacaoRecebivel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntecipacaoRecebivel.Infrastructure.EntitiesConfiguration;

public class CarrinhoConfiguration : IEntityTypeConfiguration<Carrinho>
{
	public void Configure(EntityTypeBuilder<Carrinho> builder)
	{
		builder.ToTable("CARRINHO");

		builder.HasKey(c => c.Id);

		builder.Property(c => c.ValorTotalBruto)
			   .HasColumnType("decimal(18,2)");

		builder.Property(c => c.ValorTotalLiquido)
			   .HasColumnType("decimal(18,2)");

		builder.HasOne(c => c.Empresa)
			   .WithMany(e => e.Carrinhos)
			   .HasForeignKey(c => c.EmpresaId)
			   .OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(c => c.NotasFiscais)
			   .WithOne(nf => nf.Carrinho)
			   .HasForeignKey(nf => nf.CarrinhoId)
			   .OnDelete(DeleteBehavior.SetNull);
	}
}
