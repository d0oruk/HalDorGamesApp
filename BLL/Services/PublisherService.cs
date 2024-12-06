using Azure.Identity;
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
    public interface IPublisherService
    {
        public IQueryable<PublisherModel> Query(); //READ
        public ServiceBase Create(Publisher record); //CREATE
        public ServiceBase Update(Publisher record); //UPDATE
        public ServiceBase Delete(int Id); //DELETE

        //4 Main Methods To Manage CRUD Operations in an interface, in a Service


    }
    public class PublisherService : ServiceBase, IPublisherService //(INTERFACE)
    {
        public PublisherService(Db db) : base(db) //CONSTRUCTOR CHAINING
        {

        }
        public IQueryable<PublisherModel> Query()
        {
            return _db.Publishers.OrderBy(p => p.Name).Select(p => new PublisherModel() { Record = p });
            //Öncelikle Publishers Db Set'e Erişiyor, Sonra İsme Göre Sıralıyor, Sonra ise Sonuçları Teker Teker Record'a Atıyor.
        }

        public ServiceBase Create(Publisher record)
        {
            if (_db.Publishers.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Recorddaki Name'lerin Herhangi Biri, Recorddaki Name ile Aynı ise
            {
                return Error("Publisher with the same name exists!");
            }
            record.Name = record.Name.Trim(); //record'daki Boşlukları Siler
            _db.Publishers.Add(record);
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Eder.
            return Success("Publisher created successfully!");
        }

        public ServiceBase Update(Publisher record)
        {

            if (_db.Publishers.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
            //Veritabanında Aynı Adda (Şu An Update Ettiğimiz Id Dışındakiler Kontrol Ediliyor) Başka Bir Veri Var Mı Kontrolü
            {
                Error("Publisher with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Publishers.SingleOrDefault(s => s.Id == record.Id);
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                //Record, Id ile Bulunamadı. Bu Id'de Veri Yok.
                Error("Publisher couldn't be found!");
            }
            entity.Name = record.Name.Trim(); //Oluşturmuş Olduğumuz Entity'nin Name'i, Record'un Name'i ile Değiştirildi.
            _db.Update(entity); //Veritabanı, Ayno Id'deki, Fakat Güncellenmiş İsimdeki Bu Entity ile Güncellendi.
            _db.SaveChanges(); //Değişiklikleri Database'e Commit Edildi. 
            return Success("Publisher updated successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Publishers.Include(p => p.Games).SingleOrDefault(p => p.Id == id);
            //*** Entity'deki Navigational Property'lere Erişmek için: Erişmek İstediğimiz Property'leri "Include" ile Belirtmeliyiz. ***
            //*** BUNA "EAGER LOADING" DENİR.
            //SingleOrDefault methodunu kullanarak Veritabanından; Record Id'si ile Aynı Olan Entity'e Eriştik.
            if (entity == null)
            {
                return Error("Publisher couldn't be found!");
            }
            if (entity.Games.Count() > 0) // if(entity.Games.Any())    de Alternatif olarak kullanılabilir.
            //Veritabanında, Publisher Tablosunda Olup, İçerisinde Oyun Olan Publisher Var ise
            {
                return Error("Publisher couldn't be deleted! There are games related with the publisher!");
            }
            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Publisher deleted successfully!");

        }
    }
}