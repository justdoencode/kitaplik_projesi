using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace kitaplik_projesi
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\Users\poyra\OneDrive\Masaüstü\C# Çalışmalar\Udemy Çalışma\kitaplik.mdb'");

        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select* from tbl_kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btn_listele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum;
        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            
            OleDbCommand omd = new OleDbCommand("insert into tbl_kitaplar (kitap_ad,kitap_yazar,kitap_tur,kitap_sayfa,kitap_durum) values (@ad,@yazar,@tur,@sayfa,@durum)", baglanti);
            omd.Parameters.AddWithValue("@ad", txt_ad.Text);
            omd.Parameters.AddWithValue("@yazar", txt_yazar.Text);
            omd.Parameters.AddWithValue("@tur", combo_tur.Text);
            omd.Parameters.AddWithValue("@sayfa", txt_sayfa.Text);
            
            if (radio_kullanilmis.Checked)
                durum = "0";
            if (radio_sifir.Checked)
                durum = "1";
            omd.Parameters.AddWithValue("@durum", durum);
            omd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("delete from tbl_kitaplar where kitap_id="+txt_id.Text,baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            txt_id.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            txt_ad.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            txt_yazar.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
            combo_tur.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            txt_sayfa.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secili].Cells[5].Value.ToString() == "True")
                radio_sifir.Checked = true;
            if (dataGridView1.Rows[secili].Cells[5].Value.ToString() == "False")
                radio_kullanilmis.Checked = true;
        }
        
        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("update tbl_kitaplar set kitap_ad=@ad,kitap_yazar=@yazar,kitap_sayfa=@sayfa,kitap_tur=@tur,kitap_durum=@durum where kitap_id="+txt_id.Text, baglanti);
            cmd.Parameters.AddWithValue("@ad", txt_ad.Text);
            cmd.Parameters.AddWithValue("@yazar", txt_yazar.Text);
            cmd.Parameters.AddWithValue("@sayfa", txt_sayfa.Text);
            cmd.Parameters.AddWithValue("@tur", combo_tur.Text);
            if (radio_kullanilmis.Checked)
                durum = "0";
            if (radio_sifir.Checked)
                durum = "1";
            cmd.Parameters.AddWithValue("durum", durum);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veriler Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void btn_bul_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select* from tbl_kitaplar where kitap_ad='"+txt_bul.Text+"'", baglanti);
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void txt_arama_TextChanged(object sender, EventArgs e)
        {
            DataTable dt3 = new DataTable();
            OleDbDataAdapter da3 = new OleDbDataAdapter("select* from tbl_kitaplar where kitap_ad like '%"+txt_arama.Text+"%'", baglanti);
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
    }
}
