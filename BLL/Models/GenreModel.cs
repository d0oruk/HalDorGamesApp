using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class GenreModel
    {
        public Genre Record { get; set; }
        public string Name => Record.Name;
        
        [DisplayName("Games")]
        public string Games => Record.GameGenres == null || !Record.GameGenres.Any()
            ? "N/A"
            : string.Join(", ", Record.GameGenres.Select(gg => gg.Game.Name));
    }
}