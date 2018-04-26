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
using ConsultasSP.Droid.Activities.Usuario;

namespace ConsultasSP.Droid.Activities.Launcher
{
    class CountDownLauncher : CountDownTimer
    {
        private Activity _act;
        private Activity _actLaunch;
        private bool _result;

        public CountDownLauncher(long millisInFuture, long countDown, Activity act) :
            base(millisInFuture, countDown)
        {
            _act = act;
        }

        public override void OnFinish()
        {
            _act.StartActivity(typeof(LoginActivity));
            _act.Finish();
        }

        public override void OnTick(long millisUntilFinished)
        {
            throw new NotImplementedException();
        }
    }
}