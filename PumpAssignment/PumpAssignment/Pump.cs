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
using System.Xml;
using System.Xml.Serialization;

namespace PumpAssignment
{
    [XmlInclude(typeof(Car))]
    [XmlInclude(typeof(Van))]
    [XmlInclude(typeof(HGV))]
    [Serializable]
    public class Pump
    {

        private Vehicle m_CurrentVehicle;
        private double m_Fueled = 0;
        private static double s_rate = 1.5;
        private Controller m_systemController;
        private string m_Active = "XXXXXX";
        private int m_PumpNum;
        private static double m_PetrolDispensed, m_DieselDispensed, m_LPGDispensed;

        
        public string Active { get { return m_Active; } set { m_Active = value; } }
        public double PetrolDispensed { get { return m_PetrolDispensed; } set { m_PetrolDispensed = value; } }
        public double DieselDispensed { get { return m_DieselDispensed; } set { m_DieselDispensed = value; } }
        public double LPGDispensed { get { return m_LPGDispensed; } set { m_LPGDispensed = value; } }
        public int PumpNum { get { return m_PumpNum; } set { m_PumpNum = value; } }
        public Vehicle CurrentVehicle { get { return m_CurrentVehicle; } set { m_CurrentVehicle = value; } }
        public double Fueled { get { return m_Fueled; } set { m_Fueled = value; } }
       

        /// <summary>
        /// Creates a pump
        /// </summary>
        /// <param name="p_PumpNum"></param>
        /// <param name="systemControl"></param>
        public Pump(int p_PumpNum, Controller systemControl)
        {
            m_PumpNum = p_PumpNum;
            m_systemController = systemControl;
        }

        public Pump()
        {

        }

        /// <summary>
        /// Dispenses Fuel at the rate per second and if a vehicle is full removes it and adds the transaction
        /// </summary>
        /// <returns></returns>
        public bool Dispense()
        {
            if (m_CurrentVehicle != null)
            {
                if (m_CurrentVehicle.currentFuel < m_CurrentVehicle.maxFuel)
                {
                    m_CurrentVehicle.currentFuel += s_rate / 10;
                    if(CurrentVehicle.fuelType == "Petrol")
                    {
                        m_PetrolDispensed += s_rate / 10;
                        m_Fueled += s_rate / 10;
                    }
                    if (CurrentVehicle.fuelType == "Diesel")
                    {
                        m_DieselDispensed += s_rate / 10;
                        m_Fueled += s_rate / 10;
                    }
                    if (CurrentVehicle.fuelType == "LPG")
                    {
                        m_LPGDispensed += s_rate / 10;
                        m_Fueled += s_rate / 10;
                    }
                }
                

                //Checks if the vehicle has been fully fueled
                if (m_CurrentVehicle.currentFuel >= m_CurrentVehicle.maxFuel)
                {
                    Debug.WriteLine("Fueled a {0}", m_CurrentVehicle.GetType());
                    m_systemController.addTransaction(m_CurrentVehicle, m_PumpNum, m_Fueled);
                    m_CurrentVehicle = null;
                    m_Fueled = 0;
                    m_Active = "XXXXXX";
                    return true;

                    
                }
            }
            return false;
        }

        /// <summary>
        /// sets a vehicle if one is not already set to the pump
        /// </summary>
        /// <param name="p_assignedVehicle"></param>
        public void assignVehicle(Vehicle p_assignedVehicle)
        {
            if (!this.getActive())
            {
                m_CurrentVehicle = p_assignedVehicle;
                m_Active = "ACTIVE";
            }
        }

        /// <summary>
        /// Returns true if the pump is active otherwise it returns false
        /// </summary>
        /// <returns></returns>
        public bool getActive()
        {
            if (m_Active == "XXXXXX")
            {
                return false;
            }
            return true;
        }

        public double getPercentage()
        {
            if (CurrentVehicle != null)
            {
                return Math.Round((CurrentVehicle.currentFuel / CurrentVehicle.maxFuel) * 100);
            }
            return 0;
        }
    }
}
