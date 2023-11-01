using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        //Sistem içerisinde yer alan repolara manager üzerinden erişim verilecektir.
        IBookRepository Book { get; }
        void Save(); 
    }
}
