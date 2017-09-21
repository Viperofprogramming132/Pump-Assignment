/* 
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
using System.Threading;
using System.IO;


namespace PumpAssignment
{
    [Serializable]
    public partial class frm_PumpUI : Form
    {
        private Controller m_systemController;
        private frm_Infomation m_frmInfo;
        private delegate void InvokeDelegate();
        private double m_Commission;

        /// <summary>
        /// Constructor for the main windows form UI and sets all the tool tips
        /// </summary>
        public frm_PumpUI(Controller p_systemController)
        {
            InitializeComponent();
            KeyPreview = true;
            m_systemController = p_systemController;
            //Sets all the tooltips for the buttons
            #region tooltips
            ToolTip toolTips = new ToolTip();

            toolTips.AutoPopDelay = 5000;
            toolTips.InitialDelay = 1000;
            toolTips.ReshowDelay = 500;
            toolTips.SetToolTip(btn_Pump1Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump2Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump3Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump4Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump5Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump6Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump7Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump8Assign, "Click this to assign a vehicle");
            toolTips.SetToolTip(btn_Pump9Assign, "Click this to assign a vehicle");

            toolTips.SetToolTip(btn_Save, "Save the current load to XML");
            toolTips.SetToolTip(btn_Transactions, "Show transaction list");

            toolTips.SetToolTip(btn_Pump1Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump2Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump3Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump4Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump5Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump6Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump7Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump8Info, "Click this to get more infomation on the current vehicle fueling");
            toolTips.SetToolTip(btn_Pump9Info, "Click this to get more infomation on the current vehicle fueling");
            #endregion

        }

        /// <summary>
        /// Makes sure that the it can be run from a different thread
        /// </summary>
        public void updateUI()
        {
            if (this.Enabled)
            {
                BeginInvoke(new InvokeDelegate(HandleSelection));
            }
        }

        /// <summary>
        /// Updates all the infomation for the UI
        /// </summary>
        private void HandleSelection()
        {
            #region UpdateElements
            ltb_VehicleList.Items.Clear();
            ltb_VehicleList.Items.AddRange(m_systemController.VehicleList.ToArray());

            pgb_Pump1.Value = (int) m_systemController.PumpList[0].getPercentage();
            pgb_Pump2.Value = (int) m_systemController.PumpList[1].getPercentage();
            pgb_Pump3.Value = (int) m_systemController.PumpList[2].getPercentage();
            pgb_Pump4.Value = (int) m_systemController.PumpList[3].getPercentage();
            pgb_Pump5.Value = (int) m_systemController.PumpList[4].getPercentage();
            pgb_Pump6.Value = (int) m_systemController.PumpList[5].getPercentage();
            pgb_Pump7.Value = (int) m_systemController.PumpList[6].getPercentage();
            pgb_Pump8.Value = (int) m_systemController.PumpList[7].getPercentage();
            pgb_Pump9.Value = (int) m_systemController.PumpList[8].getPercentage();

            txt_DieselDispensed.Text = Math.Round(m_systemController.PumpList[0].DieselDispensed,2).ToString();
            txt_LPGDispensed.Text = Math.Round(m_systemController.PumpList[0].LPGDispensed,2).ToString();
            txt_PetrolDispensed.Text = Math.Round(m_systemController.PumpList[0].PetrolDispensed,2).ToString();
            txt_DieselMoney.Text = Math.Round(m_systemController.PumpList[0].DieselDispensed * 1.2,2).ToString();
            txt_LPGMoney.Text = Math.Round(m_systemController.PumpList[0].LPGDispensed * 0.6,2).ToString();
            txt_PetrolMoney.Text = Math.Round(m_systemController.PumpList[0].PetrolDispensed * 1.17,2).ToString();

            m_Commission = (double.Parse(txt_DieselMoney.Text) + double.Parse(txt_LPGMoney.Text) + double.Parse(txt_PetrolMoney.Text)) * 0.01;
            m_Commission =  Math.Round(m_Commission, 2);
            txt_commission.Text = m_Commission.ToString();

            txt_Serviced.Text = m_systemController.Transactions.Count.ToString();
            txt_Expired.Text = m_systemController.ExpiredCount.ToString();
            #endregion

            #region ActiveCheck

            //Checks if pump 3 is active and stops assignment to it if it is
            if (m_systemController.PumpList[2].getActive())
            {
                rtb_Pump3.Clear();
                rtb_Pump3.AppendText("             " + (m_systemController.PumpList[2].getPercentage()).ToString() + "%");
                btn_Pump3Info.Text = m_systemController.PumpList[2].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump3.BackColor = Color.Red;
                btn_Pump3Assign.Enabled = false;
            }
            else
            {
                btn_Pump3Info.Text = "Infomation";
                rtb_Pump3.Text = "          Pump 3";
                rtb_Pump3.BackColor = Color.LimeGreen;
                btn_Pump3Assign.Enabled = true;
            }
            //Checks if pump 2 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[1].getActive())
            {
                btn_Pump2Info.Text = m_systemController.PumpList[1].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump2.Clear();
                rtb_Pump2.AppendText("             " + (m_systemController.PumpList[1].getPercentage()).ToString() + "%");
                rtb_Pump2.BackColor = Color.Red;
                btn_Pump2Assign.Enabled = false;
                btn_Pump3Assign.Enabled = false;
            }
            else
            {
                btn_Pump2Info.Text = "Infomation";
                rtb_Pump2.Text = "          Pump 2";
                rtb_Pump2.BackColor = Color.LimeGreen;
                btn_Pump2Assign.Enabled = true;
            }
            //Checks if pump 1 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[0].getActive())
            {
                btn_Pump1Info.Text = m_systemController.PumpList[0].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump1.Clear();
                rtb_Pump1.AppendText("             " + (m_systemController.PumpList[0].getPercentage()).ToString() + "%");
                rtb_Pump1.BackColor = Color.Red;
                btn_Pump1Assign.Enabled = false;
                btn_Pump2Assign.Enabled = false;
                btn_Pump3Assign.Enabled = false;
            }
            else
            {
                btn_Pump1Info.Text = "Infomation";
                rtb_Pump1.Text = "          Pump 1";
                rtb_Pump1.BackColor = Color.LimeGreen;
                btn_Pump1Assign.Enabled = true;
            }
            //Checks if pump 6 is active and stops assignment to it if it is
            if (m_systemController.PumpList[5].getActive())
            {
                btn_Pump6Info.Text = m_systemController.PumpList[5].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump6.Clear();
                rtb_Pump6.AppendText("             " + (m_systemController.PumpList[5].getPercentage()).ToString() + "%");
                rtb_Pump6.BackColor = Color.Red;
                btn_Pump6Assign.Enabled = false;
            }
            else
            {
                btn_Pump6Info.Text = "Infomation";
                rtb_Pump6.Text = "          Pump 6";
                rtb_Pump6.BackColor = Color.LimeGreen;
                btn_Pump6Assign.Enabled = true;
            }
            //Checks if pump 5 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[4].getActive())
            {
                btn_Pump5Info.Text = m_systemController.PumpList[4].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump5.Clear();
                rtb_Pump5.AppendText("             " + (m_systemController.PumpList[4].getPercentage()).ToString() + "%");
                rtb_Pump5.BackColor = Color.Red;
                btn_Pump5Assign.Enabled = false;
                btn_Pump6Assign.Enabled = false;
            }
            else
            {
                btn_Pump5Info.Text = "Infomation";
                rtb_Pump5.Text = "          Pump 5";
                rtb_Pump5.BackColor = Color.LimeGreen;
                btn_Pump5Assign.Enabled = true;
            }
            //Checks if pump 4 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[3].getActive())
            {
                btn_Pump4Info.Text = m_systemController.PumpList[3].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump4.Clear();
                rtb_Pump4.AppendText("             " + (m_systemController.PumpList[3].getPercentage()).ToString() + "%");
                rtb_Pump4.BackColor = Color.Red;
                btn_Pump4Assign.Enabled = false;
                btn_Pump5Assign.Enabled = false;
                btn_Pump6Assign.Enabled = false;
            }
            else
            {
                btn_Pump4Info.Text = "Infomation";
                rtb_Pump4.Text = "          Pump 4";
                rtb_Pump4.BackColor = Color.LimeGreen;
                btn_Pump4Assign.Enabled = true;
            }
            //Checks if pump 9 is active and stops assignment to it if it is
            if (m_systemController.PumpList[8].getActive())
            {
                btn_Pump9Info.Text = m_systemController.PumpList[8].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump9.Clear();
                rtb_Pump9.AppendText("             " + (m_systemController.PumpList[8].getPercentage()).ToString() + "%");
                rtb_Pump9.BackColor = Color.Red;
                btn_Pump9Assign.Enabled = false;
            }
            else
            {
                btn_Pump9Info.Text = "Infomation";
                rtb_Pump9.Text = "          Pump 9";
                rtb_Pump9.BackColor = Color.LimeGreen;
                btn_Pump9Assign.Enabled = true;
            }
            //Checks if pump 8 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[7].getActive())
            {
                btn_Pump8Info.Text = m_systemController.PumpList[7].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump8.Clear();
                rtb_Pump8.AppendText("             " + (m_systemController.PumpList[7].getPercentage()).ToString() + "%");
                rtb_Pump8.BackColor = Color.Red;
                btn_Pump8Assign.Enabled = false;
                btn_Pump9Assign.Enabled = false;
            }
            else
            {
                btn_Pump8Info.Text = "Infomation";
                rtb_Pump8.Text = "          Pump 8";
                rtb_Pump8.BackColor = Color.LimeGreen;
                btn_Pump8Assign.Enabled = true;
            }
            //Checks if pump 7 is active and stops assignment to it and all following pumps (in the line) if it is
            if (m_systemController.PumpList[6].getActive())
            {
                btn_Pump7Info.Text = m_systemController.PumpList[6].CurrentVehicle.GetType().ToString().Replace("PumpAssignment.", "") + " Info";
                rtb_Pump7.Clear();
                rtb_Pump7.AppendText("             " + (m_systemController.PumpList[6].getPercentage()).ToString() + "%");
                rtb_Pump7.BackColor = Color.Red;
                btn_Pump7Assign.Enabled = false;
                btn_Pump8Assign.Enabled = false;
                btn_Pump9Assign.Enabled = false;
            }
            else
            {
                btn_Pump7Info.Text = "Infomation";
                rtb_Pump7.Text = "          Pump 7";
                rtb_Pump7.BackColor = Color.LimeGreen;
                btn_Pump7Assign.Enabled = true;
            }
            #endregion
        }

        #region ButtonPressEvents
        /// <summary>
        /// Assigns a vehicle to pump 1 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump1Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[0].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 2 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump2Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[1].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 3 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump3Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[2].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 4 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump4Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[3].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 5 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump5Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[4].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 6 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump6Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[5].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 7 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump7Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[6].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 8 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump8Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[7].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Assigns a vehicle to pump 9 if availible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump9Assign_Click(object sender, EventArgs e)
        {
            if (m_systemController.VehicleList.Count > 0)
            {
                m_systemController.PumpList[8].assignVehicle(m_systemController.VehicleList[0]);
                m_systemController.VehicleList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Opens the infomation form for pump 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump1Info_Click(object sender, EventArgs e)
        {
            openInfo(0);
        }

        /// <summary>
        /// Opens the infomation form for pump 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump2Info_Click(object sender, EventArgs e)
        {
            openInfo(1);
        }

        /// <summary>
        /// Opens the infomation form for pump 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump3Info_Click(object sender, EventArgs e)
        {
            openInfo(2);
        }

        /// <summary>
        /// Opens the infomation form for pump 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump4Info_Click(object sender, EventArgs e)
        {
            openInfo(3);
        }

        /// <summary>
        /// Opens the infomation form for pump 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump5Info_Click(object sender, EventArgs e)
        {
            openInfo(4);
        }

        /// <summary>
        /// Opens the infomation form for pump 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump6Info_Click(object sender, EventArgs e)
        {
            openInfo(5);
        }

        /// <summary>
        /// Opens the infomation form for pump 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump7Info_Click(object sender, EventArgs e)
        {
            openInfo(6);
        }

        /// <summary>
        /// Opens the infomation form for pump 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump8Info_Click(object sender, EventArgs e)
        {
            openInfo(7);
        }

        /// <summary>
        /// Opens the infomation form for pump 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Pump9Info_Click(object sender, EventArgs e)
        {
            openInfo(8);
        }
        #endregion


        /// <summary>
        /// Opens the infomation for the specified pump
        /// </summary>
        /// <param name="i">The index of the pump in the pump list</param>
        private void openInfo(int i)
        {
            m_frmInfo = new frm_Infomation(m_systemController, i);
            this.Hide();
            m_frmInfo.ShowDialog();
            this.Show();
            m_frmInfo = null;
        }

        /// <summary>
        /// Opens the transactions form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Transactions_Click(object sender, EventArgs e)
        {
            frm_Tranactions frmTrans = new frm_Tranactions(m_systemController);
            this.Hide();
            frmTrans.ShowDialog();
            this.Show();
            frmTrans = null;
        }

        /// <summary>
        /// Exports the program to XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            m_systemController.xmlExport();
            MessageBox.Show("Saving Current load to " + Directory.GetCurrentDirectory() + "\\Save.xml");
        }

        /// <summary>
        /// Updates the UI layout to represent the numpad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chb_Layout_CheckedChanged(object sender, EventArgs e)
        {
            if(chb_Layout.Checked)
            {
                rtb_Pump1.Location = new Point(29, 249);
                btn_Pump1Info.Location = new Point(46, 276);
                btn_Pump1Assign.Location = new Point(46, 305);
                pgb_Pump1.Location = new Point(38, 347);
                rtb_Pump7.Location = new Point(29, 12);
                btn_Pump7Info.Location = new Point(46, 39);
                btn_Pump7Assign.Location = new Point(46, 68);
                pgb_Pump7.Location = new Point(38, 110);

                rtb_Pump2.Location = new Point(135, 249);
                btn_Pump2Info.Location = new Point(152, 276);
                btn_Pump2Assign.Location = new Point(152, 305);
                pgb_Pump2.Location = new Point(145, 347);
                rtb_Pump8.Location = new Point(135, 12);
                btn_Pump8Info.Location = new Point(152, 39);
                btn_Pump8Assign.Location = new Point(152, 68);
                pgb_Pump8.Location = new Point(145, 110);

                rtb_Pump3.Location = new Point(241, 249);
                btn_Pump3Info.Location = new Point(258, 276);
                btn_Pump3Assign.Location = new Point(258, 305);
                pgb_Pump3.Location = new Point(250, 347);
                rtb_Pump9.Location = new Point(241, 12);
                btn_Pump9Info.Location = new Point(258, 39);
                btn_Pump9Assign.Location = new Point(258, 68);
                pgb_Pump9.Location = new Point(250, 110);
            }
            else
            {
                rtb_Pump1.Location = new Point(29, 12);
                btn_Pump1Info.Location = new Point(46, 39);
                btn_Pump1Assign.Location = new Point(46, 68);
                pgb_Pump1.Location = new Point(38, 110);
                rtb_Pump7.Location = new Point(29, 249);
                btn_Pump7Info.Location = new Point(46, 276);
                btn_Pump7Assign.Location = new Point(46, 305);
                pgb_Pump7.Location = new Point(38, 347);

                rtb_Pump2.Location = new Point(135, 12);
                btn_Pump2Info.Location = new Point(152, 39);
                btn_Pump2Assign.Location = new Point(152, 68);
                pgb_Pump2.Location = new Point(145, 110);
                rtb_Pump8.Location = new Point(135, 249);
                btn_Pump8Info.Location = new Point(152, 276);
                btn_Pump8Assign.Location = new Point(152, 305);
                pgb_Pump8.Location = new Point(145, 347);

                rtb_Pump3.Location = new Point(241, 12);
                btn_Pump3Info.Location = new Point(258, 39);
                btn_Pump3Assign.Location = new Point(258, 68);
                pgb_Pump3.Location = new Point(250, 110);
                rtb_Pump9.Location = new Point(241, 249);
                btn_Pump9Info.Location = new Point(258, 276);
                btn_Pump9Assign.Location = new Point(258, 305);
                pgb_Pump9.Location = new Point(250, 347);
            }
        }

        private void frm_PumpUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                if (m_systemController.VehicleList.Count > 0)
                {
                    m_systemController.PumpList[0].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                if (m_systemController.VehicleList.Count > 0 && !m_systemController.PumpList[0].getActive())
                {
                    m_systemController.PumpList[1].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                if (m_systemController.VehicleList.Count > 0 && !m_systemController.PumpList[0].getActive() && !m_systemController.PumpList[1].getActive())
                {
                    m_systemController.PumpList[2].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                if (m_systemController.VehicleList.Count > 0)
                {
                    m_systemController.PumpList[3].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5 && !m_systemController.PumpList[3].getActive())
            {
                if (m_systemController.VehicleList.Count > 0)
                {
                    m_systemController.PumpList[4].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6 && !m_systemController.PumpList[3].getActive() && !m_systemController.PumpList[4].getActive())
            {
                if (m_systemController.VehicleList.Count > 0)
                {
                    m_systemController.PumpList[5].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                if (m_systemController.VehicleList.Count > 0)
                {
                    m_systemController.PumpList[6].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                if (m_systemController.VehicleList.Count > 0 && !m_systemController.PumpList[6].getActive())
                {
                    m_systemController.PumpList[7].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                if (m_systemController.VehicleList.Count > 0 && !m_systemController.PumpList[6].getActive() && !m_systemController.PumpList[7].getActive())
                {
                    m_systemController.PumpList[8].assignVehicle(m_systemController.VehicleList[0]);
                    m_systemController.VehicleList.RemoveAt(0);
                }
            }

        }
    }
}
