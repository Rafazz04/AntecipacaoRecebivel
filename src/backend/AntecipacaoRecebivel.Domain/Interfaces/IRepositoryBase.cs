namespace AntecipacaoRecebivel.Domain.Interfaces;

public interface IRepositoryBase<Entity> where Entity : class
{
	IEnumerable<Entity> GetAll();
	Entity GetById(int id);
	Entity Create(Entity entity);
	Entity Update(Entity entity);
	Entity Delete(Entity entity); 
	bool SaveChanges();
}
