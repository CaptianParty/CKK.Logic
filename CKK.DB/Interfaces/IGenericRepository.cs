using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.Interfaces
{
    public interface IGenericRepository<TRepo>
    {
        TRepo GetById(int id);
        List<TRepo> GetAll();
        int Add(TRepo entity);
        int Update(TRepo entity);
        //int Delete(int id);
        
        //IMPLEMENTED NEW CODE THAT I ADDED/CHANGED
        int Delete(TRepo id);
    }

}
