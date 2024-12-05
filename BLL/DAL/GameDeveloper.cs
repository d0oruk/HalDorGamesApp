using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class GameDeveloper
    {
        //FOR INTERMEDIARY TABLE (MANY TO MANY RELATIONSHIP)
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } //Navigational Property (Many to Many)
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; } //Navigational Property (Many to Many)
    }
}
