using Android.Content;
using Android.OS;
using Newtonsoft.Json;
using System.Net.Http;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidX.RecyclerView.Widget;

namespace Assesment1task
{
    public class fragmentTwo : Fragment
    {
        Android.App.ProgressDialog progress;
        public string url = "https://run.mocky.io/v3/46fd784a-979a-493d-a4f9-5cd08749fc6e";
        List<listmodel> data = new List<listmodel>();
        RecyclerView recycler;
        recycleviewAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
        }

        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view =inflater.Inflate(Resource.Layout.fragment2, container, false);
            progress = new Android.App.ProgressDialog(Activity);
            prog();
            bringdataAsync(view);
            return view;

        }
        private void prog()
        {
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }
        private async Task bringdataAsync(View view)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            progress.Cancel();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<listmodel>>(jsonString);
                data.AddRange(posts);
                RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(Context);
                recycler = view.FindViewById<RecyclerView>(Resource.Id.recycler);

                recycler.SetLayoutManager(mLayoutManager);
                adapter = new recycleviewAdapter(data,Activity);
                recycler.SetAdapter(adapter);
                Console.WriteLine($"First post title: {posts[0].locationName}");
                Console.ReadKey();

            }

        }
    }
}