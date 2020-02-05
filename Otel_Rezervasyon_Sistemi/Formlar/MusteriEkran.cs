using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Rezervasyon_Sistemi.Formlar
{
    public partial class MusteriEkran : Form
    {
        public MusteriEkran()
        {
            InitializeComponent();
        }
        public static string adSoyadX;
        RezervasyonYap RezervasyonYap;
        MusteriRezervasyonlar MusteriRezervasyonlar;

        private void btnRezervasyonYap_Click(object sender, EventArgs e)
        {
            RezervasyonYap = new RezervasyonYap();
            RezervasyonYap.Show();
            RezervasyonYap.BringToFront();
            this.Visible = false;
        }

        private void btnGecmisRez_Click(object sender, EventArgs e)
        {
            MusteriRezervasyonlar = new MusteriRezervasyonlar();
            MusteriRezervasyonlar.Show();
            MusteriRezervasyonlar.BringToFront();
            MusteriRezervasyonlar.MusteriEkran = this;
            this.Visible = false;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool Move;
        int Mouse_X;
        int Mouse_Y;
        private void MusteriEkran_MouseUp(object sender, MouseEventArgs e)
        {
            Move = false;
        }

        private void MusteriEkran_MouseDown(object sender, MouseEventArgs e)
        {
            Move = true;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void MusteriEkran_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void MusteriEkran_Load(object sender, EventArgs e)
        {
            lblHosgeldinX.Text = "Hoşgeldin, " + adSoyadX;
        }
    }
}
