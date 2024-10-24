using AntecipacaoRecebivel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntecipacaoRecebivel.Infrastructure.EntitiesConfiguration;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
	public void Configure(EntityTypeBuilder<Empresa> builder)
	{
		builder.ToTable("EMPRESA");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Cnpj)
			.IsRequired()
			.HasMaxLength(14);

		builder.Property(e => e.Nome)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(e => e.Faturamento)
			.IsRequired()
			.HasColumnType("decimal(18,2)");

		builder.Property(e => e.Ramo)
			.IsRequired()
			.HasConversion<string>();

		builder.HasMany(e => e.NotasFiscais)
			.WithOne(nf => nf.Empresa)
			.HasForeignKey(nf => nf.EmpresaId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
