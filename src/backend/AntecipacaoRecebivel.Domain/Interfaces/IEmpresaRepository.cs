using AntecipacaoRecebivel.Domain.Entities;

namespace AntecipacaoRecebivel.Domain.Interfaces;

public interface IEmpresaRepository : IRepositoryBase<Empresa>
{
	Empresa GetByCnpj(string cnpj);
}
