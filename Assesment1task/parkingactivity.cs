using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using AndroidX.ViewPager.Widget;

using Google.Android.Material.Tabs;
using Java.Lang;

using System;
using ActionBar = Android.App.ActionBar;


namespace Assesment1task
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class parkingactivity : AppCompatActivity
    {
        public Toolbar toolbar;
        public TabLayout _mytabLayout;
   
        public FrameLayout frameLayout;

     
        //public GifImageView myGIFImage;

        AndroidX.Fragment.App.Fragment fragment;
        AndroidX.Fragment.App.Fragment fragment1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.parkingact);

            _mytabLayout = FindViewById<TabLayout>(Resource.Id.tabLayout);


            toolbar = FindViewById<Toolbar>(Resource.Id.tool1);
          
          
            setTabName();
            fragment1 = new fragmentOne();
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayoutEx, fragment1).Commit();
            _mytabLayout.TabSelected += (object sender, TabLayout.TabSelectedEventArgs e) =>
            {
                AndroidX.Fragment.App.Fragment fragment = null;
                var tab = e.Tab;

                switch (tab.Position)
                {
                    case 0:
                        fragment = new fragmentOne();
                        break;
                    case 1:
                        fragment = new fragmentTwo();
                        break;
                }
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frameLayoutEx, fragment).Commit();

            };

        }
        private void setTabName()
        {
            _mytabLayout.AddTab(_mytabLayout.NewTab().SetText("Map"));
            _mytabLayout.AddTab(_mytabLayout.NewTab().SetText("List"));

        }


       
      


    }
}