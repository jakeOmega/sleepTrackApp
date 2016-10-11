using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace sleepTrackApp
{
    [Activity(Label = "Sleep Track", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button recordButton;
        Button clearButton;
        Button copyButton;
        TextView viewText;
        protected override void OnCreate(Bundle bundle)
        {
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            if (bundle != null)
            {
                viewText.Text = bundle.GetString("priorClicks");
            }

            recordButton = FindViewById<Button>(Resource.Id.recordButton);
            clearButton = FindViewById<Button>(Resource.Id.clearButton);
            copyButton = FindViewById<Button>(Resource.Id.copyButton);
            viewText = (TextView) FindViewById<TextView>(Resource.Id.viewBox);
            recordButton.Click += record;
            clearButton.Click += clear;
            copyButton.Click += sendEmail;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("priorClicks", viewText.Text);

            base.OnSaveInstanceState(outState);
        }

        void record(object sender, EventArgs e)
        {
            viewText.Text += DateTime.Now.ToString() + "\n";

        }

        void clear(object sender, EventArgs e)
        {
            viewText.Text = "";

        }

        void sendEmail(object sender, EventArgs e)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);

            email.PutExtra(Android.Content.Intent.ExtraEmail,
                new string[] { "jakefishmaui@gmail.com" });

            email.PutExtra(Android.Content.Intent.ExtraSubject, "Sleep disturbances");

            email.PutExtra(Android.Content.Intent.ExtraText,
            viewText.Text);

            email.SetType("message/rfc822");
            StartActivity(Intent.CreateChooser(email, "Send Email Using: "));



        }

    }
}

