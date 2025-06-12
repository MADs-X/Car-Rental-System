using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{

    public interface IKiralik
    {
        void Kirala();
        void TeslimAl();

    }
    public interface IAracOzellikleri
    {
        string YakitTuru { get; set; }
        string SanzimanTipi { get; set; }
        int KisiSayisi { get; set; }
    }
    public abstract class Arac: IAracOzellikleri,IKiralik
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Plaka { get; set; }
        public int GunlukKiraBedeli { get; set; }
        public bool KiradaMi { get; set; } = false;
        public int KiralamaSayisi { get; private set; } = 0;
        public string YakitTuru { get; set; }
        public string SanzimanTipi { get; set; }
        public int KisiSayisi { get; set; }


        public void Kirala()
        {
            if (!KiradaMi)
            {
                KiradaMi = true;
                KiralamaSayisi++;
               
            }
            else
            {
                Console.WriteLine($"{Plaka} plakalı araç zaten kirada.");
            }
        }

        public void TeslimAl()
        {
            if (KiradaMi)
            {
                KiradaMi = false;
                Console.WriteLine($"{Plaka} plakalı araç teslim alındı.");
            }
        }
    }
    public class Sedan : Arac
    {
        public bool SunroofVarmi { get; set; }

     
       
    }


    public class SUV : Arac
    {
        public bool DortCekerMi { get; set; }

       
    }
    public class Kamyonet : Arac
    {
        public int TasimaKapasitesi { get; set; }
    }

}
