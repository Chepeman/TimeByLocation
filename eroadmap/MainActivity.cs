using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Text.Format;
using Java.Text;
using Java.Util;

namespace eroadmap
{
	[Activity(Label = "eroadmap", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		MapFragment _mapFrag;
		GoogleMap _map;
		Location _myLocation;

		TextView _latLonTV;
		TextView _positionTimeTV;
		TextView _UTCTimeTV;
		TextView _localTimeTV;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			MapFragment _mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
			_latLonTV = (TextView)this.FindViewById(Resource.Id.tvLatLon);
			_localTimeTV = (TextView)this.FindViewById(Resource.Id.tvLocalTime);
			_UTCTimeTV = (TextView)this.FindViewById(Resource.Id.tvUTCTime);
			_positionTimeTV = (TextView)this.FindViewById(Resource.Id.tvTimeLocation);

			_map = _mapFrag.Map;
			if (_map != null)
			{
				_map.MapType = GoogleMap.MapTypeNormal;
				_map.MyLocationEnabled = true;
				_map.MyLocationChange += _map_MyLocationChange;
			}



		}

		private void CenterMapOnMyLocation()
		{
			var location = _myLocation;

			if (location != null)
			{
				var myLocation= new LatLng(location.Latitude, location.Longitude);
				_map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(myLocation, (float)14.6));
			}

		}

		void _map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
		{
			_myLocation = e.Location;

			SimpleDateFormat dateFormater = new SimpleDateFormat("HH:mm:ss");
			Date date = new Date();

			Java.Text.DateFormat dateFormat = Java.Text.DateFormat.TimeInstance;
			dateFormat.TimeZone = TimeZone.GetTimeZone("gmt");

			Date datePosition = new Date(_myLocation.Time);


			_latLonTV.Text = "Lat/Lon: " + _myLocation.Latitude + "/" + _myLocation.Longitude;
			_localTimeTV.Text = "LocalTime: " + dateFormater.Format(date);
			_UTCTimeTV.Text = "UTCTime: " + dateFormat.Format(new Date());
			_positionTimeTV.Text = "LocationTime: " + dateFormater.Format(datePosition);
			CenterMapOnMyLocation();
		}

	}
}


