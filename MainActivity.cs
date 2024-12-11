using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.App;
using Xamarin.Essentials;
using Android.Webkit;
using Android.Media;

namespace P.A.Y.B
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private MediaPlayer mediaPlayer;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Toast.MakeText(this, "Created by Group 5!", ToastLength.Short).Show();

            Button HotlineButton = FindViewById<Button>(Resource.Id.HotlineButton);
            // Set the click listener for the hotline button
            HotlineButton.Click += (sender, e) => StartActivity(typeof(HotlineActivity));


            Button LocatorButton = FindViewById<Button>(Resource.Id.LocatorButton);
            // Set the click listener for the locator button
            LocatorButton.Click += (sender, e) => StartActivity(typeof(LocatorActivity));

            Button PricingButton = FindViewById<Button>(Resource.Id.PricingButton);
            // Set the click listener for the locator button
            PricingButton.Click += (sender, e) => StartActivity(typeof(PricingActivity));

            Button AboutusButton = FindViewById<Button>(Resource.Id.AboutusButton);
            // Set the click listener for the locator button
            AboutusButton.Click += (sender, e) => StartActivity(typeof(AboutUsActivity));



            var sirenButton = FindViewById<ImageButton>(Resource.Id.SOSButton);
            mediaPlayer = MediaPlayer.Create(this, Resource.Raw.siren);
            bool isPlaying = false;
            sirenButton.Click += (sender, e) =>
            {
                if (isPlaying)
                {
                    // If the sound is playing, stop it and release resources
                    mediaPlayer?.Stop();
                    mediaPlayer?.Release();
                    mediaPlayer = null;
                    isPlaying = false;  // Set the flag to false
                }
                else
                {
                    // If the sound is not playing, start it and set to loop
                    mediaPlayer = MediaPlayer.Create(this, Resource.Raw.siren);  // Use the sound file from Resources/raw
                    mediaPlayer.Looping = true;  // Set the MediaPlayer to loop the sound
                    mediaPlayer.Start();
                    isPlaying = true;  // Set the flag to true
                }
            };
        }
        protected override void OnPause()
        {
            base.OnPause();
            // Release the MediaPlayer when the activity is paused
            mediaPlayer?.Release();
            mediaPlayer = null;
        }

        protected override void OnStop()
        {
            base.OnStop();
            // Release the MediaPlayer when the activity is stopped
            mediaPlayer?.Release();
            mediaPlayer = null;
        }

    }

    [Activity(Label = "Emergency Hotline")]
    public class HotlineActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hotline); // Set your new layout

            Button callButton = FindViewById<Button>(Resource.Id.PNPButton);

            //:
            
            var phoneNumbers = new Dictionary<int, string>
            {
                { Resource.Id.DOHButton, Resources.GetString(Resource.String.doh_hotline_number) },
                { Resource.Id.PNPButton, Resources.GetString(Resource.String.pnp_hotline_number) },
                { Resource.Id.PEHButton, Resources.GetString(Resource.String.peh_hotline_number) },
                { Resource.Id.PRCButton, Resources.GetString(Resource.String.prc_hotline_number) },
                { Resource.Id.BFPButton, Resources.GetString(Resource.String.bfp_hotline_number) },
                { Resource.Id.DPWHButton, Resources.GetString(Resource.String.dpwh_hotline_number) },
                { Resource.Id.MMDAButton, Resources.GetString(Resource.String.mmda_hotline_number) },
                { Resource.Id.PAGASAButton, Resources.GetString(Resource.String.pagasa_hotline_number) },
                { Resource.Id.NDRRMCButton, Resources.GetString(Resource.String.ndrrmc_hotline_number) },
            // Add more buttons and phone numbers as needed
            };

            var buttonIds = new[] {
                Resource.Id.DOHButton, Resource.Id.PNPButton, Resource.Id.PEHButton,
                Resource.Id.PRCButton, Resource.Id.BFPButton, Resource.Id.DPWHButton,
                Resource.Id.MMDAButton, Resource.Id.PAGASAButton,
                Resource.Id.NDRRMCButton };

            foreach (var buttonId in buttonIds)
            {
                Button button = FindViewById<Button>(buttonId);
                button.Click += (sender, e) =>
                {
                    string phoneNumber = phoneNumbers[buttonId];
                    var uri = Android.Net.Uri.Parse($"tel:{phoneNumber}");
                    var intent = new Intent(Intent.ActionDial, uri);

                    StartActivity(intent); // Open the phone dialer
                };

            };
            //:

        }
    }

    [Activity(Label = "Locator")]
    public class LocatorActivity : Activity
    {
        private WebView _webView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_locator);

            var location = await Geolocation.GetLastKnownLocationAsync();

            //string locate = await GetLocation();
            Toast.MakeText(this, $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", ToastLength.Short).Show();

            _webView = FindViewById<WebView>(Resource.Id.webView);

            // Enable JavaScript in WebView
            _webView.Settings.JavaScriptEnabled = true;

            // Enable wide viewport and set zoom controls
            _webView.Settings.SetSupportZoom(true);

            _webView.Settings.BuiltInZoomControls = true;
            _webView.Settings.DisplayZoomControls = false;

            // Load the OpenStreetMap URL
            _webView.LoadUrl($"https://www.openstreetmap.org/?mlat={location.Latitude}&mlon={location.Longitude}");

            // Optionally handle back press for WebView navigation
            _webView.SetWebViewClient(new WebViewClient());
        }
    }

    [Activity(Label = "Pricing")] // Activity label for this screen
    public class PricingActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pricing); // Set the layout for this activity

            // You can add any code for pricing-related functionality here
            // For example, displaying a pricing list, or interacting with a pricing API.
        }
    }

    [Activity(Label = "About Us")]
    public class AboutUsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_aboutus);

            // Find the TextView for the About Us Description
            var descriptionTextView = FindViewById<TextView>(Resource.Id.aboutUsDescription);

            // Read the content of the about_us.txt file from the Assets folder
            string aboutUsText = GetString(Resource.String.about_us_description);

            // Set the text to the About Us TextView
            descriptionTextView.Text = aboutUsText;

            // Optionally, set up the back button
            var backButton = FindViewById<Button>(Resource.Id.backButton);
            backButton.Click += (sender, e) =>
            {
                Finish(); // Close the activity and return to the previous screen
            };
        }

        // Method to read the text file from the Assets folder
        private string ReadAssetFile(string filename)
        {
            using (var reader = new StreamReader(Assets.Open(filename)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}