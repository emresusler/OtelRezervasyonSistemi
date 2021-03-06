﻿using Otel_Rezervasyon_Sistemi.BLL;
using Otel_Rezervasyon_Sistemi.Formlar;
using System;
using System.Windows.Forms;

namespace Otel_Rezervasyon_Sistemi
{
    public partial class AnaEkran : Form
    {

        public AnaEkran()
        {
            InitializeComponent();
        }
        OdaController odaController = new OdaController();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            GirisYap girisYap = new GirisYap();
            girisYap.Show();
            girisYap.BringToFront();
            girisYap.AnaEkran = this;
            this.Visible = false;
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            KayitOL kayitOL = new KayitOL();
            kayitOL.AnaEkran = this;
            kayitOL.Show();
            kayitOL.BringToFront();
            this.Visible = false;
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Date > odaController.CikisTarihleriniGetir())
            {
                odaController.OdaDurumBos();
            }
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool Hareket;
        int Mouse_X;
        int Mouse_Y;

        private void AnaEkran_MouseUp(object sender, MouseEventArgs e)
        {
            Hareket = false;
        }

        private void AnaEkran_MouseDown(object sender, MouseEventArgs e)
        {
            Hareket = true;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void AnaEkran_MouseMove(object sender, MouseEventArgs e)
        {
            if (Hareket)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
    }
}
