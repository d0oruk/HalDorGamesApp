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
    public interface IGameService
    {
        public IQueryable<GameModel> Query(); //READ
        public ServiceBase Create(Game record); //CREATE
        public ServiceBase Update(Game record); //UPDATE
        public ServiceBase Delete(int Id); //DELETE

        //4 Main Methods To Manage CRUD Operations in an interface, in a Service


    }
    public class GameService : ServiceBase, IGameService
    {
        public GameService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Game record)
        {
            if (_db.Games.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Recorddaki Name'lerin Herhangi Biri, Recorddaki Name ile Aynı ise
            {
                return Error("Game with the same name exists!");
            }
            record.Name = record.Name.Trim(); //record'daki Boşlukları Siler
            _db.Games.Add(record);
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Eder.
            return Success("Game created successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Games.SingleOrDefault(g => g.Id == id);

            if (entity == null)
            {
                Error("Game couldn't be found!");
            }

            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Game deleted successfully!");
        }

        public IQueryable<GameModel> Query()
        {
            return _db.Games.OrderBy(p => p.Name).Select(p => new GameModel() { Record = p });
            //Öncelikle Games Db Set'e Erişiyor, Sonra İsme Göre Sıralıyor, Sonra ise Sonuçları Teker Teker Record'a Atıyor.
        }

        public ServiceBase Update(Game record)
        {
            if (_db.Games.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Veritabanında Aynı Adda (Şu An Update Ettiğimiz Id Dışındakiler Kontrol Ediliyor) Başka Bir Veri Var Mı Kontrolü
            {
                Error("Game with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Games.SingleOrDefault(s => s.Id == record.Id);
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                //Record, Id ile Bulunamadı. Bu Id'de Veri Yok.
                Error("Game couldn't be found!");
            }
            entity.Name = record.Name.Trim(); //Oluşturmuş Olduğumuz Entity'nin Name'i, Record'un Name'i ile Değiştirildi.
            _db.Update(entity); //Veritabanı, Ayno Id'deki, Fakat Güncellenmiş İsimdeki Bu Entity ile Güncellendi.
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Edildi. 
            return Success("Game updated successfully!");
        }

        
    }
}
