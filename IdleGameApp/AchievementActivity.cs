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
using IdleGameApp.Helpers;
using IdleGameApp.Helpers.Achievements;
using Newtonsoft.Json;

namespace IdleGameApp
{
    [Activity(Label = "AchievementActivity")]
    public class AchievementActivity : Activity
    {
        private AchievementManager _achievementManager;
        private BuildingManager _buildingManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AchievementLayout);

            _achievementManager = JsonConvert.DeserializeObject<AchievementManager>(Intent.GetStringExtra("AchievementManager"),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            _buildingManager = JsonConvert.DeserializeObject<BuildingManager>(Intent.GetStringExtra("BuildingManager"));

            DrawMenu();
            InitializeAchievements();
            RunOnUiThread(() => DrawUi());
        }

        private void DrawMenu()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.MainMenu2);

            var button = new Button(this.ApplicationContext);
            button.Text = "Home";
            button.Click += OnHomeMenuClick;

            layout.AddView(button);
        }

        private void OnHomeMenuClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            var stuff = JsonConvert.SerializeObject(_buildingManager, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            intent.PutExtra("AchievementManager", stuff);
            intent.PutExtra("BuildingManager", JsonConvert.SerializeObject(_buildingManager));
            StartActivity(intent);
        }

        private void DrawUi()
        {
            for (int i = 0; i < _achievementManager.NumberOfAchievements; i++)
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.AchievementSection);

                layout.AddView(_achievementManager.GetAchievement(i).Wrapper.Layout);
            }
        }

        private void InitializeAchievements()
        {
            for (int i = 0; i < _achievementManager.NumberOfAchievements; ++i)
            {
                _achievementManager.GetAchievement(i).Wrapper.InitializeUi(this.ApplicationContext);
            }
        }
    }
}