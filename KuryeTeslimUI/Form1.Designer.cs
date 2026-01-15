namespace KuryeTeslimUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTakip = new System.Windows.Forms.TabPage();
            this.tabListe = new System.Windows.Forms.TabPage();
            this.tabOlustur = new System.Windows.Forms.TabPage();

            // TAKİP SEKME KONTROLLER
            this.lblTakipNo = new System.Windows.Forms.Label();
            this.txtTakipNo = new System.Windows.Forms.TextBox();
            this.btnKonumuGetir = new System.Windows.Forms.Button();
            this.btnRotaIlerle = new System.Windows.Forms.Button();
            this.btnTakipKopyala = new System.Windows.Forms.Button();
            this.splitTakip = new System.Windows.Forms.SplitContainer();
            this.gridOzetTakip = new System.Windows.Forms.DataGridView();
            this.gridHareketTakip = new System.Windows.Forms.DataGridView();

            // LİSTE SEKME KONTROLLER
            this.pnlListeTop = new System.Windows.Forms.Panel();
            this.lblSehir = new System.Windows.Forms.Label();
            this.lblDurum = new System.Windows.Forms.Label();
            this.lblKurye = new System.Windows.Forms.Label();
            this.cmbSehir = new System.Windows.Forms.ComboBox();
            this.cmbDurum = new System.Windows.Forms.ComboBox();
            this.cmbKurye = new System.Windows.Forms.ComboBox();
            this.lblAra = new System.Windows.Forms.Label();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.btnListele = new System.Windows.Forms.Button();
            this.gridListe = new System.Windows.Forms.DataGridView();

            // OLUŞTUR SEKME KONTROLLER
            this.lblYeniTakip = new System.Windows.Forms.Label();
            this.txtYeniTakipNo = new System.Windows.Forms.TextBox();
            this.lblGonderen = new System.Windows.Forms.Label();
            this.txtGonderen = new System.Windows.Forms.TextBox();
            this.lblAlici = new System.Windows.Forms.Label();
            this.txtAlici = new System.Windows.Forms.TextBox();
            this.lblCikisSehir = new System.Windows.Forms.Label();
            this.txtCikisSehir = new System.Windows.Forms.TextBox();
            this.lblVarisSehir = new System.Windows.Forms.Label();
            this.txtVarisSehir = new System.Windows.Forms.TextBox();
            this.btnPaketOlustur = new System.Windows.Forms.Button();
            this.lblDemoAdet = new System.Windows.Forms.Label();
            this.txtDemoAdet = new System.Windows.Forms.TextBox();
            this.btnDemoUret = new System.Windows.Forms.Button();

            // STATUS
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)(this.splitTakip)).BeginInit();
            this.splitTakip.Panel1.SuspendLayout();
            this.splitTakip.Panel2.SuspendLayout();
            this.splitTakip.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.gridOzetTakip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHareketTakip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListe)).BeginInit();

            this.pnlListeTop.SuspendLayout();
            this.statusStrip.SuspendLayout();

            this.SuspendLayout();

            // tabMain
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Controls.Add(this.tabTakip);
            this.tabMain.Controls.Add(this.tabListe);
            this.tabMain.Controls.Add(this.tabOlustur);

            // tabTakip
            this.tabTakip.Text = "Takip Sorgu";
            this.tabTakip.Padding = new System.Windows.Forms.Padding(10);

            // Takip üst satır
            this.lblTakipNo.AutoSize = true;
            this.lblTakipNo.Left = 12;
            this.lblTakipNo.Top = 15;
            this.lblTakipNo.Text = "Takip No:";

            this.txtTakipNo.Left = 80;
            this.txtTakipNo.Top = 12;
            this.txtTakipNo.Width = 250;
            this.txtTakipNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTakipNo_KeyDown);

            this.btnKonumuGetir.Left = 340;
            this.btnKonumuGetir.Top = 10;
            this.btnKonumuGetir.Width = 110;
            this.btnKonumuGetir.Text = "Konumu Getir";
            this.btnKonumuGetir.Click += new System.EventHandler(this.btnKonumuGetir_Click);

            this.btnRotaIlerle.Left = 455;
            this.btnRotaIlerle.Top = 10;
            this.btnRotaIlerle.Width = 95;
            this.btnRotaIlerle.Text = "Rota İlerle";
            this.btnRotaIlerle.Click += new System.EventHandler(this.btnRotaIlerle_Click);

            this.btnTakipKopyala.Left = 555;
            this.btnTakipKopyala.Top = 10;
            this.btnTakipKopyala.Width = 80;
            this.btnTakipKopyala.Text = "Kopyala";
            this.btnTakipKopyala.Click += new System.EventHandler(this.btnTakipKopyala_Click);

            // splitTakip
            this.splitTakip.Left = 10;
            this.splitTakip.Top = 45;
            this.splitTakip.Width = 1040;
            this.splitTakip.Height = 520;
            this.splitTakip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitTakip.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitTakip.SplitterDistance = 260;

            this.gridOzetTakip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHareketTakip.Dock = System.Windows.Forms.DockStyle.Fill;

            this.splitTakip.Panel1.Controls.Add(this.gridOzetTakip);
            this.splitTakip.Panel2.Controls.Add(this.gridHareketTakip);

            this.tabTakip.Controls.Add(this.lblTakipNo);
            this.tabTakip.Controls.Add(this.txtTakipNo);
            this.tabTakip.Controls.Add(this.btnKonumuGetir);
            this.tabTakip.Controls.Add(this.btnRotaIlerle);
            this.tabTakip.Controls.Add(this.btnTakipKopyala);
            this.tabTakip.Controls.Add(this.splitTakip);

            // tabListe
            this.tabListe.Text = "Liste & Filtre";
            this.tabListe.Padding = new System.Windows.Forms.Padding(10);

            // pnlListeTop
            this.pnlListeTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListeTop.Height = 55;

            this.lblSehir.AutoSize = true;
            this.lblSehir.Left = 10;
            this.lblSehir.Top = 18;
            this.lblSehir.Text = "Şehir:";

            this.cmbSehir.Left = 50;
            this.cmbSehir.Top = 14;
            this.cmbSehir.Width = 150;
            this.cmbSehir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblDurum.AutoSize = true;
            this.lblDurum.Left = 210;
            this.lblDurum.Top = 18;
            this.lblDurum.Text = "Durum:";

            this.cmbDurum.Left = 260;
            this.cmbDurum.Top = 14;
            this.cmbDurum.Width = 150;
            this.cmbDurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblKurye.AutoSize = true;
            this.lblKurye.Left = 420;
            this.lblKurye.Top = 18;
            this.lblKurye.Text = "Kurye:";

            this.cmbKurye.Left = 470;
            this.cmbKurye.Top = 14;
            this.cmbKurye.Width = 180;
            this.cmbKurye.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblAra.AutoSize = true;
            this.lblAra.Left = 660;
            this.lblAra.Top = 18;
            this.lblAra.Text = "Ara:";

            this.txtAra.Left = 695;
            this.txtAra.Top = 14;
            this.txtAra.Width = 170;
            this.txtAra.TextChanged += new System.EventHandler(this.txtAra_TextChanged);

            this.btnListele.Left = 875;
            this.btnListele.Top = 12;
            this.btnListele.Width = 140;
            this.btnListele.Text = "Listele / Filtrele";
            this.btnListele.Click += new System.EventHandler(this.btnListele_Click);

            this.pnlListeTop.Controls.Add(this.lblSehir);
            this.pnlListeTop.Controls.Add(this.cmbSehir);
            this.pnlListeTop.Controls.Add(this.lblDurum);
            this.pnlListeTop.Controls.Add(this.cmbDurum);
            this.pnlListeTop.Controls.Add(this.lblKurye);
            this.pnlListeTop.Controls.Add(this.cmbKurye);
            this.pnlListeTop.Controls.Add(this.lblAra);
            this.pnlListeTop.Controls.Add(this.txtAra);
            this.pnlListeTop.Controls.Add(this.btnListele);

            // gridListe
            this.gridListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridListe.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridListe_CellDoubleClick);

            this.tabListe.Controls.Add(this.gridListe);
            this.tabListe.Controls.Add(this.pnlListeTop);

            // tabOlustur
            this.tabOlustur.Text = "Paket Oluştur";
            this.tabOlustur.Padding = new System.Windows.Forms.Padding(10);

            int x1 = 20, y = 20;

            this.lblYeniTakip.AutoSize = true;
            this.lblYeniTakip.Left = x1; this.lblYeniTakip.Top = y;
            this.lblYeniTakip.Text = "Yeni Takip No:";

            this.txtYeniTakipNo.Left = x1 + 110; this.txtYeniTakipNo.Top = y - 3; this.txtYeniTakipNo.Width = 250;

            y += 35;
            this.lblGonderen.AutoSize = true;
            this.lblGonderen.Left = x1; this.lblGonderen.Top = y;
            this.lblGonderen.Text = "Gönderen:";

            this.txtGonderen.Left = x1 + 110; this.txtGonderen.Top = y - 3; this.txtGonderen.Width = 250;

            y += 35;
            this.lblAlici.AutoSize = true;
            this.lblAlici.Left = x1; this.lblAlici.Top = y;
            this.lblAlici.Text = "Alıcı:";

            this.txtAlici.Left = x1 + 110; this.txtAlici.Top = y - 3; this.txtAlici.Width = 250;

            y += 35;
            this.lblCikisSehir.AutoSize = true;
            this.lblCikisSehir.Left = x1; this.lblCikisSehir.Top = y;
            this.lblCikisSehir.Text = "Çıkış Şehir:";

            this.txtCikisSehir.Left = x1 + 110; this.txtCikisSehir.Top = y - 3; this.txtCikisSehir.Width = 250;

            y += 35;
            this.lblVarisSehir.AutoSize = true;
            this.lblVarisSehir.Left = x1; this.lblVarisSehir.Top = y;
            this.lblVarisSehir.Text = "Varış Şehir:";

            this.txtVarisSehir.Left = x1 + 110; this.txtVarisSehir.Top = y - 3; this.txtVarisSehir.Width = 250;

            y += 45;
            this.btnPaketOlustur.Left = x1 + 110; this.btnPaketOlustur.Top = y - 5;
            this.btnPaketOlustur.Width = 250;
            this.btnPaketOlustur.Text = "Paket Oluştur";
            this.btnPaketOlustur.Click += new System.EventHandler(this.btnPaketOlustur_Click);

            // Demo mini alanı
            y += 55;
            this.lblDemoAdet.AutoSize = true;
            this.lblDemoAdet.Left = x1; this.lblDemoAdet.Top = y;
            this.lblDemoAdet.Text = "Demo Adet:";

            this.txtDemoAdet.Left = x1 + 110; this.txtDemoAdet.Top = y - 3; this.txtDemoAdet.Width = 80;

            this.btnDemoUret.Left = x1 + 200; this.btnDemoUret.Top = y - 5;
            this.btnDemoUret.Width = 160;
            this.btnDemoUret.Text = "Demo Üret";
            this.btnDemoUret.Click += new System.EventHandler(this.btnDemoUret_Click);

            this.tabOlustur.Controls.Add(this.lblYeniTakip);
            this.tabOlustur.Controls.Add(this.txtYeniTakipNo);
            this.tabOlustur.Controls.Add(this.lblGonderen);
            this.tabOlustur.Controls.Add(this.txtGonderen);
            this.tabOlustur.Controls.Add(this.lblAlici);
            this.tabOlustur.Controls.Add(this.txtAlici);
            this.tabOlustur.Controls.Add(this.lblCikisSehir);
            this.tabOlustur.Controls.Add(this.txtCikisSehir);
            this.tabOlustur.Controls.Add(this.lblVarisSehir);
            this.tabOlustur.Controls.Add(this.txtVarisSehir);
            this.tabOlustur.Controls.Add(this.btnPaketOlustur);
            this.tabOlustur.Controls.Add(this.lblDemoAdet);
            this.tabOlustur.Controls.Add(this.txtDemoAdet);
            this.tabOlustur.Controls.Add(this.btnDemoUret);

            // statusStrip
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
            this.lblStatus.Text = "Hazır.";

            // Form1
            this.ClientSize = new System.Drawing.Size(1080, 650);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.Name = "Form1";
            this.Text = "KuryeTeslimUI - Geliştirilmiş Frontend";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.splitTakip.Panel1.ResumeLayout(false);
            this.splitTakip.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTakip)).EndInit();
            this.splitTakip.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)(this.gridOzetTakip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHareketTakip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListe)).EndInit();

            this.pnlListeTop.ResumeLayout(false);
            this.pnlListeTop.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTakip;
        private System.Windows.Forms.TabPage tabListe;
        private System.Windows.Forms.TabPage tabOlustur;

        // Takip
        private System.Windows.Forms.Label lblTakipNo;
        private System.Windows.Forms.TextBox txtTakipNo;
        private System.Windows.Forms.Button btnKonumuGetir;
        private System.Windows.Forms.Button btnRotaIlerle;
        private System.Windows.Forms.Button btnTakipKopyala;
        private System.Windows.Forms.SplitContainer splitTakip;
        private System.Windows.Forms.DataGridView gridOzetTakip;
        private System.Windows.Forms.DataGridView gridHareketTakip;

        // Liste
        private System.Windows.Forms.Panel pnlListeTop;
        private System.Windows.Forms.Label lblSehir;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Label lblKurye;
        private System.Windows.Forms.ComboBox cmbSehir;
        private System.Windows.Forms.ComboBox cmbDurum;
        private System.Windows.Forms.ComboBox cmbKurye;
        private System.Windows.Forms.Label lblAra;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Button btnListele;
        private System.Windows.Forms.DataGridView gridListe;

        // Oluştur
        private System.Windows.Forms.Label lblYeniTakip;
        private System.Windows.Forms.TextBox txtYeniTakipNo;
        private System.Windows.Forms.Label lblGonderen;
        private System.Windows.Forms.TextBox txtGonderen;
        private System.Windows.Forms.Label lblAlici;
        private System.Windows.Forms.TextBox txtAlici;
        private System.Windows.Forms.Label lblCikisSehir;
        private System.Windows.Forms.TextBox txtCikisSehir;
        private System.Windows.Forms.Label lblVarisSehir;
        private System.Windows.Forms.TextBox txtVarisSehir;
        private System.Windows.Forms.Button btnPaketOlustur;

        private System.Windows.Forms.Label lblDemoAdet;
        private System.Windows.Forms.TextBox txtDemoAdet;
        private System.Windows.Forms.Button btnDemoUret;

        // Status
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}
