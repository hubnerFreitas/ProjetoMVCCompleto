using AppMvc.Models;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevIO.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext _Db;
        protected readonly DbSet<TEntity> _DbSet;

        public Repository(MeuDbContext db)
        {
            _Db = db;
            _DbSet = _Db.Set<TEntity>();
        }

        public  async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _DbSet.AsNoTracking().ToListAsync();
        }


        public virtual async Task Adicionar(TEntity entity)
        {
            _DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            _DbSet.Remove(new TEntity() { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Db?.Dispose();
        }
    }
}
