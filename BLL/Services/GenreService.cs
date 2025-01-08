using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public interface IGenreService
    {
        public IQueryable<GenreModel> Query(); //READ
        public ServiceBase Create(Genre record); //CREATE
        public ServiceBase Update(Genre record); //UPDATE
        public ServiceBase Delete(int Id); //DELETE
    }

    public class GenreService : ServiceBase, IGenreService
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                return Error("Genre with the same name exists!");
            }
            record.Name = record.Name.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre created successfully!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.GameGenres)
                .SingleOrDefault(g => g.Id == id);

            if (entity == null)
            {
                return Error("Genre couldn't be found!");
            }

            if (entity.GameGenres != null && entity.GameGenres.Any())
            {
                return Error("Genre cannot be deleted because it has related games!");
            }

            _db.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully!");
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(g => g.Name)
                .Select(g => new GenreModel() { Record = g });
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(g => g.Id != record.Id && g.Name.ToUpper() == record.Name.ToUpper().Trim()))
            {
                return Error("Genre with the same name exists! You cannot update with this name!");
            }
            var entity = _db.Genres.SingleOrDefault(g => g.Id == record.Id);
            
            if (entity == null)
            {
                return Error("Genre couldn't be found!");
            }
            entity.Name = record.Name.Trim();
            _db.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully!");
        }
    }
}

