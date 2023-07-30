using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Produtos.DAL.DAO
{
    public interface IDAO<T> where T : class
    {
        T Create(T obj);
        T Update(T obj);
        void Delete(T obj);
        void Delete(long id);
        T Get(long id);
        IEnumerable<T> GetAll();
    }
}
