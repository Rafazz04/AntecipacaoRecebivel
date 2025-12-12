using AnticipationOfReceivables.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnticipationOfReceivables.Infrastructure.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart", "AnticipationOfReceivables");

        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.GrossTotalAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("GrossTotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(c => c.NetTotalAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("NetTotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(c => c.AvailableCreditLimit, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("AvailableCreditLimit")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });


        builder.Metadata
            .FindNavigation(nameof(Cart.Invoices))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(c => c.Invoices)
               .WithOne(i => i.Cart)
               .HasForeignKey(i => i.CartId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Company)
               .WithMany(company => company.Carts)
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
