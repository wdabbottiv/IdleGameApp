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
        private GameObject _gameObject;

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

            if (Intent.GetStringExtra("GameObject") != null)
            {
                _gameObject = JsonConvert.DeserializeObject<GameObject>(Intent.GetStringExtra("GameObject"),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            }

            _gameObject = _gameObject ?? new GameObject();

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
            intent.PutExtra("GameObject", JsonConvert.SerializeObject(_gameObject));
            StartActivity(intent);
        }

        private void HandleMainButtonPress(object sender, EventArgs e)
        {
            _gameObject.AchievementManager.ClickOccured();
            _gameObject.MoneyManager.TotalMoney += _gameObject.MoneyManager.ClickValue;
            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);

            buttonText.Text = _gameObject.MoneyManager.GetPrettyTotal();
        }

        private void HandleMainGameTimerTick(object sender, ElapsedEventArgs e)
        {
            for (int i = 1; i <= _gameObject.BuildingManager.NumberOfBuildings; i++)
            {
                var protectedIterator = i;
                RunOnUiThread(() =>
                {
                    _gameObject.BuildingManager.GetBuilding(protectedIterator).GetUiWrapper().Layout.Background.Alpha = _gameObject.BuildingManager.GetBuilding(protectedIterator).IsButtonEnabled ? 255 : 60;
                });
            }

            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);
            RunOnUiThread(() => { buttonText.Text = _gameObject.MoneyManager.GetPrettyTotal(); });
        }

        private void InitializeHandlers()
        {
            _gameObject.AchievementManager = _gameObject.AchievementManager ?? new AchievementManager();
            _gameObject.MoneyManager = _gameObject.MoneyManager ?? new MoneyManager();
            _gameObject.BuildingManager = _gameObject.BuildingManager ?? new BuildingManager(_gameObject.MoneyManager);
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
            _gameObject.BuildingManager.AddBuilding(10, 10);
            _gameObject.BuildingManager.AddBuilding(50, 20);
            _gameObject.BuildingManager.AddBuilding(100, 40);
            _gameObject.BuildingManager.AddBuilding(500, 100);
            _gameObject.BuildingManager.AddBuilding(1000, 150);
            _gameObject.BuildingManager.AddBuilding(5000, 300);
            _gameObject.BuildingManager.AddBuilding(10000, 500);
            _gameObject.BuildingManager.AddBuilding(100000, 2000);
        }

        private void CreateUiComponentsForBuildings()
        {
            for (int i = 1; i <= _gameObject.BuildingManager.NumberOfBuildings; i++)
            {
                var building = _gameObject.BuildingManager.GetBuilding(i);
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
            for (int i = 1; i <= _gameObject.BuildingManager.NumberOfBuildings; i++)
            {
                _gameObject.BuildingManager.GetBuilding(i).GetUiWrapper().Layout.Click += _gameObject.BuildingManager.GetBuilding(i).HandleClickEvent;
            }
        }

        private void AddBuildingsToUi()
        {
            for (int i = 1; i <= _gameObject.BuildingManager.NumberOfBuildings; i++)
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.TestLayout);

                layout.AddView(_gameObject.BuildingManager.GetBuilding(i).GetUiWrapper().Layout);
            }
        }
    }
}

