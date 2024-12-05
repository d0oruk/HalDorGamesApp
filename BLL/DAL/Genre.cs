using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Genre
    {
        public int Id { get; set; }
        [Required, StringLength(70)]
        public string Name { get; set; }
        public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>(); //Navigational Property (Many to Many)

    }
}
