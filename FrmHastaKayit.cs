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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti(); 
        /*bütün formlarda kullanmak için bir sqlbaglanti sınıfı oluşturduk 
        ve bu sınıfı bütün formlara ekliyoruz. tek tek her forma oluşturmak yerine 
        classtan çağırmak sistemi daha az yorar.
        */
        private void BtnKayıtOl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_hastalar(HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);                 // formdan aldığımız verileri sqlveritabanına ekleme yöntemi. 
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);                  // ÖNEMLİ!!!
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            komut.ExecuteNonQuery(); // insert , update , delete komutlarında kullanılan kod.
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz: " + TxtSifre.Text,"Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
    }
}
