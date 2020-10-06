﻿using System;
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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("select * from tbl_sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", MskTC.Text);
            komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.TCnumara = MskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre");
            }
            
            bgl.baglanti().Close();
        }

        
    }
}
