using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AntecipacaoRecebivel.Infrastructure.DataAcess;

namespace AntecipacaoRecebivel.Infrastructure.Repositories;

public class NotaFiscalRepository : RepositoryBase<NotaFiscal>, INotaFiscalRepository
{
    public NotaFiscalRepository(AntecipacaoRecebiveisDbContext context) : base(context)
    {
    }
}
