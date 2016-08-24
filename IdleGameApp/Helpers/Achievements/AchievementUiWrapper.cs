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

namespace IdleGameApp.Helpers.Achievements
{
    public class AchievementUiWrapper
    {
        public LinearLayout Layout;
        private TextView _textView;

        public void InitializeUi(Context applicationContext)
        {
            Layout = new LinearLayout(applicationContext);

            var text = new TextView(applicationContext) {Text = "blah"};

            Layout.AddView(text);

            _textView = text;
        }
    }
}