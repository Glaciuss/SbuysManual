using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseClassLibrary;
using System.Globalization;

namespace CarService
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Program.ProgramName;
            toolBarProgramID.Text = Program.ProgramID;
            toolBarUserLogin.Text = Program.LoginName;
            toolBarCompany.Text = Program.CompanyName;
            toolBarRelVersion.Text = "V" + Program.Version + " Release " + Program.Release + "";

            try
            {
                Database.Connect();
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("ไม่สามารถเชื่อมต่อฐานข้อมูลได้", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            // Put funtion when form show
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("คุณต้องการออกจากโปรแกรม?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Database.Close();
            }
            catch { }
            Program.prcs.Kill();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X)
            {
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            frmPopUp f = new frmPopUp();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCalendar f = new frmCalendar();
            f.monthCalendar1.SelectionStart = DateTime.Today;
            f.monthCalendar1.SelectionEnd = DateTime.Today;
            f.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                textBox1.Text = f.selectedDate;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                try
                {
                    textBox1.Text = sharedFunction.ConvertToDate(textBox1.Text).ToString("dd/MM/yyyy");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
