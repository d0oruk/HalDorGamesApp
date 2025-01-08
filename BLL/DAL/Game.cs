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
        
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        
        public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
        public List<GameDeveloper> GameDevelopers { get; set; }
    }
}
