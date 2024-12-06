using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GameModel
    {
        public Game Record { get; set; }
        public string Name => Record.Name;
        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");
        [DisplayName("Price")]
        public string Price => Record.Price.ToString("F2");

        //public string Publisher => Record.Publisher?.Name;
        public string Publisher => Record.Publisher?.Name ?? "N/A";
        [DisplayName("Genres")]

        //public string Genres => Record.GameGenres == null
        //? string.Empty
        //: string.Join(", ", Record.GameGenres.Select(gg => gg.Genre.Name));
        public string Genres => Record.GameGenres == null || !Record.GameGenres.Any()
    ? "N/A"
    : string.Join(", ", Record.GameGenres.Select(gg => gg.Genre.Name));
    }
}
