using System;
using System.Drawing;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Widget;
using IdleGameApp.Helpers;
using IdleGameApp.Helpers.Achievements;

namespace IdleGameApp
{
    [Activity(Label = "Political Planning", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AchievementManager _achievementManager;
        private MoneyManager _moneyManager;
        private BuildingManager _buildingManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.MyButton);
            layout.Click += HandleMainButtonPress;

            Timer mainGameTimer = new Timer(100);
            mainGameTimer.Elapsed += HandleMainGameTimerTick;
            mainGameTimer.Start();


            InitializeHandlers();
            InitializeBuildings();
        }

        private void HandleMainButtonPress(object sender, EventArgs e)
        {
            _achievementManager.ClickOccured();
            _moneyManager.TotalMoney += _moneyManager.ClickValue;
            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);

            buttonText.Text = _moneyManager.GetPrettyTotal();
        }

        private void HandleMainGameTimerTick(object sender, ElapsedEventArgs e)
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                var protectedIterator = i;
                RunOnUiThread(() =>
                {
                    _buildingManager.GetBuilding(protectedIterator).Wrapper.Layout.Background.Alpha =
                        _buildingManager.GetBuilding(protectedIterator).IsButtonEnabled ? 255 : 60;
                });
            }

            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);
            RunOnUiThread(() => { buttonText.Text = _moneyManager.GetPrettyTotal(); });
        }

        private void InitializeHandlers()
        {
            _achievementManager = new AchievementManager();
            _moneyManager = new MoneyManager();
            _buildingManager = new BuildingManager(_moneyManager);
        }

        private void InitializeBuildings()
        {
            InitializePhaseOneBuildings();

            CreateUiComponentsForBuildings();

            SetUpBuildingButtonClickHandlers();

            AddBuildingsToUi();
        }

        private void InitializePhaseOneBuildings()
        {
            _buildingManager.AddBuilding(10, 10);
            _buildingManager.AddBuilding(50, 20);
            _buildingManager.AddBuilding(100, 40);
            _buildingManager.AddBuilding(500, 100);
            _buildingManager.AddBuilding(1000, 150);
            _buildingManager.AddBuilding(5000, 300);
            _buildingManager.AddBuilding(10000, 500);
            _buildingManager.AddBuilding(100000, 2000);
        }

        private void CreateUiComponentsForBuildings()
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                var building = _buildingManager.GetBuilding(i);
                building.InitializeTimer();
                building.Wrapper.InitializeUi(this.ApplicationContext,
                    i % 2 == 0 ? Android.Graphics.Color.Cyan : Android.Graphics.Color.White);
                building.Wrapper.UpdateBuildingsOwned(building.BuildingsOwned);
                building.Wrapper.UpdateEarningPerSecond(building.EarningsPerSecond);
                building.Wrapper.UpdateCostToBuyNext(building.BuildingCost);
            }
        }

        private void SetUpBuildingButtonClickHandlers()
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                _buildingManager.GetBuilding(i).Wrapper.Layout.Click +=
                    _buildingManager.GetBuilding(i).HandleClickEvent;
            }
        }

        private void AddBuildingsToUi()
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.TestLayout);

                layout.AddView(_buildingManager.GetBuilding(i).Wrapper.Layout);
            }
        }
    }
}

