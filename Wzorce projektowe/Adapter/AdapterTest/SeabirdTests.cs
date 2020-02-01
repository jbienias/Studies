using Adapter.SeabirdExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdapterTest
{
    [TestClass]
    public class SeabirdTests
    {
        IAircraft seabirdSea;
        ISeacraft seabirdAir;

        [TestInitialize]
        public void Initialize()
        {
            seabirdSea = new Seabird(); //SeabirdSeacraft
            seabirdAir = new SeabirdAircraft();
        }

        [TestMethod]
        public void SeabirdSeacraft_TestSpeed()
        {

            seabirdSea.TakeOff();
            Assert.IsTrue((seabirdSea as ISeacraft).Speed > 0);
        }

        [TestMethod]
        public void SeabirdSeacraft_TestAirborne()
        {
            for (int i = 0; i < 100; i++)
                (seabirdSea as ISeacraft).IncreaseRevs();
            Assert.IsTrue(seabirdSea.Airborne);
        }

        [TestMethod]
        public void SeabirdAircraft_TestSpeed()
        {
            seabirdAir.IncreaseRevs();
            Assert.IsTrue((seabirdAir as ISeacraft).Speed > 0);
        }

        [TestMethod]
        public void SeabirdAircraft_TestAirborne()
        {
            (seabirdAir as IAircraft).TakeOff();
            Assert.IsTrue((seabirdAir as IAircraft).Airborne);
        }

        [TestCleanup]
        public void Cleanup()
        {
            seabirdAir = null;
            seabirdSea = null;
        }
    }
}
