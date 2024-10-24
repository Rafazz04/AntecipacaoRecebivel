using AntecipacaoRecebivel.Domain.Entities;

namespace AntecipacaoRecebivel.Domain.Interfaces;

public interface ICarrinhoRepository : IRepositoryBase<Carrinho>
{
    Carrinho GetCarrinhoComEmpresa(int id);
}
