using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Media.Session.MediaSession;

namespace Assesment1task
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView registertext;
        TextInputLayout emaillayout, passwordlayout;
        Android.App.ProgressDialog progress;
        TextInputEditText emailEditText, passWordEditText;
        MaterialButton signupbutton;
        public string url = "https://reqres.in/api/login";
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            emaillayout = FindViewById<TextInputLayout>(Resource.Id.emailcontainer);
            passwordlayout = FindViewById<TextInputLayout>(Resource.Id.passwordcontainer);
            registertext = FindViewById<TextView>(Resource.Id.endText);
            passWordEditText = FindViewById<TextInputEditText>(Resource.Id.passwordedittext);
            emailEditText = FindViewById<TextInputEditText>(Resource.Id.emailedittext);
            signupbutton = FindViewById<MaterialButton>(Resource.Id.signupbutton);
            signupbutton.Click += Signupbutton_Click;
            registertext.Click += Registertext_Click;
        }

        private void Registertext_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(registteractivity));
            StartActivity(intent);  

        }

        private void Signupbutton_Click(object sender, EventArgs e)
        {
           
            string emailtext = emailEditText.Text;
            string passWordtext = passWordEditText.Text;
            loginmodel loginmodel = new loginmodel();
            loginmodel.email = emailtext;
            loginmodel.password = passWordtext;
            if (emailEditText.Text == "" || (passWordEditText.Text == ""))
            {
                emaillayout.Error = "email is empty";
                passwordlayout.Error = "password is empty";
            }
            else if (passWordEditText.Text == "")
            {
                passwordlayout.Error = "password is empty";
            }
                postdataAsync(loginmodel);
        }

        private void prog()
        {
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading... Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }

        private async void postdataAsync(loginmodel loginmodel)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
            string serializedObject = JsonConvert.SerializeObject(loginmodel);
            HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            try
            {
                progress = new Android.App.ProgressDialog(this);
                prog();
                HttpResponseMessage response = await httpClient.PostAsync(url, contentPost);
                var content = await response.Content.ReadAsStringAsync();
                loginmodel tok = JsonConvert.DeserializeObject<loginmodel>(content);
                response.Content.ToString();
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(this, "post sent ", ToastLength.Long).Show();
                    progress.Cancel();
                    Intent intent = new Intent(this, typeof(parkingactivity));
                    StartActivity(intent);
                }
                else
                {
                      progress.Cancel();
                    Toast.MakeText(this, "Invalid Credentials", ToastLength.Long).Show();
                }
                //return response;
            }
            catch (TaskCanceledException ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                //return new HttpResponseMessage();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
//mail": "eve.holt@reqres.in", 
//cityslicka
