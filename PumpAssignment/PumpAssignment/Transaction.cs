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
    [Serializable]
    public class Transaction
    {
        private StringBuilder m_result = new StringBuilder();
        private string m_output;
        private Vehicle m_Vehicle;
        private double m_LitersDispensed;
        private int m_PumpNumber;

        public Vehicle vehicle { get { return m_Vehicle; } set { } }
        public double litersDispensed { get { return m_LitersDispensed; } set { } }
        public int pumpNum { get { return m_PumpNumber; } set { } }

        /// <summary>
        /// Contructs a transaction
        /// </summary>
        /// <param name="p_Vehicle"></param>
        /// <param name="p_LitresDispensed"></param>
        /// <param name="p_PumpNumber"></param>
        public Transaction(Vehicle p_Vehicle,double p_LitresDispensed,int p_PumpNumber)
        {
            m_Vehicle = p_Vehicle;
            m_PumpNumber = p_PumpNumber;
            m_LitersDispensed = p_LitresDispensed;
        }

        public Transaction()
        {

        }

        /// <summary>
        /// Makes the given transaction to a string
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string toDelimitedString()
        {
            m_result.Append(string.Format("{0}|{1}|{2}", m_Vehicle.GetType().ToString().Replace("PumpAssignment.", ""), Math.Round(m_LitersDispensed), m_PumpNumber));
            m_output = m_result.ToString();
            m_result.Clear();
            return m_output;
        }
    }
}
