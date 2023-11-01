using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;
        //Base bir class yazıyoruz. Bu classı abstract hale getreceğiz.Abstract olması demek new lenememesi demektir.Bu class içerisine implementasyonları yapacağız ve diğer classlar kalıtım yoluyla bu implementasyonları devralacaklar. Ve diğer classlar ihtiyacı olan diğer metotları yazarak işlemlerine devam edebileceklerdir. Dolayısıyla base classımız abstract bir class olacak. Abstract class üzerinden devralacak diğer classların contexte erişim sağlayabilmesi amacıyla private değilde protected olarak context tanımlaması yapılmaktadır.
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);
        

        public void Delete(T entity) => _context.Set<T>().Remove(entity);


        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() : //Değişiklikler izlenmiyor.
            _context.Set<T>();

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? 
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
        

        public void Update(T entity) => _context.Set<T>().Update(entity);
        
    }
}
