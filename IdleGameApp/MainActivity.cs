using System;
using System.Drawing;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using IdleGameApp.Helpers;
using IdleGameApp.Helpers.Achievements;
using Newtonsoft.Json;

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

            if (Intent.GetStringExtra("AchievementManager") != null)
            {
                _achievementManager = JsonConvert.DeserializeObject<AchievementManager>(Intent.GetStringExtra("AchievementManager"),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }

            if (Intent.GetStringExtra("BuildingManager") != null)
            {
                _buildingManager = JsonConvert.DeserializeObject<BuildingManager>(Intent.GetStringExtra("BuildingManager"),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }
            


            InitializeHandlers();
            InitializeBuildings();
            DrawMenu();
        }

        private void DrawMenu()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.MainMenu);

            var button = new Button(this.ApplicationContext);
            button.Text = "Achievements";
            button.Click += OnAchievementMenuClick;

            layout.AddView(button);
        }

        private void OnAchievementMenuClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AchievementActivity));
            var stuff = JsonConvert.SerializeObject(_buildingManager, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            intent.PutExtra("AchievementManager", stuff);
            intent.PutExtra("BuildingManager", JsonConvert.SerializeObject(_buildingManager));
            StartActivity(intent);
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
                    _buildingManager.GetBuilding(protectedIterator).GetUiWrapper().Layout.Background.Alpha =
                        _buildingManager.GetBuilding(protectedIterator).IsButtonEnabled ? 255 : 60;
                });
            }

            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);
            RunOnUiThread(() => { buttonText.Text = _moneyManager.GetPrettyTotal(); });
        }

        private void InitializeHandlers()
        {
            _achievementManager = _achievementManager ?? new AchievementManager();
            _moneyManager = _moneyManager ?? new MoneyManager();
            _buildingManager = _buildingManager ?? new BuildingManager(_moneyManager);
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
                building.GetUiWrapper().InitializeUi(this.ApplicationContext,
                    i % 2 == 0 ? Android.Graphics.Color.Cyan : Android.Graphics.Color.White);
                building.GetUiWrapper().UpdateBuildingsOwned(building.BuildingsOwned);
                building.GetUiWrapper().UpdateEarningPerSecond(building.EarningsPerSecond);
                building.GetUiWrapper().UpdateCostToBuyNext(building.BuildingCost);
            }
        }

        private void SetUpBuildingButtonClickHandlers()
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                _buildingManager.GetBuilding(i).GetUiWrapper().Layout.Click +=
                    _buildingManager.GetBuilding(i).HandleClickEvent;
            }
        }

        private void AddBuildingsToUi()
        {
            for (int i = 1; i <= _buildingManager.NumberOfBuildings; i++)
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.TestLayout);

                layout.AddView(_buildingManager.GetBuilding(i).GetUiWrapper().Layout);
            }
        }
    }
}

