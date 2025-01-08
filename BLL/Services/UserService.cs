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
            return _db.Users
                .Include(u => u.Role)
                .OrderBy(p => p.UserName)
                .Select(p => new UserModel() { Record = p });
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(p => p.Id != record.Id && p.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
            {
                return Error("User with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Users.SingleOrDefault(s => s.Id == record.Id);
            if (entity == null)
            {
                return Error("User couldn't be found!");
            }
            
            // Update all relevant properties
            entity.UserName = record.UserName.Trim();
            entity.Password = record.Password;
            entity.IsActive = record.IsActive;
            entity.RoleId = record.RoleId;
            
            _db.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully!");
        }
    }
}
