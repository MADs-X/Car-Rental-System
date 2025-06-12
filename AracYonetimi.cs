using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    class AracYonetimi
    {
        public List<Arac> Araclar { get; private set; } = new List<Arac>();

        public bool Ekle(Arac arac)
        {
            if (!Araclar.Any(a => a.Plaka == arac.Plaka))
            {
                Araclar.Add(arac);
                return true;
            }
            MessageBox.Show("Bu plakaya sahip bir araç zaten mevcut!", "Hata");
            return false;
        }

        public bool Sil(Arac arac)
        {
            if (arac != null && !arac.KiradaMi)
            {
                Araclar.Remove(arac);
                return true;
            }
            MessageBox.Show("Araç bulunamadı veya şu an kirada!", "Hata");
            return false;
        }
        public Arac AracGetir(string plaka)
        {
            return Araclar.FirstOrDefault(a => a.Plaka == plaka);
        }
    }
}
