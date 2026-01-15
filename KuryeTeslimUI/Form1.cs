using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KuryeTeslimUI
{
    public partial class Form1 : Form
    {
        private readonly string _cs =
            "Server=localhost\\SQLEXPRESS;Database=KuryeTeslimDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupGrid(gridOzetTakip);
            SetupGrid(gridHareketTakip);
            SetupGrid(gridListe);

            txtTakipNo.Text = "";
            txtDemoAdet.Text = "20";

            SetStatus("Hazır.");

            try
            {
                LoadFilters();
                LoadListWithFilters(); // açılışta liste dolsun
            }
            catch (Exception ex)
            {
                SetStatus("Filtre/Liste yüklenemedi: " + ex.Message);
            }
        }

        private void SetupGrid(DataGridView g)
        {
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
        }

        // -------------------------
        // TAKİP SEKME
        // -------------------------
        private void btnKonumuGetir_Click(object sender, EventArgs e)
        {
            string takipNo = txtTakipNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(takipNo))
            {
                MessageBox.Show("Takip No gir.");
                txtTakipNo.Focus();
                return;
            }
            LoadByTakipNo(takipNo);
        }

        private void txtTakipNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnKonumuGetir_Click(sender, e);
            }
        }

        private void btnRotaIlerle_Click(object sender, EventArgs e)
        {
            string takipNo = txtTakipNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(takipNo))
            {
                MessageBox.Show("Takip No gir.");
                txtTakipNo.Focus();
                return;
            }

            try
            {
                ExecSP("dbo.sp_RotaIlerle", new SqlParameter("@TakipNo", takipNo));
                LoadByTakipNo(takipNo);
                SetStatus("Rota ilerletildi: " + takipNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                SetStatus("Hata: " + ex.Message);
            }
        }

        private void btnTakipKopyala_Click(object sender, EventArgs e)
        {
            var t = txtTakipNo.Text.Trim();
            if (!string.IsNullOrWhiteSpace(t))
            {
                Clipboard.SetText(t);
                SetStatus("Kopyalandı: " + t);
            }
        }

        private void LoadByTakipNo(string takipNo)
        {
            try
            {
                var dtOzet = QueryTable(
                    "SELECT * FROM dbo.vw_PaketOzet WHERE TakipNo = @TakipNo",
                    new SqlParameter("@TakipNo", takipNo)
                );
                gridOzetTakip.DataSource = dtOzet;

                var dtHareket = QueryTable(
@"SELECT p.TakipNo, ph.TarihSaat, ph.Aciklama
  FROM dbo.PaketHareket ph
  JOIN dbo.Paket p ON p.PaketID = ph.PaketID
  WHERE p.TakipNo = @TakipNo
  ORDER BY ph.TarihSaat;",
                    new SqlParameter("@TakipNo", takipNo)
                );
                gridHareketTakip.DataSource = dtHareket;

                if (dtOzet.Rows.Count == 0)
                    MessageBox.Show("Bu takip numarası ile paket bulunamadı.");

                SetStatus("Takip sorgulandı: " + takipNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                SetStatus("Hata: " + ex.Message);
            }
        }

        // -------------------------
        // LİSTE SEKME
        // -------------------------
        private void btnListele_Click(object sender, EventArgs e)
        {
            LoadListWithFilters();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            // yazdıkça filtre
            LoadListWithFilters();
        }

        private void gridListe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Çift tık: takip sekmesine götür + sorgula
            if (gridListe.CurrentRow == null) return;

            if (gridListe.Columns.Contains("TakipNo"))
            {
                var v = gridListe.CurrentRow.Cells["TakipNo"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(v))
                {
                    txtTakipNo.Text = v;
                    tabMain.SelectedTab = tabTakip;
                    LoadByTakipNo(v);
                }
            }
        }

        private void LoadListWithFilters()
        {
            try
            {
                string sehir = (cmbSehir.SelectedItem ?? "").ToString();
                string durum = (cmbDurum.SelectedItem ?? "").ToString();
                string kurye = (cmbKurye.SelectedItem ?? "").ToString();
                string ara = txtAra.Text.Trim();

                object pSehir = (sehir == "Tümü" || sehir == "") ? (object)DBNull.Value : sehir;
                object pDurum = (durum == "Tümü" || durum == "") ? (object)DBNull.Value : durum;
                object pKurye = (kurye == "Tüm Kuryeler" || kurye == "Tümü" || kurye == "") ? (object)DBNull.Value : kurye;
                object pAra = string.IsNullOrWhiteSpace(ara) ? (object)DBNull.Value : ara;

                var dt = QueryTable(
@"SELECT TOP 300 *
  FROM dbo.vw_PaketOzet
  WHERE (@Sehir IS NULL OR CikisSehir = @Sehir)
    AND (@Durum IS NULL OR DurumAdi = @Durum)
    AND (@Kurye IS NULL OR KuryeAdSoyad = @Kurye)
    AND (
        @Ara IS NULL OR
        TakipNo LIKE '%' + @Ara + '%' OR
        Gonderen LIKE '%' + @Ara + '%' OR
        Alici LIKE '%' + @Ara + '%'
    )
  ORDER BY OlusturmaTarihi DESC;",
                    new SqlParameter("@Sehir", pSehir),
                    new SqlParameter("@Durum", pDurum),
                    new SqlParameter("@Kurye", pKurye),
                    new SqlParameter("@Ara", pAra)
                );

                gridListe.DataSource = dt;
                SetStatus($"Liste: {dt.Rows.Count} kayıt");
            }
            catch (Exception ex)
            {
                SetStatus("Liste hatası: " + ex.Message);
            }
        }

        private void LoadFilters()
        {
            cmbSehir.Items.Clear();
            cmbDurum.Items.Clear();
            cmbKurye.Items.Clear();

            cmbSehir.Items.Add("Tümü");
            cmbDurum.Items.Add("Tümü");
            cmbKurye.Items.Add("Tüm Kuryeler");

            var dtSehir = QueryTable("SELECT DISTINCT CikisSehir FROM dbo.vw_PaketOzet ORDER BY CikisSehir;");
            foreach (DataRow r in dtSehir.Rows) cmbSehir.Items.Add(r[0].ToString());

            var dtDurum = QueryTable("SELECT DurumAdi FROM dbo.PaketDurum ORDER BY DurumAdi;");
            foreach (DataRow r in dtDurum.Rows) cmbDurum.Items.Add(r[0].ToString());

            var dtKurye = QueryTable(
@"SELECT (k.Ad + ' ' + k.Soyad) AS KuryeAdSoyad
  FROM dbo.Kullanici k
  JOIN dbo.Rol r ON r.RolID = k.RolID
  WHERE r.RolAdi = N'Kurye'
  ORDER BY KuryeAdSoyad;"
            );
            foreach (DataRow r in dtKurye.Rows) cmbKurye.Items.Add(r[0].ToString());

            cmbSehir.SelectedIndex = 0;
            cmbDurum.SelectedIndex = 0;
            cmbKurye.SelectedIndex = 0;
        }

        // -------------------------
        // PAKET OLUŞTUR SEKME
        // -------------------------
        private void btnPaketOlustur_Click(object sender, EventArgs e)
        {
            string takipNo = txtYeniTakipNo.Text.Trim();
            string gonderen = txtGonderen.Text.Trim();
            string alici = txtAlici.Text.Trim();
            string cikisSehir = txtCikisSehir.Text.Trim();
            string varisSehir = txtVarisSehir.Text.Trim();

            if (string.IsNullOrWhiteSpace(takipNo) ||
                string.IsNullOrWhiteSpace(gonderen) ||
                string.IsNullOrWhiteSpace(alici) ||
                string.IsNullOrWhiteSpace(cikisSehir) ||
                string.IsNullOrWhiteSpace(varisSehir))
            {
                MessageBox.Show("Tüm alanları doldur.");
                return;
            }

            try
            {
                ExecSP("dbo.sp_PaketOlusturTR",
                    new SqlParameter("@TakipNo", takipNo),
                    new SqlParameter("@Gonderen", gonderen),
                    new SqlParameter("@Alici", alici),
                    new SqlParameter("@CikisSehir", cikisSehir),
                    new SqlParameter("@VarisSehir", varisSehir)
                );

                MessageBox.Show("Paket oluşturuldu.");
                SetStatus("Paket oluşturuldu: " + takipNo);

                // Takip sekmesine geçir
                txtTakipNo.Text = takipNo;
                tabMain.SelectedTab = tabTakip;
                LoadByTakipNo(takipNo);

                // Listeyi yenile
                LoadListWithFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                SetStatus("Hata: " + ex.Message);
            }
        }

        private void btnDemoUret_Click(object sender, EventArgs e)
        {
            int adet;
            if (!int.TryParse(txtDemoAdet.Text.Trim(), out adet) || adet <= 0) adet = 20;

            try
            {
                ExecSP("dbo.sp_DemoVeriUret", new SqlParameter("@Adet", adet));
                MessageBox.Show("Demo üretildi: " + adet);
                SetStatus("Demo üretildi: " + adet);
                LoadListWithFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                SetStatus("Hata: " + ex.Message);
            }
        }

        // -------------------------
        // DB Helper
        // -------------------------
        private DataTable QueryTable(string sql, params SqlParameter[] prms)
        {
            using (var con = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, con))
            {
                if (prms != null && prms.Length > 0) cmd.Parameters.AddRange(prms);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        private void ExecSP(string spName, params SqlParameter[] prms)
        {
            using (var con = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(spName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (prms != null && prms.Length > 0) cmd.Parameters.AddRange(prms);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void SetStatus(string msg)
        {
            lblStatus.Text = msg;
        }
    }
}
