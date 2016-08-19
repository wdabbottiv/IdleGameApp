using System;
using System.Numerics;
using IdleGameApp.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleGameUnitTests
{
    [TestClass]
    public class BuildingTest
    {
        [TestMethod]
        public void TestRevenueOfOneBuilding()
        {
            var manager = new MoneyManager();
            var building = new Building(10, 10, manager);
            var initialMoney = new BigInteger(0);
            var moneyAfterOneTick = new BigInteger(10);
            building.BuildingsOwned = 1;

            Assert.AreEqual(initialMoney, manager.TotalMoney);

            building.MoneyEarningTimerElapsed(new object(), new EventArgs());

            Assert.AreEqual(moneyAfterOneTick, manager.TotalMoney);
            Assert.AreEqual(100, building.EarningsPerSecond);
        }

        [TestMethod]
        public void TestRevenueOfTwoBuildings()
        {
            var manager = new MoneyManager();
            var building = new Building(10, 10, manager);
            var initialMoney = new BigInteger(0);
            var moneyAfterOneTick = new BigInteger(20);
            building.BuildingsOwned = 2;

            Assert.AreEqual(initialMoney, manager.TotalMoney);

            building.MoneyEarningTimerElapsed(new object(), new EventArgs());

            Assert.AreEqual(moneyAfterOneTick, manager.TotalMoney);
            Assert.AreEqual(200, building.EarningsPerSecond);
        }

        [TestMethod]
        public void TestCreatingOneBuildingWhenItsAffordable()
        {
            var manager = new MoneyManager();
            manager.TotalMoney += 10;
            var building = new Building(10, 10, manager);

            building.HandleBuyBuilding();

            Assert.AreEqual(manager.TotalMoney, new BigInteger(0));
            Assert.AreEqual(building.BuildingsOwned, 1);
        }

        [TestMethod]
        public void TestPrettyValueOfOneBuilding()
        {
            var manager = new MoneyManager();
            manager.TotalMoney += 10;

            Assert.AreEqual("$10", manager.GetPrettyTotal());
        }
    }
}
