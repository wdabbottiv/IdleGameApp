using System;
using System.Collections.Generic;
using Java.IO;

namespace IdleGameApp.Helpers.Achievements
{
    public class AchievementManager : ISerializable
    {
        private readonly List<Achievement> _achievements;

        public AchievementManager()
        {
            _achievements = new List<Achievement>();

            _achievements.Add(
                new NumberOfClicksAchievement(
                    new List<int>() { 1, 5, 20, 100, 1000 }
            ));
        }

        public Achievement GetAchievement(int achievementToGet)
        {
            return _achievements[achievementToGet];
        }

        public int NumberOfAchievements => _achievements.Count;

        public IntPtr Handle => new IntPtr(1);

        public void ClickOccured()
        {
            foreach (var achievement in _achievements)
            {
                achievement.Increment(1);
            }
        }

        public void Add(Achievement achievement)
        {
            _achievements.Add(achievement);
        }

        public void Dispose()
        {
            
        }
    }
}