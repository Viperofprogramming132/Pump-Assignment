/* Logs the pump system
 * Author: 1628071
 * Version: 1.0.1
 * Created: 18/03/17
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace PumpAssignment
{
    [Serializable]
    public class Controller
    {

        private Output m_Output;
        private Thread m_WinForm;
        private List<Vehicle> m_VehicleList = new List<Vehicle>();
        private List<Pump> m_PumpList = new List<Pump>();
        private Random m_CreateVehcile = new Random();
        private frm_PumpUI m_UI;
        private int m_VehicleTime, m_TimeElaped = 0;
        private Timer m_Refreash;
        private List<Transaction> m_Transactions = new List<Transaction>();
        private int m_ExpiredCount;
        private char m_C = '0';
        private string m_Error;
        private bool m_Finished = false;
        private BackgroundWorker m_BW = new BackgroundWorker();
        private bool m_ShowTrans = false;

        public List<Pump> PumpList { get { return m_PumpList; } set { m_PumpList = value; } }
        public List<Vehicle> VehicleList { get { return m_VehicleList; } set { m_VehicleList = value; } }
        public List<Transaction> Transactions { get { return m_Transactions; } set { m_Transactions = value; } }
        public int ExpiredCount { get { return m_ExpiredCount; } set { m_ExpiredCount = value; } }
        public bool Finished { get { return m_Finished; } }



        /// <summary>
        /// Controller constructor creates timer, pumps and opens the windows form
        /// </summary>
        public Controller()
        {
            for (int i = 0; i<9;i++)
            {
                m_PumpList.Add(new Pump(i+1, this));
            }
            m_Output = new Output(this);
            m_UI = new frm_PumpUI(this);
            m_VehicleTime = m_CreateVehcile.Next(15, 23);
            m_Refreash = new Timer(timerTick,null,0,100);


            m_BW.DoWork += new DoWorkEventHandler(bg_DoWork);

            
            //Thread for the windows form
            m_WinForm = new Thread(new ThreadStart(WindowsForm));
            m_WinForm.Start();




            while (m_C != 'q' && !m_Finished)
            {
                if(!m_BW.IsBusy)
                {
                    m_BW.RunWorkerAsync();
                }
            }
            m_WinForm.Abort();
        }


        /// <summary>
        /// Timer Tick for the whole program
        /// </summary>
        /// <param name="m_Refreash"></param>
        /// <param name="e"></param>
        void timerTick(object m_Refreash)
        {
            //Creates the vehicles randomly and outputs the info to debug
            #region creatingVehicles
            if (m_TimeElaped >= m_VehicleTime)
            {
                m_VehicleTime = m_CreateVehcile.Next(1, 4);

                switch (m_VehicleTime)
                {
                    case (1):
                        m_VehicleList.Add(new Car());
                        Debug.WriteLine("Car Created");
                        break;
                    case (2):
                        m_VehicleList.Add(new Van());
                        Debug.WriteLine("Van Created");
                        break;
                    case (3):
                        m_VehicleList.Add(new HGV());
                        Debug.WriteLine("HGV Created");
                        break;
                    default:
                        Exception m_UnknowVehicle = new Exception("Did not create Vehicle. Unknow Vehicle Type tried type " + m_VehicleTime);
                        throw m_UnknowVehicle;
                }
                m_VehicleTime = m_CreateVehcile.Next(15, 23);
                Debug.WriteLine(m_VehicleTime);
                m_Error = new string(' ', Console.WindowWidth);
                m_TimeElaped = -1;
            }
            m_TimeElaped++;
            #endregion


            //Assignment of vehicles and dispensing of fuel
            #region pumpSystem
            //manual assignment of vehicles to pumps
            if (m_C != '0' && m_VehicleList != null && m_VehicleList.Count > 0)
            {
                switch (m_C)
                {
                    case '1':
                        if (!m_PumpList[0].getActive())
                        {
                            m_PumpList[0].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '2':
                        if (!m_PumpList[1].getActive() && !m_PumpList[0].getActive())
                        {
                            m_PumpList[1].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '3':
                        if (!m_PumpList[1].getActive() && !m_PumpList[2].getActive() && !m_PumpList[0].getActive())
                        {
                            m_PumpList[2].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '4':
                        if (!m_PumpList[3].getActive())
                        {
                            m_PumpList[3].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '5':
                        if (!m_PumpList[3].getActive() && !m_PumpList[4].getActive())
                        {
                            m_PumpList[4].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '6':
                        if (!m_PumpList[4].getActive() && !m_PumpList[5].getActive() && !m_PumpList[3].getActive())
                        {
                            m_PumpList[5].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '7':
                        if (!m_PumpList[6].getActive())
                        {
                            m_PumpList[6].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '8':
                        if (!m_PumpList[7].getActive() && !m_PumpList[6].getActive())
                        {
                            m_PumpList[7].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case '9':
                        if (!m_PumpList[7].getActive() && !m_PumpList[8].getActive() && !m_PumpList[6].getActive())
                        {
                            m_PumpList[8].assignVehicle(m_VehicleList[0]);
                            m_VehicleList.RemoveAt(0);
                        }
                        else
                            m_Error = "Unable to put vehicle there. Is it blocked?";
                        break;
                    case 'c':
                        Console.Clear();
                        break;
                    case 't':
                        m_Output.transactionList(m_Transactions);
                        if (!m_ShowTrans)
                        {
                            Console.Clear();
                            m_ShowTrans = true;
                        }
                        else
                        {
                            Console.Clear();
                            m_ShowTrans = false;
                        }
                        break;
                    case 's':
                        m_C = '0';
                        m_Output.writeFile(m_Output.Serialize(this), "Save.xml");
                        break;

                }
                m_C = '0';
            }

            //dispenses the fuel on all active pumps
            foreach (Pump element in m_PumpList)
            {
                element.Dispense();
            }

            #endregion

            if (PumpList.Count > 0 && !m_ShowTrans)
            {
                m_Output.redraw(m_Error);
            }
            else
            {
                m_Output.redrawTransactions(m_Transactions);
            }

            //Checks if the vehicle has expired and deletes it if it has
            #region vehicleExpire
            for(int i = 0; i<m_VehicleList.Count;i++)
            {
                if (m_VehicleList.Count > 0 && m_VehicleList[i] != null)
                {
                    m_VehicleList[i].currentTime++;
                    if (m_VehicleList[i].currentTime >= m_VehicleList[i].maxTime)
                    {
                        m_VehicleList[i] = null;
                        m_VehicleList.RemoveAt(i);
                        m_ExpiredCount++;
                    }
                }
            }
            #endregion


            #region WindowsForm
            if(m_WinForm.IsAlive)
                m_UI.updateUI();
            #endregion
        }

        /// <summary>
        /// Starts the windows form
        /// </summary>
        private void WindowsForm()
        {
            m_UI.ShowDialog();
            
            m_C = 'q';
            m_Finished = true;
            
            m_WinForm.Abort();
            
        }

        /// <summary>
        /// Adds a transaction
        /// </summary>
        /// <param name="v"></param>
        /// <param name="pumpNum"></param>
        /// <param name="dispensed"></param>
        public void addTransaction(Vehicle v,int pumpNum, double dispensed)
        {
            m_Transactions.Add(new Transaction(v, dispensed, pumpNum));
        }

        /// <summary>
        /// Background worker do work. Waits for console input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                ConsoleKeyInfo keypress = Console.ReadKey(true);
                m_C = keypress.KeyChar;
            }
        }

        /// <summary>
        /// Exports the transaction list to a TXT file
        /// </summary>
        public void exportTransactions()
        {
            m_Output.transactionList(m_Transactions);
        }

        /// <summary>
        /// Exports the program to XML
        /// </summary>
        public void xmlExport()
        {
            m_Output.writeFile(m_Output.Serialize(this), "Save.xml");
        }

    }
}
