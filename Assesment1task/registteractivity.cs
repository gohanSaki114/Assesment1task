using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assesment1task
{
    [Activity(Label = "registteractivity", Theme = "@style/AppTheme")]
    public class registteractivity : AppCompatActivity
    {
        TextView logintext; 
        TextInputLayout emaillayout, passwordlayout;
        Android.App.ProgressDialog progress;
        TextInputEditText emailEditText, passWordEditText;
        MaterialButton signupbutton1;
        public string url = "https://reqres.in/api/register";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registerlayout);
            emaillayout = FindViewById<TextInputLayout>(Resource.Id.emailcontainer);
            passwordlayout = FindViewById<TextInputLayout>(Resource.Id.passwordcontainer);
            passWordEditText = FindViewById<TextInputEditText>(Resource.Id.passwordedittext);
            emailEditText = FindViewById<TextInputEditText>(Resource.Id.emailedittext);
            logintext = FindViewById<TextView>(Resource.Id.endText);
            signupbutton1 = FindViewById<MaterialButton>(Resource.Id.signupbutton2);
            signupbutton1.Click += Signupbutton1_Click;
            logintext.Click += Logintext_Click;
        }

        private void Signupbutton1_Click(object sender, EventArgs e)
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

        private void Logintext_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
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
                    Toast.MakeText(this, "user Registered", ToastLength.Long).Show();
                    progress.Cancel();
                    //Intent intent = new Intent(this, typeof(parkingactivity));
                    //StartActivity(intent);
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
    }
}