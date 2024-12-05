using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class GameGenre
    {
        //FOR INTERMEDIARY TABLE (MANY TO MANY RELATIONSHIP)
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } //Navigational Property (Many to Many)
        public int GenreId { get; set; }
        public Genre Genre { get; set; } //Navigational Property (Many to Many)

    }
}
