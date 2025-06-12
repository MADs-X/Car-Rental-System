using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    class MusteriYonetimi
    {
        public List<Musteri> Musteriler { get; private set; } = new List<Musteri>();

        public bool Ekle(Musteri musteri)
        {
            if (!Musteriler.Any(m => m.TCNo == musteri.TCNo))
            {
                Musteriler.Add(musteri);
                return true;
            }
            return false;
        }

        public Musteri Getir(string tcNo)
        {
            return Musteriler.FirstOrDefault(m => m.TCNo == tcNo);
        }
    }
}
