﻿using System;
using System.Collections.Generic;

namespace UnitTestProject2
{
    internal class AchievementManager
    {
        private readonly NumberOfClicksAchievement _numberOfClicks =
            new NumberOfClicksAchievement(
            new List<int>() { 1, 5, 20, 100, 1000 }
            );
        public AchievementManager()
        {
        }

        public int TotalClicks { get; internal set; }

        public void ClickOccured()
        {
            _numberOfClicks.Increment();
        }
    }
}