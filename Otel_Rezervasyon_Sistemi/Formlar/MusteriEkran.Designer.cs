namespace Otel_Rezervasyon_Sistemi.Formlar
{
    partial class MusteriEkran
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteriEkran));
            this.btnRezervasyonYap = new System.Windows.Forms.Button();
            this.btnGecmisRez = new System.Windows.Forms.Button();
            this.btnCikis = new System.Windows.Forms.Button();
            this.lblHosgeldinX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRezervasyonYap
            // 
            this.btnRezervasyonYap.BackColor = System.Drawing.Color.Transparent;
            this.btnRezervasyonYap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRezervasyonYap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRezervasyonYap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(145)))), ((int)(((byte)(62)))));
            this.btnRezervasyonYap.Location = new System.Drawing.Point(222, 257);
            this.btnRezervasyonYap.Name = "btnRezervasyonYap";
            this.btnRezervasyonYap.Size = new System.Drawing.Size(165, 44);
            this.btnRezervasyonYap.TabIndex = 0;
            this.btnRezervasyonYap.Text = "REZERVASYON YAP";
            this.btnRezervasyonYap.UseVisualStyleBackColor = false;
            this.btnRezervasyonYap.Click += new System.EventHandler(this.btnRezervasyonYap_Click);
            // 
            // btnGecmisRez
            // 
            this.btnGecmisRez.BackColor = System.Drawing.Color.Transparent;
            this.btnGecmisRez.FlatAppearance.BorderSize = 2;
            this.btnGecmisRez.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGecmisRez.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGecmisRez.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(145)))), ((int)(((byte)(62)))));
            this.btnGecmisRez.Location = new System.Drawing.Point(11, 257);
            this.btnGecmisRez.Name = "btnGecmisRez";
            this.btnGecmisRez.Size = new System.Drawing.Size(165, 44);
            this.btnGecmisRez.TabIndex = 1;
            this.btnGecmisRez.Text = "TÜM REZERVASYONLARIM";
            this.btnGecmisRez.UseVisualStyleBackColor = false;
            this.btnGecmisRez.Click += new System.EventHandler(this.btnGecmisRez_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.BackColor = System.Drawing.Color.Transparent;
            this.btnCikis.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCikis.FlatAppearance.BorderSize = 2;
            this.btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCikis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(145)))), ((int)(((byte)(62)))));
            this.btnCikis.Location = new System.Drawing.Point(430, 257);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(165, 44);
            this.btnCikis.TabIndex = 2;
            this.btnCikis.Text = "ÇIKIŞ YAP";
            this.btnCikis.UseVisualStyleBackColor = false;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // lblHosgeldinX
            // 
            this.lblHosgeldinX.AutoSize = true;
            this.lblHosgeldinX.BackColor = System.Drawing.Color.Transparent;
            this.lblHosgeldinX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(145)))), ((int)(((byte)(62)))));
            this.lblHosgeldinX.Location = new System.Drawing.Point(469, 9);
            this.lblHosgeldinX.Name = "lblHosgeldinX";
            this.lblHosgeldinX.Size = new System.Drawing.Size(82, 13);
            this.lblHosgeldinX.TabIndex = 3;
            this.lblHosgeldinX.Text = "Hoşgeldiniz , ....";
            // 
            // MusteriEkran
            // 
            this.AcceptButton = this.btnRezervasyonYap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Otel_Rezervasyon_Sistemi.Properties.Resources.WhatsApp_Image_2019_10_31_at_16_38_43;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCikis;
            this.ClientSize = new System.Drawing.Size(608, 354);
            this.Controls.Add(this.lblHosgeldinX);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnGecmisRez);
            this.Controls.Add(this.btnRezervasyonYap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MusteriEkran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MusteriEkran";
            this.Load += new System.EventHandler(this.MusteriEkran_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MusteriEkran_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MusteriEkran_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MusteriEkran_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRezervasyonYap;
        private System.Windows.Forms.Button btnGecmisRez;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.Label lblHosgeldinX;
    }
}