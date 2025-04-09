using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.CKK.DB.Interfaces
{
    public interface IGenericRepository<TRepo> where TRepo : class
    {
        TRepo GetById(int id);
        List<TRepo> GetAll();
        int Add(TRepo entity);
        int Update(TRepo entity);
        int Delete(int id); 
    }
    
}
