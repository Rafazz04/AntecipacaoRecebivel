using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AntecipacaoRecebivel.Infrastructure.DataAcess;

namespace AntecipacaoRecebivel.Infrastructure.Repositories;

public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
{
	private readonly AntecipacaoRecebiveisDbContext _context;
	public EmpresaRepository(AntecipacaoRecebiveisDbContext context) : base(context)
	{
		_context = context;
	}

	public Empresa GetByCnpj(string cnpj)
	{
		return _context.EMPRESA.FirstOrDefault(e => e.Cnpj == cnpj);
	}
}
