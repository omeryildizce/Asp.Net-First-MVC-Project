﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCodeFirst.Entity
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        public string KategoriDetay { get; set; }
        public string KategoriAd { get; set; }
        public ICollection<Urunler> Urunlers { get; set; }
    }
}
