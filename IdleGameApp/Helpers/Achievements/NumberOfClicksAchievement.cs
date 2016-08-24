using System;
using System.Collections.Generic;

namespace IdleGameApp.Helpers.Achievements
{
    public class NumberOfClicksAchievement : Achievement
    {
        private int _numberOfClicks = 0;
        private int _levelReached = 0;

        private readonly List<int> _goals;

        public NumberOfClicksAchievement(List<int> goals)
        {
            _goals = goals;
        }

        public int TotalClicks => _numberOfClicks;

        public int LevelReached => _levelReached;

        public override void Increment(int numberOfTimes = 1)
        {
            _numberOfClicks += numberOfTimes;

            IncrementLevelIfGoalReached();
        }

        private void IncrementLevelIfGoalReached()
        {
            while (_levelReached < _goals.Count && _numberOfClicks >= _goals[_levelReached])
            {
                _levelReached++;
            }
        }
    }
}