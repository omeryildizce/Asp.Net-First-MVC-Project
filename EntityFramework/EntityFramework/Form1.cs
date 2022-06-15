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
namespace EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        EntityFrameworkEntities db = new EntityFrameworkEntities();
        private void btnDersListele_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EntityFramework;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from Ders ", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Ogrenci.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnNotListele_Click(object sender, EventArgs e)
        {
            var query = from item in db.Notlar
                        select new
                        {
                            item.NotID,
                            item.Ogrenci.OgreciAd,
                            item.Ogrenci.OgrenciSoyad,
                            item.Ders.DersAd,
                            item.Sinav1,
                            item.Sinav2,
                            item.Sinav3,
                            item.Ortalama,
                            item.Durum
                        };
            dataGridView1.DataSource = query.ToList();
            //  dataGridView1.DataSource = db.Notlar.ToList();
        }

        private void btnOgrenciKaydet_Click(object sender, EventArgs e)
        {
            Ogrenci ogrenci = new Ogrenci();
            ogrenci.OgreciAd = txtOgrenciAd.Text;
            ogrenci.OgrenciSoyad = txtOgrenciSoyad.Text;
            db.Ogrenci.Add(ogrenci);
            db.SaveChanges();


        }

        private void btnDersKaydet_Click(object sender, EventArgs e)
        {
            Ders ders = new Ders();
            ders.DersAd = txtDersAd.Text;
            db.Ders.Add(ders);
            db.SaveChanges();
            dataGridView1.DataBindings.Clear();
        }

        private void btnOgrenciSil_Click(object sender, EventArgs e)
        {
            Ogrenci id = OgrenciIdFind();
            db.Ogrenci.Remove(id);
            db.SaveChanges();

        }

        private Ogrenci OgrenciIdFind()
        {
            int ogrenciId = Convert.ToInt32(txtOgrenciId.Text);
            var id = db.Ogrenci.Find(ogrenciId);
            return id;
        }

        private void btnOgrenciGuncelle_Click(object sender, EventArgs e)
        {
            Ogrenci id = OgrenciIdFind();
            id.OgreciAd = txtOgrenciAd.Text;
            id.OgrenciSoyad = txtOgrenciSoyad.Text;
            id.OgrenciFotograf = txtFotograf.Text;
            db.SaveChanges();

        }

        private void btnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotListesi();
        }

        private void btnOgrenciBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Ogrenci.Where(x => x.OgreciAd == txtOgrenciAd.Text | x.OgrenciSoyad == txtOgrenciSoyad.Text).ToList();
        }

        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtOgrenciAd.Text;
            var degerler = from item in db.Ogrenci
                           where item.OgreciAd.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            if (rdbtnSirala.Checked)
            {
                List<Ogrenci> liste1 = db.Ogrenci.OrderBy(p => p.OgreciAd).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (rdbtnSiralaZtoA.Checked)
            {
                List<Ogrenci> liste2 = db.Ogrenci.OrderByDescending(p => p.OgreciAd).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (rdbtnUcKayıt.Checked)
            {
                List<Ogrenci> liste3 = db.Ogrenci.OrderBy(p => p.OgreciAd).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (rdbtnSerachId.Checked)
            {
                List<Ogrenci> liste4 = db.Ogrenci.Where(p => p.OgrenciId == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (rdbtnContainsA.Checked)
            {
                List<Ogrenci> liste5 = db.Ogrenci.Where(p => p.OgreciAd.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (rdbtnEndZ.Checked)
            {
                List<Ogrenci> liste6 = db.Ogrenci.Where(p => p.OgrenciSoyad.EndsWith("z")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (rdbtnDegerler.Checked)
            {
                bool deger = db.Ogrenci.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (rdnbtnOgrenciSayisi.Checked)
            {
                int sum = db.Ogrenci.Count();
                MessageBox.Show(sum.ToString(), "Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            if (rdbtnSinav1toplamPuan.Checked)
            {
                var sinavToplamPuan = db.Notlar.Sum(p => p.Sinav1);
                MessageBox.Show(sinavToplamPuan.ToString(), "Sınav 1 toplam puan", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            if (rdbtnAverageExam1.Checked)
            {
                var averageExam1 = db.Notlar.Average(p => p.Sinav1);
                MessageBox.Show(averageExam1.ToString(), "Sınav 1 ortalaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (rdbtnOverAverage.Checked)
            {
                var overAverage = db.Notlar.Max(p => p.Sinav1);
                MessageBox.Show(overAverage.ToString(), "Sınav 1 en yüksek", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (rdbtnSmallest.Checked)
            {
                var overAverage = db.Notlar.Min(p => p.Sinav1);
                MessageBox.Show(overAverage.ToString(), "Sınav 1 en düşük", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from item in db.Notlar
                        join item2 in db.Ogrenci
                        on item.OgrenciId equals item2.OgrenciId
                        join item3 in db.Ders
                        on item.DersId equals item3.DersId
                        select new
                        {
                            ÖğrenciAdı = item2.OgreciAd,
                            ÖğrenciSoyadı = item2.OgrenciSoyad,
                            DersAdı = item3.DersAd,
                            Sınav1 = item.Sinav1,
                            Sınav2 = item.Sinav2,
                            Sınav3 = item.Sinav3,
                            Ortalama = (item.Sinav3 + item.Sinav2 + item.Sinav1) / 3


                        };
            dataGridView1.DataSource = sorgu.ToList();
        }

        private void btnForm2_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
