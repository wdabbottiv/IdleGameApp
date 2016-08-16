using System;
using System.Timers;

namespace IdleGameApp.Helpers
{
    public class Building
    {
        public int BuildingsOwned = 0;
        public int BuildingCost = 10;
        public Timer BuildingTimer = new Timer();
        public int BuildingRevenue = 1;
        public MoneyManager MoneyManager;
        public bool IsButtonEnabled => MoneyManager.TotalMoney >= BuildingCost;

        public Building(MoneyManager manager)
        {
            MoneyManager = manager;
        }

        public Building(int buildingCost, int buildingRevenue, MoneyManager manager)
        {
            MoneyManager = manager;
            BuildingCost = buildingCost;
            BuildingRevenue = buildingRevenue;
        }

        private void InitializeTimer()
        {
            BuildingTimer.Elapsed += MoneyEarningTimerElapsed;
            BuildingTimer.Interval = 250;
            BuildingTimer.Enabled = true;
        }

        public void MoneyEarningTimerElapsed(object sender, EventArgs e)
        {
            MoneyManager.TotalMoney += BuildingsOwned * BuildingRevenue;
        }

        public void HandleBuyBuilding()
        {
            if (MoneyManager.TotalMoney >= BuildingCost)
            {
                BuildingsOwned += 1;
                MoneyManager.TotalMoney -= BuildingCost;
                BuildingCost *= 2;
            }
        }
    }
}