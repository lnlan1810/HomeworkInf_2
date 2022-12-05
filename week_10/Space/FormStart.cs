using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace Space
{
    public partial class FormStart : Form
    {

        public FormStart()
        {
            InitializeComponent();
        }

        private void bt_play(object sender, EventArgs e)
        {
           
        }

        private void playClick(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();

        }

        private void FormStart_Load(object sender, EventArgs e)
        {

        }
    }
}
