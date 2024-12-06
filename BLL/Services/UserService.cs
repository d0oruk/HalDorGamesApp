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
    public interface IUserService
    {
        public IQueryable<UserModel> Query(); //READ
        public ServiceBase Create(User record); //CREATE
        public ServiceBase Update(User record); //UPDATE
        public ServiceBase Delete(int Id); //DELETE

        //4 Main Methods To Manage CRUD Operations in an interface, in a Service


    }
    public class UserService : ServiceBase, IUserService
    {
        public UserService(Db db) : base(db)
        {
        }

        public ServiceBase Create(User record)
        {
            if (_db.Users.Any(p => p.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
            //Recorddaki Name'lerin Herhangi Biri, Recorddaki Name ile Aynı ise
            {
                return Error("Username with the same name exists!");
            }
            record.UserName = record.UserName.Trim(); //record'daki Boşlukları Siler
            _db.Users.Add(record);
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Eder.
            return Success("User created successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users.SingleOrDefault(g => g.Id == id);

            if (entity == null)
            {
                return Error("User couldn't be found!");
            }

            _db.Remove(entity);
            _db.SaveChanges();
            return Success("User deleted successfully!");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.OrderBy(p => p.UserName).Select(p => new UserModel() { Record = p });
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(p => p.Id != record.Id && p.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
            //Veritabanında Aynı Adda (Şu An Update Ettiğimiz Id Dışındakiler Kontrol Ediliyor) Başka Bir Veri Var Mı Kontrolü
            {
                Error("User with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Users.SingleOrDefault(s => s.Id == record.Id);
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                //Record, Id ile Bulunamadı. Bu Id'de Veri Yok.
                Error("User couldn't be found!");
            }
            entity.UserName = record.UserName.Trim(); //Oluşturmuş Olduğumuz Entity'nin Name'i, Record'un Name'i ile Değiştirildi.
            _db.Update(entity); //Veritabanı, Ayno Id'deki, Fakat Güncellenmiş İsimdeki Bu Entity ile Güncellendi.
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Edildi. 
            return Success("User updated successfully!");
        }
    }
}
