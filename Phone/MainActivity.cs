using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace Phone
{
    [Activity(Label = "Phone", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var phoneNumberText = FindViewById<EditText>(Resources.GetIdentifier("PhoneNumberText", "EditText","Phone"));
            var translateButton = FindViewById<Button>(Resources.GetIdentifier("TranslateButton", "Button", "Phone"));
            var callButton = FindViewById<Button>(Resources.GetIdentifier("CallButton", "Button", "Phone"));

            callButton.Enabled = false;

            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                translatedNumber = Phone.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;

                }
            };

            callButton.Click += (object sender, EventArgs e) =>
            {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate
                    {
                        var callIntent = new Intent(Intent.ActionCall);
                        callIntent.SetData(Android.Net.Uri.Parse("tel: " + translatedNumber));
                        StartActivity(callIntent);
                    });
                callDialog.Show();
            };

        }
    }
}

