using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IDevelopersService
    {
        public IQueryable<DeveloperModel> Query();
        public ServiceBase Create(Developer record);
        public ServiceBase Update(Developer record);
        public ServiceBase Delete(int Id);
    }
    public class DevelopersService : ServiceBase, IDevelopersService
    {
        public DevelopersService(Db db) : base(db)
        {

        }

        public ServiceBase Create(Developer record)
        {
            throw new NotImplementedException();
        }

        public ServiceBase Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DeveloperModel> Query()
        {
            return _db.Developers.OrderBy(d => d.Name).Select(p => new DeveloperModel() { Record = p });

        }

        public ServiceBase Update(Developer record)
        {
            throw new NotImplementedException();
        }
    }
}
