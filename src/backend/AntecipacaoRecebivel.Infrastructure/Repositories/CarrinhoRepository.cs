using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AntecipacaoRecebivel.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;

namespace AntecipacaoRecebivel.Infrastructure.Repositories;

public class CarrinhoRepository : RepositoryBase<Carrinho>, ICarrinhoRepository
{
    private readonly AntecipacaoRecebiveisDbContext _context;
    public CarrinhoRepository(AntecipacaoRecebiveisDbContext context) : base(context)
    {
        _context = context;
    }

    public Carrinho GetCarrinhoComEmpresa(int id)
    {
        return _context.CARRINHO.Include(x => x.Empresa).Include(x => x.NotasFiscais).FirstOrDefault(c => c.Id == id);
    }
}
