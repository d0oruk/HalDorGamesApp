using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query(); //READ
        public ServiceBase Create(Role record); //CREATE
        public ServiceBase Update(Role record); //UPDATE
        public ServiceBase Delete(int Id); //DELETE

        //4 Main Methods To Manage CRUD Operations in an interface, in a Service


    }
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }
        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(p => p.Name).Select(p => new RoleModel() { Record = p });
            //Öncelikle Publishers Db Set'e Erişiyor, Sonra İsme Göre Sıralıyor, Sonra ise Sonuçları Teker Teker Record'a Atıyor.
        }
        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Recorddaki Name'lerin Herhangi Biri, Recorddaki Name ile Aynı ise
            {
                return Error("Role with the same name exists!");
            }
            record.Name = record.Name.Trim(); //record'daki Boşlukları Siler
            _db.Roles.Add(record);
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Eder.
            return Success("Role created successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(p => p.Users).SingleOrDefault(p => p.Id == id);
            //*** Entity'deki Navigational Property'lere Erişmek için: Erişmek İstediğimiz Property'leri "Include" ile Belirtmeliyiz. ***
            //*** BUNA "EAGER LOADING" DENİR.
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                return Error("Role couldn't be found!");
            }
            if (entity.Users.Count() > 0) // if(entity.Games.Any())    de Alternatif olarak kullanılabilir.
            //Veritabanında, Publisher Tablosunda Olup, İçerisinde Oyun Olan Publisher Var ise
            {
                return Error("User couldn't be deleted! There are games related with the publisher!");
            }
            _db.Remove(entity);
            _db.SaveChanges();
            return Success("User deleted successfully!");
        }


        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Veritabanında Aynı Adda (Şu An Update Ettiğimiz Id Dışındakiler Kontrol Ediliyor) Başka Bir Veri Var Mı Kontrolü
            {
                Error("Role with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Roles.SingleOrDefault(s => s.Id == record.Id);
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                //Record, Id ile Bulunamadı. Bu Id'de Veri Yok.
                Error("Role couldn't be found!");
            }
            entity.Name = record.Name.Trim(); //Oluşturmuş Olduğumuz Entity'nin Name'i, Record'un Name'i ile Değiştirildi.
            _db.Update(entity); //Veritabanı, Ayno Id'deki, Fakat Güncellenmiş İsimdeki Bu Entity ile Güncellendi.
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Edildi. 
            return Success("Role updated successfully!");
        }
    }
}
