using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IdleGameApp.Helpers
{
    public class BuildingManager
    {
        public int NumberOfBuildings => _buildings.Count;

        private List<Building> _buildings = new List<Building>();
        private MoneyManager _moneyManager;

        public BuildingManager(MoneyManager moneyManager)
        {
            this._moneyManager = moneyManager;
        }

        public Building GetBuilding(int v)
        {
            if (_buildings.Count >= v)
            {
                return _buildings[v - 1];
            }

            return null;
        }

        public void InstatiateBuildings(int numberOfBuildings)
        {
            for (int i = 0; i < numberOfBuildings; i++)
            {
                _buildings.Add(new Building(i, i, _moneyManager));
            }
            
        }

        public void AddBuilding(int buildingCost, int buildingProfit)
        {
            _buildings.Add(new Building(buildingCost, buildingProfit, _moneyManager));
        }
    }
}