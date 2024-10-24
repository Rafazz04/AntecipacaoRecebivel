using AntecipacaoRecebivel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntecipacaoRecebivel.Infrastructure.EntitiesConfiguration;

public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
{
	public void Configure(EntityTypeBuilder<NotaFiscal> builder)
	{
		builder.ToTable("NOTAFISCAL");

		builder.HasKey(nf => nf.Id);

		builder.Property(nf => nf.Numero)
			   .IsRequired()
			   .HasMaxLength(50); 

		builder.Property(nf => nf.ValorBruto)
			   .IsRequired()
			   .HasColumnType("decimal(18,2)");

		builder.Property(nf => nf.DataVencimento)
			   .IsRequired();

		builder.HasOne(nf => nf.Empresa)
			   .WithMany(e => e.NotasFiscais)
			   .HasForeignKey(nf => nf.EmpresaId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(nf => nf.Carrinho)
			   .WithMany(c => c.NotasFiscais)
			   .HasForeignKey(nf => nf.CarrinhoId)
			   .OnDelete(DeleteBehavior.SetNull); 
	}
}
