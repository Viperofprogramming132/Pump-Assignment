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

namespace PumpAssignment
{
    [Serializable]
    public partial class frm_Infomation : Form
    {
        /// <summary>
        /// Constructor for the form
        /// </summary>
        /// <param name="systemController"></param>
        /// <param name="i"></param>
        public frm_Infomation(Controller systemController, int i)
        {
            InitializeComponent();

            //Writes all the transactions to the listview
            if (systemController.PumpList[i].CurrentVehicle != null)
            {
                txt_VehicleType.Text = systemController.PumpList[i].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "");
                txt_CurrentFuel.Text = systemController.PumpList[i].CurrentVehicle.currentFuel.ToString();
                txt_MaxFuel.Text = systemController.PumpList[i].CurrentVehicle.maxFuel.ToString();
                txt_FuelType.Text = systemController.PumpList[i].CurrentVehicle.fuelType.ToString();
            }
        }


        public frm_Infomation()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Closes the form to return to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
