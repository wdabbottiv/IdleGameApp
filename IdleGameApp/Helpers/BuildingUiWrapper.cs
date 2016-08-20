using System;
using System.Drawing;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace IdleGameApp.Helpers
{
    public class BuildingUiWrapper
    {
        public LinearLayout Layout;
        public TextView EarningsPerSecond;
        public TextView BuildingsOwned;
        public TextView CostToBuyNext;

        internal void InitializeUi(Context applicationContext, Android.Graphics.Color backgroundColor)
        {
            var buildingLayout = new LinearLayout(applicationContext);
            buildingLayout.SetGravity(GravityFlags.CenterHorizontal);
            buildingLayout.Orientation = Orientation.Vertical;
            buildingLayout.SetBackgroundColor(backgroundColor);
            var earnedPerSecond = new TextView(applicationContext) {Text = "0"};
            var buildingsOwned = new TextView(applicationContext) {Text = "0"};
            var costToBuyNext = new TextView(applicationContext) { Text = "0" };

            buildingLayout.AddView(earnedPerSecond);
            buildingLayout.AddView(buildingsOwned);
            buildingLayout.AddView(costToBuyNext);

            Layout = buildingLayout;
            BuildingsOwned = buildingsOwned;
            EarningsPerSecond = earnedPerSecond;
            CostToBuyNext = costToBuyNext;
        }

        internal void UpdateEarningPerSecond(double earningsPerSecond)
        {
            EarningsPerSecond.Text = $"{earningsPerSecond}/S";
        }

        internal void UpdateBuildingsOwned(int buildingsOwned)
        {
            BuildingsOwned.Text = $"Owned : {buildingsOwned}";
        }

        internal void UpdateCostToBuyNext(int costToBuyNext)
        {
            CostToBuyNext.Text = $"Cost To Buy : {costToBuyNext}";
        }
    }
}