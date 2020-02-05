using Otel_Rezervasyon_Sistemi.BLL;
using Otel_Rezervasyon_Sistemi.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Otel_Rezervasyon_Sistemi.Formlar
{
    public partial class RezervasyonYap : Form
    {
        Rezervasyon rezervasyon;
        public static int kayitliUyeID;
        RezervasyonController RezervasyonController = new RezervasyonController();
        RezervasyonTipiController RezervasyonTipiController = new RezervasyonTipiController();
        FiyatController fiyatController = new FiyatController();
        Musteri musteri;
        MusteriController MusteriController = new MusteriController();
        OdaController OdaController = new OdaController();
        MusteriEkran MusteriEkran = new MusteriEkran();

        public Yonetici Yonetici;

        decimal sabitFiyat,
         haftaSonuFiyat,
         haftaSonuFiyatOran,
         tekKisilikOdaFiyat,
         tekKisilikOdaFiyatOran,
         kisiSayisiUcFiyat,
         kisiSayisiUcFiyatOran,
         fullOdaFiyat,
         fullArtıFullOdaFiyat,
         fullOdaOran,
         fullArtıFullOdaOran,
         fiyat,
         toplamTutar;
        int c = 0;

        private void cmbRezervasyonTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= cmbRezervasyonTipi.Items.Count; i++)
            {
                if (cmbRezervasyonTipi.SelectedIndex == i - 1)
                {
                    lblRezTipAcıklama.Text = RezervasyonTipiController.RezervasyonTipiAcıklamaGetir(i).ToString();

                }
            }
            FiyatHesapla();
        }

        public RezervasyonYap()
        {
            InitializeComponent();

        }

        private void RezervasyonYap_Load(object sender, EventArgs e)
        {
            nudKisiSayisi.Minimum = 1;

            sabitFiyat = fiyatController.FiyatOranGetir(1);
            tekKisilikOdaFiyatOran = fiyatController.FiyatOranGetir(2);
            kisiSayisiUcFiyatOran = fiyatController.FiyatOranGetir(3);
            haftaSonuFiyatOran = fiyatController.FiyatOranGetir(4);
            fullOdaOran = fiyatController.FiyatOranGetir(5);
            fullArtıFullOdaOran = fiyatController.FiyatOranGetir(6);

            lblYatakSayisi.Text = nudKisiSayisi.Value.ToString();
            lblFiyat.Text = string.Empty;

            nudKisiSayisi.Minimum = 1;
            nudKisiSayisi.Maximum = 6;

            dtpGirisTarihi.MinDate = DateTime.Now;
            dtpCikisTarihi.MinDate = dtpGirisTarihi.MinDate.AddDays(1);

            cmbRezervasyonTipi.DataSource = RezervasyonTipiController.RezervasyonTipleriniGetir();
            cmbRezervasyonTipi.DisplayMember = "Tipi";
            cmbRezervasyonTipi.ValueMember = "RezervasyonTipiId";

            BosOdalariGetir();
            FiyatHesapla();
        }

        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            rezervasyon = new Rezervasyon();

            if (lblFiyat.Text == "0")
            {
                FiyatHesapla();
            }

            DialogResult result = MessageBox.Show("Rezervasyonu onaylıyor musunuz ?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                do
                {

                    try
                    {
                        if (dtpGirisTarihi.Value.Date >= DateTime.Now.Date)
                        {
                            rezervasyon.GirisTarihi = dtpGirisTarihi.Value;
                        }
                        else
                        {
                            throw new Exception("Giriş Tarihini Kontrol Ediniz.!");

                        }
                        if (dtpCikisTarihi.Value.Date > dtpGirisTarihi.Value.Date)
                        {
                            rezervasyon.CikisTarihi = dtpCikisTarihi.Value;
                        }
                        else
                        {
                            throw new Exception("Çıkış Tarihini Kontrol Ediniz.!");
                        }
                        if (cmbOdaNo.SelectedIndex==-1)
                        {
                            throw new Exception("Boş odamız kalmamıştır. Lütfen daha sonra tekrar rezervasyon yapmayı deneyiniz.");
                        }
                        rezervasyon.KayitliUyeID = kayitliUyeID;
                        rezervasyon.KisiSayisi = (int)nudKisiSayisi.Value;
                        rezervasyon.OdaID = (int)cmbOdaNo.SelectedValue;
                        rezervasyon.RezervasyonTipiID = (int)cmbRezervasyonTipi.SelectedValue;
                        rezervasyon.OdaSayisi = (int)nudOdaSayisi.Value;
                        rezervasyon.YatakSayisi = Convert.ToInt32(lblYatakSayisi.Text);
                        rezervasyon.Fiyat = toplamTutar;

                        DinamikTextbox();

                        if (OdaController.OdaDurum((int)cmbOdaNo.SelectedValue) == false)
                        {
                            OdaController.OdaDurumGuncelle((int)cmbOdaNo.SelectedValue);
                        }
                        else
                        {
                            throw new Exception("Seçtiğiniz Oda Doludur.Lütfen Başka Oda Seçiniz.!");
                        }

                    }
                    catch (Exception ex)
                    {
                        AdList.Clear();
                        SoyadList.Clear();
                        TcList.Clear();
                        MusterilerList.Clear();
                        MessageBox.Show(ex.Message);
                        break;
                    }
                    foreach (var item in MusterilerList)
                    {
                        MusteriController.MusteriEkle(item);
                    }

                    RezervasyonController.RezervasyonEkle(rezervasyon);

                    MessageBox.Show("Rezervasyonunuz Kaydedilmiştir.");
                    btnRezervasyonYap.Enabled = false;

                    MusteriEkran.Visible = true;
                    MusteriEkran.Show();
                    MusteriEkran.BringToFront();
                    this.Close();
                    break;

                } while (true);
            }


        }

        private void DinamikTextbox()
        {
            for (int i = 0; i < nudKisiSayisi.Value; i++)
            {
                musteri = new Musteri();
                var adGetir = gBoxMusteri.Controls.OfType<TextBox>().Where(x => x.Name.StartsWith("textbox1_")).Select(x => x.Text).ToList();

                foreach (var txtAd in adGetir)
                {
                    AdList.Add(txtAd);
                    musteri.Ad = AdList[i];
                }
                var soyadGetir = gBoxMusteri.Controls.OfType<TextBox>().Where(x => x.Name.StartsWith("textbox2_")).Select(x => x.Text).ToList();

                foreach (var txtSoyad in soyadGetir)
                {
                    SoyadList.Add(txtSoyad);
                    musteri.Soyad = SoyadList[i];
                }
                var tcGetir = gBoxMusteri.Controls.OfType<TextBox>().Where(x => x.Name.StartsWith("textbox3_")).Select(x => x.Text).ToList();

                foreach (var txtTC in tcGetir)
                {
                    TcList.Add(txtTC);
                    musteri.TcNO = TcList[i];
                }
                musteri.OdaId = (int)cmbOdaNo.SelectedValue;

                MusterilerList.Add(musteri);
            }
        }

        List<string> AdList = new List<string>();
        List<string> SoyadList = new List<string>();
        List<string> TcList = new List<string>();
        List<Musteri> MusterilerList = new List<Musteri>();

        private decimal FiyatHesapla()
        {
            //DateTime girisTarihi = new DateTime(rezervasyon.GirisTarihi.Year, rezervasyon.GirisTarihi.Month, rezervasyon.GirisTarihi.Day);
            DateTime girisTarihi = dtpGirisTarihi.Value.Date;
            DateTime cikisTarihi = dtpCikisTarihi.Value.Date;
            //DateTime cikisTarihi = new DateTime(rezervasyon.CikisTarihi.Year, rezervasyon.CikisTarihi.Month, rezervasyon.CikisTarihi.Day);

            toplamTutar = 0; // fiyat göster dedikçe ekliyordu üstüne.Düzeltmek için bu kodu ekledik.

            //kisiSayisiUcFiyat = sabitFiyat * kisiSayisiUcFiyatOran;
            //tekKisilikOdaFiyat = sabitFiyat * tekKisilikOdaFiyatOran;
            //fullArtıFullOdaFiyat = sabitFiyat * fullArtıFullOdaOran;
            //fullOdaFiyat = sabitFiyat * fullOdaOran;
            //haftaSonuFiyat = sabitFiyat * haftaSonuFiyatOran;


            do
            {
                #region Hesaplama
                int seciliRezTipi = 0;
                try
                {
                    seciliRezTipi = (int)cmbRezervasyonTipi.SelectedValue;
                    fiyat = 0;
                }
                catch (Exception)
                {

                }

                if (girisTarihi.DayOfWeek == DayOfWeek.Friday || girisTarihi.DayOfWeek == DayOfWeek.Saturday)
                {
                    switch (seciliRezTipi)
                    {
                        case 1://standart
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * tekKisilikOdaFiyatOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran;
                            }
                            break;
                        case 2://full
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * tekKisilikOdaFiyatOran * fullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * fullOdaOran;
                            }
                            break;
                        case 3://fullartifull
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * tekKisilikOdaFiyatOran * fullArtıFullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * haftaSonuFiyatOran * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * haftaSonuFiyatOran * fullArtıFullOdaOran;
                            }
                            break;
                        default:
                            fiyat = 0;
                            break;
                    }
                }
                else
                {
                    switch (seciliRezTipi)
                    {
                        case 1://standart
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat * kisiSayisiUcFiyatOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat * tekKisilikOdaFiyatOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran;
                                }
                                fiyat += sabitFiyat;
                            }
                            break;
                        case 2://full
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * kisiSayisiUcFiyatOran * fullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * tekKisilikOdaFiyatOran * fullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullOdaOran;
                                }
                                fiyat += sabitFiyat * fullOdaOran;
                            }
                            break;
                        case 3://fullartifull
                            if (nudKisiSayisi.Value % 3 == 0)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 1)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * tekKisilikOdaFiyatOran * fullArtıFullOdaOran;
                            }
                            else if (nudKisiSayisi.Value % 3 == 2)
                            {
                                if (nudKisiSayisi.Value > 3)
                                {
                                    fiyat = sabitFiyat * kisiSayisiUcFiyatOran * fullArtıFullOdaOran;
                                }
                                fiyat += sabitFiyat * fullArtıFullOdaOran;
                            }
                            break;
                        default:
                            fiyat = 0;
                            break;
                    }

                }


                #endregion
                //else
                //{
                //        toplamTutar = 0;
                //        fiyat = 0;
                //        throw new Exception("Lütfen Girdiğiniz Değerleri Kontrol Ediniz.!");
                //}

                toplamTutar += fiyat;
                girisTarihi = girisTarihi.AddDays(1);
            } while (girisTarihi < cikisTarihi);



            lblFiyat.Text = string.Format("{0:0.##}", toplamTutar) + " TL";

            return toplamTutar;
        }

        private void dtpGirisTarihi_ValueChanged(object sender, EventArgs e)
        {
            dtpCikisTarihi.MinDate = dtpGirisTarihi.Value.AddDays(1); // giriş tarihinden önce çıkış tarihini seçemesin
            FiyatHesapla();
        }

        private void dtpCikisTarihi_ValueChanged(object sender, EventArgs e)
        {
            FiyatHesapla();
        }

        TextBox textBox1;
        TextBox textBox2;
        TextBox textBox3;
        private void nudKisiSayisi_ValueChanged(object sender, EventArgs e)
        {
            lblYatakSayisi.Text = nudKisiSayisi.Value.ToString();

            int y = 20;
            if (nudKisiSayisi.Value > c)
            {
                c++;
                textBox1 = new TextBox();
                textBox1.Location = new Point(52, (y * c) * 2);
                textBox1.Size = new Size(100, 25);
                textBox1.Name = "textbox1_" + c.ToString();
                textBox1.MaxLength = 30;
                textBox1.KeyPress += TextBox1_KeyPress;
                textBox1.TextChanged += TextBox1_TextChanged;

                textBox2 = new TextBox();
                textBox2.Location = new Point(214, (y * c) * 2);
                textBox2.Size = new Size(100, 25);
                textBox2.Name = "textbox2_" + c.ToString();
                textBox2.MaxLength = 30;
                textBox2.KeyPress += TextBox2_KeyPress;
                textBox2.TextChanged += TextBox2_TextChanged;


                textBox3 = new TextBox();
                textBox3.Location = new Point(362, (y * c) * 2);
                textBox3.Size = new Size(100, 25);
                textBox3.Name = "textbox3_" + c.ToString();
                textBox3.MaxLength = 11;
                textBox3.KeyPress += TextBox3_KeyPress;


                gBoxMusteri.Controls.Add(textBox1);
                gBoxMusteri.Controls.Add(textBox2);
                gBoxMusteri.Controls.Add(textBox3);
            }
            else
            {
                gBoxMusteri.Controls.RemoveByKey("textbox1_" + c.ToString());
                gBoxMusteri.Controls.RemoveByKey("textbox2_" + c.ToString());
                gBoxMusteri.Controls.RemoveByKey("textbox3_" + c.ToString());
                c--;
            }

            if (nudKisiSayisi.Value <= 3)
            {
                nudOdaSayisi.Maximum = 1;
                nudOdaSayisi.Value = 1;
            }
            else
            {
                nudOdaSayisi.Minimum = 2;
                nudOdaSayisi.Value = 2;
                nudOdaSayisi.Maximum = 2;
            }

            FiyatHesapla();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            char[] v = textBox2.Text.ToCharArray();
            if (v.Length > 0)
            {
                string s = v[0].ToString().ToUpper();
                for (int b = 1; b < v.Length; b++)
                    s += v[b].ToString().ToLower();
                textBox2.Text = s;
            }
            textBox2.Select(textBox2.Text.Length, 0);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            char[] v = textBox1.Text.ToCharArray();
            if (v.Length > 0)
            {
                string s = v[0].ToString().ToUpper();
                for (int b = 1; b < v.Length; b++)
                    s += v[b].ToString().ToLower();
                textBox1.Text = s;
            }
            textBox1.Select(textBox1.Text.Length, 0);


        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }



        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            MusteriEkran.Visible = true;
            MusteriEkran.Show();
            MusteriEkran.BringToFront();
            this.Close();
        }

        void BosOdalariGetir()
        {
                cmbOdaNo.DataSource = OdaController.BosOdalariGetir();
                cmbOdaNo.DisplayMember = "OdaNumarasi";
                cmbOdaNo.ValueMember = "OdaID";
        }

    }
}
