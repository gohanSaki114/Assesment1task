
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment1task
{
    public class fragmentOne : Fragment
    {
        private ImageView imageViewOne, imageViewTwo;
        private TextView textViewOne, textViewTwo;
  
        private readonly string _tag = "Main Activity";
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment



            View view = inflater.Inflate(Resource.Layout.fragmentone, container, false);

            return view;
          


        }
     
    }
}