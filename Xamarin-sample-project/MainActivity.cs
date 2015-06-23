using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using IO.Presage;

namespace example
{
	[Activity (Label = "example", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Presage.Instance.Context = this.BaseContext;
			Presage.Instance.Start ();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			var interstitialEvents = Presage.Instance.AdToServe ("interstitial");
				
			interstitialEvents.AdClosed += (s, e) => {
				System.Diagnostics.Debug.WriteLine ("Ad closed");
			};

			interstitialEvents.AdFound += (s, e) => {
				System.Diagnostics.Debug.WriteLine ("Ad found");
			};

			interstitialEvents.AdNotFound += (s, e) => {
				System.Diagnostics.Debug.WriteLine ("Ad not found");
			};
		}
	}
}


