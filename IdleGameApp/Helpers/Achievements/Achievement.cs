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
    public partial class Achievement : IAchieveable
    {
        public AchievementUiWrapper Wrapper = new AchievementUiWrapper();

        public virtual void Increment(int numberOfTimes)
        {
            
        }
    }
}