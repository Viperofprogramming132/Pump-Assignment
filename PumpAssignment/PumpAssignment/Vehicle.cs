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


namespace PumpAssignment
{
    public class Vehicle
    {
        protected Random m_ran = new Random();
        protected string m_FuelType;
        protected double m_maxFuel;
        protected double m_CurrentFuel;
        protected int m_maxTime;
        protected int m_currentTime;

        public double currentFuel { get { return m_CurrentFuel; } set { m_CurrentFuel = value; } }
        public double maxFuel { get { return m_maxFuel; } set { } }
        public string fuelType { get { return m_FuelType; } set { m_FuelType = value; } }
        public int currentTime { get { return m_currentTime; } set { m_currentTime = value; } }
        public int maxTime { get { return m_maxTime; } set { m_maxTime = value; } }


        public Vehicle()
        {
            m_maxTime = m_ran.Next(10, 21);
        }

    }
    [Serializable]
    public class Car : Vehicle
    {
        /// <summary>
        /// Selects the fuel type, current fuel and when it will expire for the vehicle
        /// </summary>
        public Car() : base()
        {
            m_maxFuel = 40;
            int i = m_ran.Next(1, 4);

            switch (i)
            {
                case (1):
                    m_FuelType = "Petrol";
                    break;
                case (2):
                    m_FuelType = "Diesel";
                    break;
                case (3):
                    m_FuelType = "LPG";
                    break;
                default:
                    Exception blah = new Exception("Unknown Fuel Type");
                    break;
            }
            m_CurrentFuel = m_ran.Next(0, (int) m_maxFuel / 4);
        }
    }
    [Serializable]
    public class Van : Vehicle
    {
        /// <summary>
        /// Selects the fuel type, current fuel and when it will expire for the vehicle
        /// </summary>
        public Van() : base()
        {
            m_maxFuel = 80;
            int i = m_ran.Next(1, 3);
            switch (i)
            {
                case (1):
                    m_FuelType = "LPG";
                    break;
                case (2):
                    m_FuelType = "Diesel";
                    break;
                default:
                    Exception blah = new Exception("Unknown Fuel Type");
                    break;
            }
            m_CurrentFuel = m_ran.Next(0, (int)m_maxFuel / 4);
        }
    }
    [Serializable]
    public class HGV : Vehicle
    {
        /// <summary>
        /// Selects the fuel type, current fuel and when it will expire for the vehicle
        /// </summary>
        public HGV() : base()
        {
            m_maxFuel = 150;
            m_FuelType = "Diesel";
            m_CurrentFuel = m_ran.Next(0, (int)m_maxFuel / 4);
        }
    }
}
