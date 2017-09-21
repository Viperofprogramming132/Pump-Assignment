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
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace PumpAssignment
{
    [Serializable]
    public class Output
    {
        private Controller m_SystemController;
        private List<string> m_FileWrite = new List<string>();
        /// <summary>
        /// Draws the console view when the application starts
        /// </summary>
        public Output(Controller p_SystemController)
        {
            m_SystemController = p_SystemController;
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- XXXXXX ------ XXXXXX ------ XXXXXX -");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- XXXXXX ------ XXXXXX ------ XXXXXX -");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- XXXXXX ------ XXXXXX ------ XXXXXX -");
            Console.WriteLine("--------------------------------------");
        }

        public Output()
        {

        }

        /// <summary>
        /// Redraws the console UI
        /// </summary>
        /// <param name="p_Error"></param>
        public void redraw(string p_Error)
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 85;
            Console.SetCursorPosition(0,0);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- {0} ------ {1} ------ {2} -", m_SystemController.PumpList[0].Active, m_SystemController.PumpList[1].Active, m_SystemController.PumpList[2].Active);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- {0} ------ {1} ------ {2} -", m_SystemController.PumpList[3].Active, m_SystemController.PumpList[4].Active, m_SystemController.PumpList[5].Active);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("- {0} ------ {1} ------ {2} -", m_SystemController.PumpList[6].Active, m_SystemController.PumpList[7].Active, m_SystemController.PumpList[8].Active);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Waiting Cars: {0}", m_SystemController.VehicleList.Count);
            Console.WriteLine("\nCommission: {6:N2} \nPetrol Dispensed: {0:N2}         Petrol Money {3:N2}\n\nDiesel Dispensed: {1:N2}         Diesel Money {4:N2}\n\nLPG Dispensed: {2:N2}         LPG Money {5:N2}", m_SystemController.PumpList[0].PetrolDispensed, m_SystemController.PumpList[0].DieselDispensed, m_SystemController.PumpList[0].LPGDispensed, m_SystemController.PumpList[0].PetrolDispensed * 1.17, m_SystemController.PumpList[0].DieselDispensed * 1.2, m_SystemController.PumpList[0].LPGDispensed * 0.6, (m_SystemController.PumpList[0].PetrolDispensed * 1.17 + m_SystemController.PumpList[0].DieselDispensed * 1.2 + m_SystemController.PumpList[0].LPGDispensed * 0.6) * 0.01);
            Console.WriteLine("Expired Vehicles: {0}    Service Vehicles: {1}", m_SystemController.ExpiredCount, m_SystemController.Transactions.Count);
            Console.WriteLine(p_Error);

            Console.SetCursorPosition(3, 0);
            Console.WriteLine(" " + m_SystemController.PumpList[0].getPercentage() + " ");
            Console.SetCursorPosition(17, 0);
            Console.WriteLine(" " + m_SystemController.PumpList[1].getPercentage() + " ");
            Console.SetCursorPosition(31, 0);
            Console.WriteLine(" " + m_SystemController.PumpList[2].getPercentage() + " ");

            Console.SetCursorPosition(3, 2);
            Console.WriteLine(" " + m_SystemController.PumpList[3].getPercentage() + " ");
            Console.SetCursorPosition(17, 2);
            Console.WriteLine(" " + m_SystemController.PumpList[4].getPercentage() + " ");
            Console.SetCursorPosition(31, 2);
            Console.WriteLine(" " + m_SystemController.PumpList[5].getPercentage() + " ");

            Console.SetCursorPosition(3, 4);
            Console.WriteLine(" " + m_SystemController.PumpList[6].getPercentage() + " ");
            Console.SetCursorPosition(17, 4);
            Console.WriteLine(" " + m_SystemController.PumpList[7].getPercentage() + " ");
            Console.SetCursorPosition(31, 4);
            Console.WriteLine(" " + m_SystemController.PumpList[8].getPercentage() + " ");
        }

        /// <summary>
        /// Draws the transactions
        /// </summary>
        /// <param name="p_Trans"></param>
        public void redrawTransactions(List<Transaction> p_Trans)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Transaction List");
            Console.WriteLine("Vehicle | Lieters Dispensed | Pump Number");
            foreach(Transaction t in p_Trans)
            {
                Console.WriteLine(t.toDelimitedString());
            }
        }

        /// <summary>
        /// Writes the transaction File from the given list
        /// </summary>
        /// <param name="t"></param>
        public void transactionList(List<Transaction> t)
        {
            foreach(Transaction trans in t)
            {
                m_FileWrite.Add(trans.toDelimitedString());
            }
            writeFile(m_FileWrite, "TransactionList.txt");
            m_FileWrite.Clear();
        }

        /// <summary>
        /// Serializes the given controller
        /// </summary>
        /// <param name="p_value"></param>
        /// <returns></returns>
        public string Serialize(Controller p_value)
        {
            if (p_value == null)
            {
                return string.Empty;
            }
            try
            {
                XmlSerializer m_xmlserializer = new XmlSerializer(typeof(Controller));
                StringWriter m_stringWriter = new StringWriter();
                using (XmlWriter writer = XmlWriter.Create(m_stringWriter))
                {
                    m_xmlserializer.Serialize(writer, p_value);
                    return m_stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        /// <summary>
        /// Deserializes the xml save
        /// </summary>
        /// <returns></returns>
        public Controller Deserialize()
        {
            XmlSerializer m_xmlserializer = new XmlSerializer(typeof(Controller));
            try
            {
                using (XmlReader reader = XmlReader.Create(Directory.GetCurrentDirectory().ToString() + "/Save.xml"))
                {                    
                    return (Controller)m_xmlserializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred", e);
            }
        }

        /// <summary>
        /// Writes the file with the given string list and the file name
        /// </summary>
        /// <param name="p_s"></param>
        /// <param name="p_FileName"></param>
        public void writeFile(List<String> p_s, string p_FileName)
        {
            File.WriteAllLines(Directory.GetCurrentDirectory().ToString() + "/" + p_FileName, p_s);
        }

        /// <summary>
        /// Writes the file with the given string and the file name
        /// </summary>
        /// <param name="p_s"></param>
        /// <param name="p_FileName"></param>
        public void writeFile(string p_s, string p_FileName)
        {
            m_FileWrite.Add(p_s);
            File.WriteAllLines(Directory.GetCurrentDirectory().ToString() + "/" + p_FileName, m_FileWrite);
            m_FileWrite.Clear();
        }

    }
}
