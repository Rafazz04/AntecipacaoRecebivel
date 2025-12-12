using AnticipationOfReceivables.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnticipationOfReceivables.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company", "AnticipationOfReceivables");

        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Cnpj, cnpj =>
        {
            cnpj.Property(c => c.Value)
                .HasColumnName("Cnpj")
                .HasMaxLength(14)
                .IsRequired();
        });

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(c => c.Revenue, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("Revenue")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(c => c.CreditLimit, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("CreditLimit")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.Property(c => c.BusinessSector)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();


        builder.Metadata
            .FindNavigation(nameof(Company.Invoices))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.Invoices)
               .WithOne(i => i.Company)
               .HasForeignKey(i => i.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Metadata
            .FindNavigation(nameof(Company.Carts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.Carts)
               .WithOne(cart => cart.Company)
               .HasForeignKey(cart => cart.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

