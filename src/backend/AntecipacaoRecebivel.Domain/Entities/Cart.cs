using AnticipationOfReceivables.BuildingBlocks.Domain.Base;
using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.Domain.Entities;

public class Cart : EntityBase
{
    protected Cart() { }

    public Cart(Company company, Money availableCreditLimit)
    {
        Company = company;
        CompanyId = company.Id;

        AvailableCreditLimit = availableCreditLimit;
        GrossTotalAmount = Money.Zero();
        NetTotalAmount = Money.Zero();
    }

    public Money GrossTotalAmount { get; private set; }
    public Money NetTotalAmount { get; private set; }
    public Money AvailableCreditLimit { get; private set; }

    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }

    private readonly List<Invoice> _invoices = new();
    public IReadOnlyCollection<Invoice> Invoices => _invoices;

    public void AddInvoice(Invoice invoice)
    {
        if (_invoices.Any(i => i.Id == invoice.Id))
            throw new BusinessException(CartError.InvoiceAlreadyExixts);

        var projected = GrossTotalAmount.Value + invoice.GrossAmount.Value;
        if (projected > AvailableCreditLimit.Value)
            throw new BusinessException(CartError.NoLimit);

        _invoices.Add(invoice);
        invoice.AttachToCart(this);

        GrossTotalAmount = new Money(GrossTotalAmount.Value + invoice.GrossAmount.Value, allowZero: true);
        RecalculateNetAmount();
    }

    public void RemoveInvoice(Invoice invoice)
    {
        if (!_invoices.Remove(invoice))
            throw new BusinessException(CartError.InvoiceNoExists);

        GrossTotalAmount = new Money(GrossTotalAmount.Value - invoice.GrossAmount.Value, allowZero: true);
        RecalculateNetAmount();
    }

    private void RecalculateNetAmount()
    {
        NetTotalAmount = new Money(GrossTotalAmount.Value, allowZero: true);
    }
}
