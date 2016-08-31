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
        private GameObject _gameObject;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AchievementLayout);
            
            _gameObject = JsonConvert.DeserializeObject<GameObject>(Intent.GetStringExtra("GameObject"),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

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

            intent.PutExtra("GameObject", JsonConvert.SerializeObject(_gameObject));
            StartActivity(intent);
        }

        private void DrawUi()
        {
            for (int i = 0; i < _gameObject.AchievementManager.NumberOfAchievements; i++)
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.AchievementSection);

                layout.AddView(_gameObject.AchievementManager.GetAchievement(i).Wrapper.Layout);
            }
        }

        private void InitializeAchievements()
        {
            for (int i = 0; i < _gameObject.AchievementManager.NumberOfAchievements; ++i)
            {
                _gameObject.AchievementManager.GetAchievement(i).Wrapper.InitializeUi(this.ApplicationContext);
            }
        }
    }
}