using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Repositories.Interfaces;
using Twitter.Core.Entities.Common;
using Twitter.DAL.Contexts;

namespace Twitter.Business.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        TwitterContext _context { get; }

        public GenericRepository(TwitterContext context)
        {
            _context = context;
        }
        
        public DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll(bool noTracking) =>
           noTracking ? Table.AsNoTracking(): Table;

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.CountAsync(expression) > 0;
        }
    }
}
