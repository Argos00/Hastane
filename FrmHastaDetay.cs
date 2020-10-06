using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglanti bgl = new sqlbaglanti();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //AD SOYAD ÇEKME.
            lblTC.Text = tc;
            SqlCommand komut = new SqlCommand("select hastaAd,HastaSoyad from tbl_hastalar where hastatc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text); // hastadetay formunda giriş kısmından taşınan tc'nin sütununda bulunan hasta adını ve soyadını getir.
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1]; // Önemli!!!

            }
            bgl.baglanti().Close();
            //RANDEVU GEÇMİŞİNİ FORMA ÇEKME.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_randevular where hastatc=" + tc, bgl.baglanti());
            da.Fill(dt); // dataadapterden gelen değer ile datatable'ı doldur.
            dataGridView1.DataSource = dt;

            // BRANŞLARI ÇEKME.
            SqlCommand komut2 = new SqlCommand("select bransad from tbl_branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktorlar where doktorbrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Randevular where randevubrans='" + cmbBrans.Text+"'"+"and randevudoktor='"+cmbDoktor.Text+"'and randevudurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnlBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_Randevular set randevudurum=1,hastatc=@p1,hastasikayet=@p2 where randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", richSikayet.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
