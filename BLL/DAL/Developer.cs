﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Developer
    {
        public int Id { get; set; }
        [Required, StringLength(70)]
        public string Name { get; set; }
        public List<GameDeveloper> GameDevelopers { get; set; } //Navigational Property (Many to Many)
    }
}
