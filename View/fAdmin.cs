using PBL_Tan.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL_Tan
{
    public partial class fAdmin: Form
    {
        public fAdmin()
        {
            InitializeComponent();
        }
        private void LoadFormIntoPanel(Form form)
        {
            panel3.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.None;
            form.Size = panel3.ClientSize;
            form.Location = new Point(0, 0);
            panel3.Controls.Add(form);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            LoadFormIntoPanel(new fAdmin_Cus());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            LoadFormIntoPanel(new fAdmin_Book());
        }
    }
}
