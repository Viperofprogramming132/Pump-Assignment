/* Logs the pump system
 * Author: 1628071
 * Version: 1.0.1
 * Created: 18/03/17
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PumpAssignment
{
    [Serializable]
    public partial class frm_Tranactions : Form
    {
        private Controller m_systemControl;
        private ListViewItem m_lvi;
        public frm_Tranactions(Controller systemController)
        {
            m_systemControl = systemController;
            InitializeComponent();
            ltv_Transactions.View = View.Details;
            ltv_Transactions.Columns.Add("Vehicle",-2);
            ltv_Transactions.Columns.Add("Liters Dispensed",-2);
            ltv_Transactions.Columns.Add("Pump Num",-2);
            foreach (Transaction i in systemController.Transactions)
            {
                m_lvi = new ListViewItem(i.vehicle.ToString().Replace("PumpAssignment.", ""),0);
                m_lvi.SubItems.Add(Math.Round(i.litersDispensed).ToString());
                m_lvi.SubItems.Add(i.pumpNum.ToString());
                ltv_Transactions.Items.Add(m_lvi);
            }
            m_lvi = null;
        }
        public frm_Tranactions()
        {
            InitializeComponent();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            m_systemControl.exportTransactions();
            MessageBox.Show("Saving Current transactions to " + Directory.GetCurrentDirectory() + "\\TransactionList.txt");
        }
    }
}
