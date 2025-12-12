using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class CartError
{
    public static readonly BusinessError InvoiceAlreadyExixts = 
        new("Cart.Invoice.Exists", "Esta nota fiscal já foi adicionado anteriormente neste carrinho.");

    public static readonly BusinessError NoLimit =
       new("Cart.AvailableCreditLimit.NoLimit", "Não foi possível adicionar esta nota, pois excede o limite de crédito.");

    public static readonly BusinessError InvoiceNoExists =
       new("Cart.Invoice.NoExists", "Não foi possível remover essa nota, pois ela não está no carrinho.");

    public static readonly BusinessError CartNoExixts = 
        new("Cart.Id.NoExists", "Esse carrinho não esta cadastrado em nosso sitema.");
}
