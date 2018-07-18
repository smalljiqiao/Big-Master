using System.Linq;

namespace BM.Core.Data
{
    public partial interface IRepository<T> where T:BaseEntity
    {
        T GetByPhone(string phone);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Modify(T entity);

        IQueryable<T> Table { get; }
    }
}
