using Microsoft.EntityFrameworkCore;

namespace API_Produtos.DAL.DAO
{
    public class BaseDAO<T> : IDAO<T> where T : class
    {
        public ProdutosContext _ProdutosContext { get; set; }

        public BaseDAO()
        {
            _ProdutosContext = new ProdutosContext();
        }
        public T Create(T obj)
        {
            _ProdutosContext.Add(obj);
            _ProdutosContext.SaveChanges();

            return obj;
        }

        public void Delete(long id)
        {
            using (_ProdutosContext = new ProdutosContext())
            {
                var obj = Get(id);

                if (obj != null)
                {
                    _ProdutosContext.Remove(obj);
                    _ProdutosContext.SaveChanges();
                }
            }
        }

        public T Get(long id)
        {
            var obj = _ProdutosContext.Set<T>().Find(id);
            if (obj == null)
            {
                throw new OperationCanceledException("Could not find any with the given Id");
            }
            return obj;
        }

        public IEnumerable<T> GetAll()
        {
            var _context = new ProdutosContext();
            return _context.Set<T>();
        }

        public T Update(T obj)
        {
            using (_ProdutosContext = new ProdutosContext())
            {
                _ProdutosContext.Entry(obj).State = EntityState.Modified;
                _ProdutosContext.SaveChanges();

                return obj;
            }
        }
    }
}
