using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Twitter.Core.Entities.Common;
using Twitter.DAL.Contexts;

namespace Twitter.Business.Repositories.Interfaces;
public interface IGenericRepository<T> where T: BaseEntity
{
    DbSet<T> Table { get;  }
    IQueryable<T> GetAll(bool noTracking=true);
    Task<bool> IsExistAsync(Expression<Func<T,bool>> expression);

}
