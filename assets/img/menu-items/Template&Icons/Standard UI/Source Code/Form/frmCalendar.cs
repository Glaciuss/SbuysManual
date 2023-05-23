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
    public partial class frmCalendar : Form
    {
        public string selectedDate;

        public frmCalendar()
        {
            InitializeComponent();
        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            txtSelectedDate.Text = monthCalendar1.TodayDate.ToString("dd/MM/yyyy");
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtSelectedDate.Text = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            selectedDate = txtSelectedDate.Text;
            DialogResult = DialogResult.OK;
        }

        private DateTime last_mouse_down = DateTime.Now;

        private void monthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((DateTime.Now - last_mouse_down).TotalMilliseconds <= SystemInformation.DoubleClickTime)
            {
                // respond to double click
                selectedDate = txtSelectedDate.Text;
                DialogResult = DialogResult.OK;
            }
            last_mouse_down = DateTime.Now;
        }
    }
}
