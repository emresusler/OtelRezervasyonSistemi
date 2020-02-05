using Otel_Rezervasyon_Sistemi.BLL;
using Otel_Rezervasyon_Sistemi.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Rezervasyon_Sistemi.Formlar
{
    public partial class KayitOL : Form
    {
        public KayitOL()
        {
            InitializeComponent();
        }
        KayitliUye kayitliUye;
        KayitliUyeController KayitliUyeController = new KayitliUyeController();
        YoneticiController yoneticiController = new YoneticiController();
        Regex regexItem = new Regex("^[a-zA-Z0-9]*$");
        public AnaEkran AnaEkran;


        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            txtEmail.Text = Regex.Replace(txtEmail.Text, @"\s+", "");

            if (!string.IsNullOrWhiteSpace(txtAd.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtSoyad.Text))
                {
                    if ((txtEmail.Text.Contains("@gmail.com") || txtEmail.Text.Contains("@hotmail.com")) && !string.IsNullOrWhiteSpace(txtEmail.Text.Split('@')[0]))
                    {
                        if (txtSifre.Text.Length >= 8 && txtSifre.Text.Length <= 16 && txtSifreTekrar.Text.Length >= 8 && txtSifreTekrar.Text.Length <= 16)
                        {
                            if (regexItem.IsMatch(txtSifre.Text) && regexItem.IsMatch(txtSifreTekrar.Text))
                            {
                                if (txtSifre.Text == txtSifreTekrar.Text)
                                {
                                    if ((txtEmail.Text != KayitliUyeController.KayitliUyeMail(txtEmail.Text)) && !yoneticiController.YoneticiMailKontrol(txtEmail.Text))
                                    {
                                        kayitliUye = new KayitliUye();
                                        kayitliUye.Mail = txtEmail.Text;
                                        kayitliUye.Sifre = txtSifre.Text;
                                        kayitliUye.Ad = txtAd.Text;
                                        kayitliUye.Soyad = txtSoyad.Text;
                                        KayitliUyeController.KayitliUyeEkle(kayitliUye);
                                        Temizle();
                                        MessageBox.Show("Başarıyla Kayıt Oldunuz.");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Girdiğiniz Bilgilerde Üye Bulunmaktadır.");
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Uyumsuz şifre girişi.");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Lütfen Şifrenizde Özel Karakter Kullanmayınız.!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lütfen 8 ile 16 haneli şifre giriniz.!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Mail Adresinizi Kontrol Edip Tekrar Deneyinizi.");

                    }
                }
                else
                {
                    MessageBox.Show("Lütfen soyadınızı giriniz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen adınızı giriniz.");
            }
        }

        void Temizle()
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = (TextBox)item;
                    txt.Clear();
                }
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            AnaEkran.Visible = true;
            AnaEkran.Show();
            AnaEkran.BringToFront();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //karakteri göster.
                txtSifre.PasswordChar = '\0';
                txtSifreTekrar.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                txtSifre.PasswordChar = '*';
                txtSifreTekrar.PasswordChar = '*';
            }
        }
        bool Move;
        int Mouse_X;
        int Mouse_Y;
        private void KayitOL_MouseUp(object sender, MouseEventArgs e)
        {
            Move = false;
        }

        private void KayitOL_MouseDown(object sender, MouseEventArgs e)
        {
            Move = true;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void KayitOL_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void SadeceHarf(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }
    }
}
