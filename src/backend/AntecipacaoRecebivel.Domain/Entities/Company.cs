using AnticipationOfReceivables.BuildingBlocks.Domain.Base;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.Domain.Entities.Enums;

namespace AnticipationOfReceivables.Domain.Entities;

public class Company : EntityBase
{
    protected Company() { }

    public Company(Cnpj cnpj, string name, BusinessSector businessSector, Money revenue)
    {
        Cnpj = cnpj;
        Name = name;
        BusinessSector = businessSector;
        Revenue = revenue;

        CalculateCreditLimit();
    }
    public Cnpj Cnpj { get; private set; } 
    public string Name { get; private set; } 
    public BusinessSector BusinessSector { get; private set; }
    public Money CreditLimit { get; private set; }
    public Money Revenue { get; private set; } 

    private readonly List<Invoice> _invoices = [];
    private readonly List<Cart> _carts = [];

    public IReadOnlyCollection<Invoice> Invoices => _invoices;
    public IReadOnlyCollection<Cart> Carts => _carts;

    public void Update(string name, BusinessSector businessSector, Money revenue)
    {
        Name = name;
        BusinessSector = businessSector;
        Revenue = revenue;

        CalculateCreditLimit();
    }
    public void AddInvoice(Invoice invoice)
    {
        _invoices.Add(invoice);
        Revenue += invoice.GrossAmount;

        CalculateCreditLimit();
    }

    public void InvoicesAdd(Invoice invoice)
    {
        _invoices.Add(invoice);
    }

    public Cart CreateCart()
    {
        var cart = new Cart(this, CreditLimit);
        _carts.Add(cart);
        return cart;
    }

    public void CalculateCreditLimit()
    {
        var revenueValue = Revenue.Value;

        decimal percentage = 0;

        if (revenueValue >= 10_000 && revenueValue <= 50_000)
        {
            percentage = 0.50m;
        }
        else if (revenueValue >= 50_001 && revenueValue <= 100_000)
        {
            percentage = BusinessSector == BusinessSector.SERVIÇOS
                ? 0.55m
                : 0.60m;
        }
        else if (revenueValue > 100_000)
        {
            percentage = BusinessSector == BusinessSector.SERVIÇOS
                ? 0.60m
                : 0.65m;
        }

        CreditLimit = new Money(revenueValue * percentage);
    }
}
