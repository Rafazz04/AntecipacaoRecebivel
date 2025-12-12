using AnticipationOfReceivables.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnticipationOfReceivables.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices", "AnticipationOfReceivables");

        builder.HasKey(i => i.Id);

        builder.OwnsOne(i => i.Number, number =>
        {
            number.Property(n => n.Value)
                .HasColumnName("Number")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(i => i.GrossAmount, money =>
        {
            money.Property(m => m.Value)
                .HasColumnName("GrossAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(i => i.DueDate, dueDate =>
        {
            dueDate.Property(d => d.Value)
                .HasColumnName("DueDate")
                .HasColumnType("datetime2")
                .IsRequired();
        });


        builder.HasOne(i => i.Company)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Cart)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.CartId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Metadata
            .FindNavigation(nameof(Invoice.Cart))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
