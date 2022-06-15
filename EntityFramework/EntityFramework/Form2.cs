using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        EntityFrameworkEntities db = new EntityFrameworkEntities();
        private void btnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                var degerler = db.Notlar.Where(x => x.Sinav1 <= 50);
                dataGridView1.DataSource = degerler.ToList();
            }
        }
    }
}
