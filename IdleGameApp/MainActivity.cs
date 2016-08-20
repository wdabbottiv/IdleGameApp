using System;
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

            SetUpBuildingButtons();


            Timer mainGameTimer = new Timer(100);
            mainGameTimer.Elapsed += HandleMainGameTimerTick;
            mainGameTimer.Start();


            InitializeHandlers();
        }

        private void SetUpBuildingButtons()
        {
            LinearLayout buildingButton1 = FindViewById<LinearLayout>(Resource.Id.MyBuilding1);
            buildingButton1.Click += HandleBuyBuilding1;

            Button buildingButton2 = FindViewById<Button>(Resource.Id.MyBuilding2);
            buildingButton2.Click += HandleBuyBuilding2;

            Button buildingButton3 = FindViewById<Button>(Resource.Id.MyBuilding3);
            buildingButton3.Click += HandleBuyBuilding3;

            Button buildingButton4 = FindViewById<Button>(Resource.Id.MyBuilding4);
            buildingButton4.Click += HandleBuyBuilding4;

            Button buildingButton5 = FindViewById<Button>(Resource.Id.MyBuilding5);
            buildingButton5.Click += HandleBuyBuilding5;

            Button buildingButton6 = FindViewById<Button>(Resource.Id.MyBuilding6);
            buildingButton6.Click += HandleBuyBuilding6;

            Button buildingButton7 = FindViewById<Button>(Resource.Id.MyBuilding7);
            buildingButton7.Click += HandleBuyBuilding7;

            Button buildingButton8 = FindViewById<Button>(Resource.Id.MyBuilding8);
            buildingButton8.Click += HandleBuyBuilding8;
        }

        private void HandleBuyBuilding1(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(1).HandleBuyBuilding();

            TextView buildingButton1OwnedText = FindViewById<TextView>(Resource.Id.MyBuilding1Owned);
            TextView buildingButton1Eps = FindViewById<TextView>(Resource.Id.MyBuilding1EarnedPerSecond);

            buildingButton1OwnedText.Text = $"{_buildingManager.GetBuilding(1).BuildingsOwned}";
            buildingButton1Eps.Text = $"Earning: {_buildingManager.GetBuilding(1).EarningsPerSecond}/S";
        }

        private void HandleBuyBuilding2(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(2).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding2);

            buildingButton.Text = $"{_buildingManager.GetBuilding(2).BuildingsOwned}";
        }

        private void HandleBuyBuilding3(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(3).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding3);

            buildingButton.Text = $"{_buildingManager.GetBuilding(3).BuildingsOwned}";
        }

        private void HandleBuyBuilding4(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(4).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding4);

            buildingButton.Text = $"{_buildingManager.GetBuilding(4).BuildingsOwned}";
        }

        private void HandleBuyBuilding5(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(5).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding5);

            buildingButton.Text = $"{_buildingManager.GetBuilding(5).BuildingsOwned}";
        }

        private void HandleBuyBuilding6(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(6).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding6);

            buildingButton.Text = $"{_buildingManager.GetBuilding(6).BuildingsOwned}";
        }

        private void HandleBuyBuilding7(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(7).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding7);

            buildingButton.Text = $"{_buildingManager.GetBuilding(7).BuildingsOwned}";
        }

        private void HandleBuyBuilding8(object sender, EventArgs e)
        {
            _buildingManager.GetBuilding(8).HandleBuyBuilding();

            Button buildingButton = FindViewById<Button>(Resource.Id.MyBuilding8);

            buildingButton.Text = $"{_buildingManager.GetBuilding(8).BuildingsOwned}";
        }


        private void HandleMainGameTimerTick(object sender, ElapsedEventArgs e)
        {
            LinearLayout buildingButton1 = FindViewById<LinearLayout>(Resource.Id.MyBuilding1);

            Button buildingButton2 = FindViewById<Button>(Resource.Id.MyBuilding2);
            Button buildingButton3 = FindViewById<Button>(Resource.Id.MyBuilding3);
            Button buildingButton4 = FindViewById<Button>(Resource.Id.MyBuilding4);
            Button buildingButton5 = FindViewById<Button>(Resource.Id.MyBuilding5);
            Button buildingButton6 = FindViewById<Button>(Resource.Id.MyBuilding6);
            Button buildingButton7 = FindViewById<Button>(Resource.Id.MyBuilding7);
            Button buildingButton8 = FindViewById<Button>(Resource.Id.MyBuilding8);

            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);

            RunOnUiThread(() => { buildingButton1.Enabled = _buildingManager.GetBuilding(1).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton2.Enabled = _buildingManager.GetBuilding(2).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton3.Enabled = _buildingManager.GetBuilding(3).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton4.Enabled = _buildingManager.GetBuilding(4).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton5.Enabled = _buildingManager.GetBuilding(5).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton6.Enabled = _buildingManager.GetBuilding(6).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton7.Enabled = _buildingManager.GetBuilding(7).IsButtonEnabled; });
            RunOnUiThread(() => { buildingButton8.Enabled = _buildingManager.GetBuilding(8).IsButtonEnabled; });
            RunOnUiThread(() => { buttonText.Text = _moneyManager.GetPrettyTotal(); });
        }

        private void InitializeHandlers()
        {
            _achievementManager = new AchievementManager();
            _moneyManager = new MoneyManager();
            _buildingManager = new BuildingManager(_moneyManager);

            InitializeBuildings();
        }

        private void InitializeBuildings()
        {
            _buildingManager.AddBuilding(10, 10);
            _buildingManager.GetBuilding(1).InitializeTimer();
            _buildingManager.GetBuilding(1).Wrapper = new UiWrapper
            {
                Layout = FindViewById<LinearLayout>(Resource.Id.MyBuilding1),
                BuildingsOwned = FindViewById<TextView>(Resource.Id.MyBuilding1Owned)
            };

            _buildingManager.AddBuilding(50, 20);
            _buildingManager.GetBuilding(2).InitializeTimer();

            _buildingManager.AddBuilding(100, 40);
            _buildingManager.GetBuilding(3).InitializeTimer();

            _buildingManager.AddBuilding(500, 100);
            _buildingManager.GetBuilding(4).InitializeTimer();

            _buildingManager.AddBuilding(1000, 150);
            _buildingManager.GetBuilding(5).InitializeTimer();

            _buildingManager.AddBuilding(5000, 300);
            _buildingManager.GetBuilding(6).InitializeTimer();

            _buildingManager.AddBuilding(10000, 500);
            _buildingManager.GetBuilding(7).InitializeTimer();

            _buildingManager.AddBuilding(100000, 2000);
            _buildingManager.GetBuilding(8).InitializeTimer();
        }

        private void HandleMainButtonPress(object sender, EventArgs e)
        {
            _achievementManager.ClickOccured();
            _moneyManager.TotalMoney += _moneyManager.ClickValue;
            TextView buttonText = FindViewById<TextView>(Resource.Id.MyButtonText);
            
            buttonText.Text = _moneyManager.GetPrettyTotal();
        }
    }
}

