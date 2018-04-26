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
using Android.Views.Animations;
using Android.Content.PM;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace ConsultasSP.Droid.Activities.Launcher
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/AppTheme")]
    public class LauncherActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_launcher);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            CountDownLauncher countDown = new CountDownLauncher(5000, 5000, this);
            AppCenter.Start("c70b4da9-b9d8-444a-8889-71e24ecc9269", typeof(Analytics), typeof(Crashes));
            countDown.Start();
            StartAnim();
        }

        private void StartAnim()
        {
            Animation anim = AnimationUtils.LoadAnimation(this, Resource.Animation.alpha);
            anim.Reset();

            var lnMain = FindViewById<RelativeLayout>(Resource.Id.LauncherhMain);
            lnMain.ClearAnimation();
            lnMain.StartAnimation(anim);


            anim = AnimationUtils.LoadAnimation(this, Resource.Animation.translate);
            anim.Reset();


            var imgMain = FindViewById<ImageView>(Resource.Id.LauncherImagen);
            imgMain.ClearAnimation();
            imgMain.StartAnimation(anim);

            anim.Dispose();
        }
        public override void OnBackPressed()
        {
            return;
        }
    }
}