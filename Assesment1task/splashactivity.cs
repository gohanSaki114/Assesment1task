using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment1task
{
    [Activity(Label = "splashactivity", NoHistory = true,Theme = "@style/SplashTheme")]
    public class splashactivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override async void OnResume()
        {
            base.OnResume();
            await SimulteStartup();
        }

        async Task SimulteStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}