using System;
using System.Text;
using System.Collections.Generic;
using HelloWorld.Helpers.Achievements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
    /// <summary>
    /// Summary description for AchievementTest
    /// </summary>
    [TestClass]
    public class AchievementTest
    {
        public AchievementTest()
        {
        }

        [TestMethod]
        public void TestFirstLevelAchievementReached()
        {
            var numberOfClicksAchievement = new NumberOfClicksAchievement(new List<int>() {1, 5, 10, 100, 1000});
            numberOfClicksAchievement.Increment();

            Assert.AreEqual(1, numberOfClicksAchievement.TotalClicks);
            Assert.AreEqual(1, numberOfClicksAchievement.LevelReached);
        }

        [TestMethod]
        public void TestSecondLevelAchievementReached()
        {
            var numberOfClicksAchievement = new NumberOfClicksAchievement(new List<int>() { 1, 5, 10, 100, 1000 });
            numberOfClicksAchievement.Increment(5);

            Assert.AreEqual(5, numberOfClicksAchievement.TotalClicks);
            Assert.AreEqual(2, numberOfClicksAchievement.LevelReached);
        }

        [TestMethod]
        public void TestFifthLevelAchievementReached()
        {
            var numberOfClicksAchievement = new NumberOfClicksAchievement(new List<int>() { 1, 5, 10, 100, 1000 });
            numberOfClicksAchievement.Increment(1001);

            Assert.AreEqual(1001, numberOfClicksAchievement.TotalClicks);
            Assert.AreEqual(5, numberOfClicksAchievement.LevelReached);
        }
    }
}
