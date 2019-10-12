using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
            InitializeComponent();
        }
        biliardService.Details dt = new biliardService.Details();
        biliardService.BilliardScoreboard obj = new biliardService.BilliardScoreboard();

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
            if (!bgWorker.IsBusy)
                bgWorker.RunWorkerAsync();
        }
        // background method for hub connection
        private void bgWorkerHub_DoWork(object sender, DoWorkEventArgs e)
        {
            biliardService.Details dt = obj.getPlayerDetails("hsgdh");
            label1.Text = dt.Name;
        }

        private void bgWorkerHub_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // you can check here the connection state
        }
    }
}
