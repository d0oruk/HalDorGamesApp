using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Game
    {
        public int Id { get; set; }

        [Required, StringLength(70)]
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public Publisher Publisher { get; set; }//Navigational Property (Many Side)
        public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>(); //Navigational Property (Many to Many)
        public List<GameDeveloper> GameDevelopers { get; set; } //Navigational Property (Many to Many)


    }
}
