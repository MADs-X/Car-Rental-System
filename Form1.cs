using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace CarRentalSystem
{
    public partial class Form1 : Form
    {
        private MusteriYonetimi _musteriYonetimi;
        private AracYonetimi _aracYonetimi;
        private Dictionary<string, string> _kiralamaKayitlari = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            _musteriYonetimi = new MusteriYonetimi();
            _aracYonetimi = new AracYonetimi();
            AraclariListele();
            MusterileriListele();
            IstatistikleriGuncelle();


            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            listBox2.SelectedIndexChanged += ListBox2_SelectedIndexChanged;
            listBox3.SelectedIndexChanged += ListBox3_SelectedIndexChanged;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string tcNo = listBox1.SelectedItem.ToString().Split('-')[1].Trim();
                var musteri = _musteriYonetimi.Getir(tcNo);
                if (musteri != null)
                {
                    txtAd.Text = musteri.Ad;
                    txtSoyad.Text = musteri.Soyad;
                    txtTelefon.Text = musteri.Telefon;
                    txtTc.Text = musteri.TCNo;
                    txtAdres.Text = musteri.Adres;
                }
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();
                var arac = _aracYonetimi.AracGetir(plaka);
                if (arac != null)
                {
                    txtMarka.Text = arac.Marka;
                    txtModel.Text = arac.Model;
                    txtPlaka.Text = arac.Plaka;
                    textBox2.Text = arac.GunlukKiraBedeli.ToString();
                    comboBox1.SelectedItem = arac.KiradaMi ? "Kirada" : "Boşta";
                    txtKisiSayisi.Text = arac.KisiSayisi.ToString();
                    txtSanziman.Text = arac.SanzimanTipi;
                    txtYakit.Text = arac.YakitTuru;


                    if (arac is Sedan)
                        txtTur.SelectedItem = "Sedan";
                    else if (arac is SUV)
                        txtTur.SelectedItem = "SUV";
                    else if (arac is Kamyonet)
                        txtTur.SelectedItem = "Kamyonet";
                }
            }
        }


        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                string[] parcalar = listBox3.SelectedItem.ToString().Split('-', '(', ')');
                string musteriBilgisi = parcalar[1].Trim();
                string plaka = parcalar[3].Trim();

                var musteri = _musteriYonetimi.Getir(musteriBilgisi);
                if (musteri != null)
                {
                    txtAd.Text = musteri.Ad;
                    txtSoyad.Text = musteri.Soyad;
                    txtTelefon.Text = musteri.Telefon;
                    txtTc.Text = musteri.TCNo;
                    txtAdres.Text = musteri.Adres;
                }

                var arac = _aracYonetimi.AracGetir(plaka);
                if (arac != null)
                {
                    txtMarka.Text = arac.Marka;
                    txtModel.Text = arac.Model;
                    txtPlaka.Text = arac.Plaka;
                    textBox2.Text = arac.GunlukKiraBedeli.ToString();
                    comboBox1.SelectedItem = arac.KiradaMi ? "Kirada" : "Boşta";
                    txtKisiSayisi.Text = arac.KisiSayisi.ToString();
                    txtSanziman.Text = arac.SanzimanTipi;
                    txtYakit.Text = arac.YakitTuru;

                    if (arac is Sedan)
                        txtTur.SelectedItem = "Sedan";
                    else if (arac is SUV)
                        txtTur.SelectedItem = "SUV";
                    else if (arac is Kamyonet)
                        txtTur.SelectedItem = "Kamyonet";
                }
                ListBoxtaVeriyiSec(listBox2, plaka);
                ListBoxtaVeriyiSec(listBox1, musteriBilgisi);

            }
        }
        public void ListBoxtaVeriyiSec(ListBox listBox, string aranan)
        {
            string kucukAranan = aranan.ToLower();

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                string item = listBox.Items[i].ToString().ToLower();

                if (item.Contains(kucukAranan))
                {
                    listBox.SelectedIndex = i;
                    return;
                }
            }
        }


        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtTc.Text) || string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata");
                return;
            }

            Musteri yeniMusteri = new Musteri
            {
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Telefon = txtTelefon.Text,
                TCNo = txtTc.Text,
                Adres = txtAdres.Text
            };

            if (_musteriYonetimi.Ekle(yeniMusteri))
            {
                MessageBox.Show("Müşteri başarıyla kaydedildi.", "Başarılı");
                MusterileriListele();
                MusteriFormuTemizle();
            }
            else
            {
                MessageBox.Show("Bu TC No ile zaten bir müşteri kayıtlı.", "Hata");
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            MusteriFormuTemizle();
            MusterileriListele();
        }

        private void MusterileriListele()
        {
            listBox1.Items.Clear();
            foreach (var musteri in _musteriYonetimi.Musteriler)
            {
                listBox1.Items.Add($"{musteri.Ad} {musteri.Soyad} - {musteri.TCNo}");
              
            }
        }

        private void MusteriFormuTemizle()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            txtTc.Clear();
            txtAdres.Clear();
        }

        private void btnAracEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMarka.Text) || string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtPlaka.Text) || txtTur.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(txtSanziman.Text)
                || string.IsNullOrWhiteSpace(txtYakit.Text) || string.IsNullOrWhiteSpace(txtKisiSayisi.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Hata");
                return;
            }

            Arac yeniArac = AracOlustur();
            if (yeniArac == null) return;



            if (_aracYonetimi.Ekle(yeniArac))
            {
                MessageBox.Show("Araç başarıyla kaydedildi.", "Başarılı");
                AraclariListele();
                IstatistikleriGuncelle();
                AracFormuTemizle();
            }
        }

        private void btnAracGuncelle_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir araç seçin.", "Hata");
                return;
            }

            if (!AracBilgileriGecerliMi()) return;

            string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();
            var arac = _aracYonetimi.AracGetir(plaka);
            if (arac == null) return;

            bool eskiDurum = arac.KiradaMi;
            arac.Marka = txtMarka.Text;
            arac.Model = txtModel.Text;
            arac.Plaka = txtPlaka.Text;
            arac.YakitTuru = txtYakit.Text;
            arac.SanzimanTipi = txtSanziman.Text;
            arac.KisiSayisi = Convert.ToInt32(txtKisiSayisi.Text);
            arac.GunlukKiraBedeli = int.Parse(textBox2.Text);
            arac.KiradaMi = comboBox1.SelectedItem?.ToString() == "Kirada";

            if (!arac.KiradaMi && eskiDurum)
            {
                _kiralamaKayitlari.Remove(arac.Plaka);
            }

            AraclariListele();
            IstatistikleriGuncelle();
            KiralamaListesiniGuncelle();
            MessageBox.Show("Araç bilgileri güncellendi.", "Başarılı");
        }

        private void btnAracSil_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir araç seçin.", "Hata");
                return;
            }

            string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();
            var arac = _aracYonetimi.AracGetir(plaka); 
            if (arac == null) return;

            if (_aracYonetimi.Sil(arac))
            {
                AraclariListele();
                IstatistikleriGuncelle();
                MessageBox.Show("Araç başarıyla silindi.", "Başarılı");
            }
        }

        private void btnBosAraclar_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            foreach (var arac in _aracYonetimi.Araclar.Where(a => !a.KiradaMi))
            {
                listBox2.Items.Add($"{arac.Marka} {arac.Model} - {arac.Plaka}");
            }
        }

        private void AraclariListele()
        {
            listBox2.Items.Clear();
            foreach (var arac in _aracYonetimi.Araclar)
            {
                listBox2.Items.Add($"{arac.Marka} {arac.Model} - {arac.Plaka}");
               
            }
        }

        private void AracFormuTemizle()
        {
            txtMarka.Clear();
            txtModel.Clear();
            txtPlaka.Clear();
            textBox2.Clear();
            txtSanziman.Clear();
            txtYakit.Clear();
            txtKisiSayisi.Clear();
            txtTur.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;

        }

        private bool AracBilgileriGecerliMi()
        {
            if (string.IsNullOrWhiteSpace(txtMarka.Text) ||
                string.IsNullOrWhiteSpace(txtPlaka.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata");
                return false;
            }

            if (!int.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Günlük kira bedeli sayısal bir değer olmalıdır.", "Hata");
                return false;
            }



            return true;
        }

        private Arac AracOlustur()
        {
            Arac yeniArac = null;
            switch (txtTur.SelectedItem?.ToString())
            {
                case "Sedan":
                    yeniArac = new Sedan();
                    break;
                case "SUV":
                    yeniArac = new SUV();
                    break;
                case "Kamyonet":
                    yeniArac = new Kamyonet();
                    break;
                default:
                    MessageBox.Show("Lütfen araç türünü seçin.", "Hata");
                    return null;
            }

            yeniArac.Marka = txtMarka.Text;
            yeniArac.Model = txtModel.Text;
            yeniArac.Plaka = txtPlaka.Text;
            yeniArac.GunlukKiraBedeli = int.Parse(textBox2.Text);
            yeniArac.KiradaMi = comboBox1.SelectedItem?.ToString() == "Kirada";
            yeniArac.YakitTuru = txtYakit.Text;
            yeniArac.KisiSayisi = Convert.ToInt32(txtKisiSayisi.Text);
            yeniArac.SanzimanTipi = txtSanziman.Text;


            return yeniArac;
        }


        private void btnKirala_Click(object sender, EventArgs e)
        {
            if (!KiralamaKontrol()) return;

            string tcNo = listBox1.SelectedItem.ToString().Split('-')[1].Trim();
            string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();

            var musteri = _musteriYonetimi.Getir(tcNo);
            var arac = _aracYonetimi.Araclar.FirstOrDefault(a => a.Plaka == plaka);

            arac.Kirala();
            _kiralamaKayitlari[plaka] = tcNo;
            AraclariListele();
            IstatistikleriGuncelle();
            KiralamaListesiniGuncelle();
            MessageBox.Show($"{arac.Marka} {arac.Model} aracı {musteri.Ad} {musteri.Soyad} adlı müşteriye kiralandı.\nToplam Ücret: {textBox1.Text} TL",
                "Başarılı");
        }


        private bool KiralamaKontrol()
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.", "Hata");
                return false;
            }

            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir araç seçin.", "Hata");
                return false;
            }

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Lütfen geçerli tarih aralığı seçin.", "Hata");
                return false;
            }

            string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();
            var arac = _aracYonetimi.AracGetir(plaka); ;

            if (arac.KiradaMi)
            {
                MessageBox.Show("Bu araç şu anda kirada!", "Hata");
                return false;
            }

            return true;
        }

        private void KiralamaListesiniGuncelle()
        {
            listBox3.Items.Clear();
            foreach (var kayit in _kiralamaKayitlari)
            {
                var arac = _aracYonetimi.AracGetir(kayit.Key); ;
                var musteri = _musteriYonetimi.Getir(kayit.Value);
                if (arac != null && musteri != null)
                {
                    listBox3.Items.Add($"{musteri.Ad} {musteri.Soyad}-{musteri.TCNo} - {arac.Marka} {arac.Model} ({arac.Plaka})");
                }
            }
        }



        private void IstatistikleriGuncelle()
        {
            var enCokKiralananArac = _aracYonetimi.Araclar
                .OrderByDescending(a => a.KiralamaSayisi)
                .FirstOrDefault();
            textBox3.Text = enCokKiralananArac != null ? $"{enCokKiralananArac.Marka} {enCokKiralananArac.Model}" : "Veri yok";

            int kiralikAracSayisi = _aracYonetimi.Araclar.Count(a => a.KiradaMi);
            textBox4.Text = kiralikAracSayisi.ToString();
            textBox5.Text = _aracYonetimi.Araclar.Count.ToString();
        }

        private void HesaplaKiraUcreti(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null && dateTimePicker1.Value < dateTimePicker2.Value)
            {
                string plaka = listBox2.SelectedItem.ToString().Split('-')[1].Trim();
                var arac = _aracYonetimi.AracGetir( plaka);
                if (arac != null)
                {
                    int gunSayisi = (int)(dateTimePicker2.Value - dateTimePicker1.Value).TotalDays+1;
                    int toplamUcret = arac.GunlukKiraBedeli * (gunSayisi);
                    textBox1.Text = toplamUcret.ToString();
                }
            }
            else
            {
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AraclariListele();
        }

      
    }


 
}

