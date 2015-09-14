using System;
using System.Linq;
using System.Linq.Expressions;

public interface IOctoFXRepository<T> : IDisposable where T : class, IId
{
	IQueryable<T> GetAll();
	T GetById(int id);
	IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
	void Add(T entity);
	void Delete(T entity);
	void Update(T entity);
	void Save();
}