using Otel_Rezervasyon_Sistemi.BLL;
using Otel_Rezervasyon_Sistemi.EF;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Otel_Rezervasyon_Sistemi.Formlar
{
    public partial class GirisYap : Form
    {
        List<KayitliUye> kayitliUye = new List<KayitliUye>();
        List<Yoneticiler> kayitliYonetici = new List<Yoneticiler>();
        KayitliUyeController KayitliUyeController = new KayitliUyeController();
        MusteriEkran MusteriEkran;
        RezervasyonYap RezervasyonYap;
        MusteriRezervasyonlar MusteriRezervasyonlar;
        Yonetici Yonetici;
        YoneticiController YoneticiController = new YoneticiController();
        public AnaEkran AnaEkran;
       
        //TODO: Bu sayfada ekstra bilgiler istenilecek

        bool _girisOnay;

        public GirisYap()
        {
            InitializeComponent();
        }
       

        private void btnGeriDon_Click_1(object sender, EventArgs e)
        {
            AnaEkran.Visible = true;
            AnaEkran.Show();
            AnaEkran.BringToFront();
            this.Close();
        }

        private void btnGirisYap_Click_1(object sender, EventArgs e)
        {
            kayitliUye = KayitliUyeController.KayitliUyeleriGetir();
            kayitliYonetici = YoneticiController.YoneticileriGetir();

            foreach (var item in kayitliUye)
            {
                if (item.Mail == txtEmail.Text && item.Sifre == txtSifre.Text)
                {
                    MusteriEkran = new MusteriEkran();
                    RezervasyonYap = new RezervasyonYap();
                    MusteriRezervasyonlar = new MusteriRezervasyonlar();
                    MusteriEkran.adSoyadX = item.Ad + " " + item.Soyad;
                    MusteriEkran.Show();
                    MusteriEkran.BringToFront();
                    RezervasyonYap.kayitliUyeID = item.KayitliUyeID;
                    MusteriRezervasyonlar._kayitliUyeID = item.KayitliUyeID;
                    this.Visible = false;
                    _girisOnay = true;
                    break;
                }
            }
            foreach (var item1 in kayitliYonetici)
            {
                if (txtEmail.Text == item1.Email && txtSifre.Text == item1.Sifre)
                {
                    Yonetici = new Yonetici();
                    Yonetici.Show();
                    Yonetici.BringToFront();
                    this.Visible = false;
                    _girisOnay = true;
                    break;
                }
            }
            if (_girisOnay == false)
            {
                MessageBox.Show("Lütfen Bilgileri Kontrol Edip Tekrar Deneyiniz.!");
            }
        }

        bool Move;
        int Mouse_X;
        int Mouse_Y;
        private void GirisYap_MouseUp(object sender, MouseEventArgs e)
        {
            Move = false;
        }

        private void GirisYap_MouseDown(object sender, MouseEventArgs e)
        {
            Move = true;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void GirisYap_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //karakteri göster.
                txtSifre.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                txtSifre.PasswordChar = '*';
            }
        }
    }
            
}

