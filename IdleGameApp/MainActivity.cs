using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Widget;
using IdleGameApp.Helpers;
using IdleGameApp.Helpers.Achievements;

namespace IdleGameApp
{
    [Activity(Label = "HelloWorld", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AchievementManager _achievementManager;
        private MoneyManager _moneyManager;
        private Building _building;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding);

            buildingButton.Enabled = false;

            button.Click += HandleMainButtonPress;
            buildingButton.Click += HandleBuyBuilding;

            Timer mainGameTimer = new Timer(100);
            mainGameTimer.Elapsed += HandleMainGameTimerTick;
            mainGameTimer.Start();


            InitializeHandlers();
        }

        private void HandleBuyBuilding(object sender, EventArgs e)
        {
            _building.HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding);

            buildingButton.Text = $"{_building.BuildingsOwned}";
        }

        private void HandleMainGameTimerTick(object sender, ElapsedEventArgs e)
        {
            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding);
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            RunOnUiThread(() => { buildingButton.Enabled = _building.IsButtonEnabled; });
            RunOnUiThread(() => { button.Text = _moneyManager.GetPrettyTotal(); });
        }

        private void InitializeHandlers()
        {
            _achievementManager = new AchievementManager();
            _moneyManager = new MoneyManager();
            _building = new Building(10, 10, _moneyManager);
            //TODO figure out how to initialize the building timer without crashing everything
            //_building.InitializeTimer();
        }

        private void HandleMainButtonPress(object sender, EventArgs e)
        {
            _achievementManager.ClickOccured();
            _moneyManager.TotalMoney += _moneyManager.ClickValue;
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            
            button.Text = _moneyManager.GetPrettyTotal();
        }
    }
}

