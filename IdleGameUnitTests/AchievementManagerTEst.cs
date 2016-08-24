using System;
using System.Collections.Generic;
using IdleGameApp.Helpers.Achievements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleGameUnitTests
{
    [TestClass]
    public class AchievementManagerTEst
    {
        [TestMethod]
        public void TestMethod1()
        {
            var manager = new AchievementManager();

            Assert.AreEqual(1, manager.NumberOfAchievements);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var manager = new AchievementManager();
            var achievement = new NumberOfClicksAchievement(new List<int>() {1});
            manager.Add(achievement);


            Assert.AreEqual(2, manager.NumberOfAchievements);
            Assert.IsNotNull(manager.GetAchievement(1));
        }
    }
}
