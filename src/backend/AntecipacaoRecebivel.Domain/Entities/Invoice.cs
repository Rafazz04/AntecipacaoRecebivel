using AnticipationOfReceivables.BuildingBlocks.Domain.Base;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

namespace AnticipationOfReceivables.Domain.Entities;

public class Invoice : EntityBase
{
    protected Invoice() { }

    public Invoice(InvoiceNumber number, Money grossAmount, DueDate dueDate, Company company)
    {
        Number = number;
        GrossAmount = grossAmount;
        DueDate = dueDate;
        Company = company;
        CompanyId = company.Id;
    }
    public InvoiceNumber Number { get; private set; } 
    public Money GrossAmount { get; private set; } 
    public DueDate DueDate { get; private set; }

    public Guid CompanyId { get; private set; } 
    public Company Company { get; private set; } 

    public Guid? CartId { get; private set; }
    public Cart? Cart { get; private set; }

    internal void AttachToCart(Cart cart)
    {
        Cart = cart;
        CartId = cart.Id;
    }
}

