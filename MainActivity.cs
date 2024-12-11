using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.App;
using Xamarin.Essentials;
using Android.Webkit;

namespace P.A.Y.B
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
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
        }
    }
}