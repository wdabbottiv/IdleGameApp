using System;
using IdleGameApp.Helpers;
using IdleGameApp.Helpers.Achievements;
using Java.IO;

namespace IdleGameApp
{
    public class GameObject : ISerializable
    {
        public AchievementManager AchievementManager;
        public MoneyManager MoneyManager;
        public BuildingManager BuildingManager;

        public IntPtr Handle => new IntPtr(1);

        public void Dispose()
        {
        }
    }
}