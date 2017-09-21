using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PumpAssignment;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Controller c = new Controller();
            Car testCar = new Car();
            c.PumpList[2].assignVehicle(new HGV());
            c.PumpList[1].assignVehicle(new Van());
            c.PumpList[0].assignVehicle(new Car());

            Assert.AreEqual(true, c.PumpList[0].getActive());

            Transaction t = new Transaction(testCar, 22, 3);

            Assert.AreEqual(22, t.litersDispensed);
            Assert.AreEqual(3, t.pumpNum);
            Assert.AreEqual(testCar, t.vehicle);

            double beforeFuel = c.PumpList[0].CurrentVehicle.currentFuel;
            c.PumpList[0].Dispense();
            c.PumpList[1].Dispense();
            c.PumpList[2].Dispense();
            c.PumpList[3].Dispense();

            Assert.AreEqual(beforeFuel + 0.15, c.PumpList[0].CurrentVehicle.currentFuel);

            c.PumpList[0].CurrentVehicle.currentFuel = 40.0000000000001;

            c.PumpList[0].Dispense();

            Assert.AreEqual(null, c.PumpList[0].CurrentVehicle);

            c.xmlExport();
            c.exportTransactions();
        }
    }
}
