using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarService
{
    public partial class frmPopUp : Form
    {
        public frmPopUp()
        {
            InitializeComponent();
        }

        private void frmPopUp_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(10);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
