using System;
using IdleGameApp.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleGameUnitTests
{
    [TestClass]
    public class BuildingManagerTest
    {
        [TestMethod]
        public void CreateManagerAndVerifyStructure()
        {
            var manager = new BuildingManager(new MoneyManager());

            manager.InstatiateBuildings(8);

            Assert.AreEqual(8, manager.NumberOfBuildings);
            Assert.IsNotNull(manager.GetBuilding(3));
            Assert.IsNull(manager.GetBuilding(12));
        }


        [TestMethod]
        public void CreateSeveralCustomBuildings()
        {
            var manager = new BuildingManager(new MoneyManager());

            manager.AddBuilding(10, 10);
            manager.AddBuilding(100, 20);
            manager.AddBuilding(1000, 50);

            Assert.AreEqual(3, manager.NumberOfBuildings);
            Assert.IsNotNull(manager.GetBuilding(1));
            Assert.IsNull(manager.GetBuilding(12));
        }
    }
}
