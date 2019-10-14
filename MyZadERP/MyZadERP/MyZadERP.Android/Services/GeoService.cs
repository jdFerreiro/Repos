using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MyZadERP.Interfaces;
using MyZadERP.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Android.Locations.Location;

namespace MyZadERP.Droid.Services
{

    [Service(Exported = true, 
        Enabled = true,
        Name = "com.zadesoft.MyZadERP.GeoServices",
        Permission = "android.permission.BIND_JOB_SERVICE")]
    public class GeoService : JobService
    {
        LocationManager _locationManager;
        static readonly string TAG = "X:" + typeof(GeoService).Name;
        static readonly int TimerWait = 30000;
        Timer timer;
        DateTime startTime;
        bool _isStarted = false;

        public override bool OnStartJob(JobParameters @params)
        {
            App.EndTime = DateTime.Now;
            TimeSpan diffTime = App.EndTime - App.StartTime;

            Task.Run(() =>
            {

                try
                {
                    // Log.Debug(TAG, $"Inicia Servicio GeoLocation ejecutado a las {startTime}, flags = {flags}, startid = {startId}");
                    Log.Debug(TAG, $"Inicia Servicio GeoLocation ejecutado a las {startTime}");
                    if (_isStarted)
                    {
                        TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                        Log.Debug(TAG, $"El Servicio GeoLocation ya esta iniciado, Este se ha ejecutado a las {runtime:c}.");
                    }
                    else
                    {
                        _locationManager = (LocationManager)GetSystemService(LocationService);
                        startTime = DateTime.UtcNow;
                        Log.Debug(TAG, $"Inicio del Servicio de GeoLocation, a las {startTime}.");
                        timer = new Timer(async delegate (object state)
                        {
                            Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync().ConfigureAwait(false);
                            if (location != null)
                            {
                                GeoServices _geoServices = new GeoServices(location);
                                await _geoServices.UpdateGeolocation().ConfigureAwait(false);
                            }
                        }, startTime, 0, TimerWait);
                        _isStarted = true;
                    }
                    JobFinished(@params, true);
                    return _isStarted; // StartCommandResult.NotSticky;
                }
                catch (Exception fail)
                {
                    Log.Debug(TAG, $"Excepcion: {fail.Message}");
                    return false; // StartCommandResult.StickyCompatibility;
                }
            });
            return false;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return false;
        }
    }

    //[Service(Exported = true, Name = "com.zadesoft.MyZadERP.GeoServices")]
    //public class GeoServices : Service
    //{
    //    const long ONE_MINUTE = 60 * 1000;
    //    const long FIVE_MINUTES = 5 * ONE_MINUTE;

    //    static readonly int RC_LAST_LOCATION_PERMISSION_CHECK = 1000;
    //    static readonly int RC_LOCATION_UPDATES_PERMISSION_CHECK = 1100;

    //    bool _isRequestingLocationUpdates;

    //    private Bundle _bundle;
    //    private string _latitude;
    //    private string _longitude;
    //    private string _provider;


    //    LocationManager _locationManager;
    //    static readonly string TAG = "X:" + typeof(GeoServices).Name;
    //    static readonly int TimerWait = 10000;
    //    Timer timer;
    //    DateTime startTime;
    //    bool _isStarted = false;

    //    public override IBinder OnBind(Intent intent)
    //    {
    //        return null;
    //    }

    //    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    //    {
    //        try
    //        {
    //            Log.Debug(TAG, $"Inicia Servicio GeoLocation ejecutado a las {startTime}, flags = {flags}, startid = {startId}");
    //            if (_isStarted)
    //            {
    //                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
    //                Log.Debug(TAG, $"El Servicio GeoLocation ya esta iniciado, Este se ha ejecutado a las {runtime:c}.");
    //            }
    //            else
    //            {
    //                _locationManager = (LocationManager)GetSystemService(LocationService);
    //                startTime = DateTime.UtcNow;
    //                Log.Debug(TAG, $"Inicio del Servicio de GeoLocation, a las {startTime}.");
    //                timer = new Timer(async delegate(object state)
    //                {
    //                    Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync();
    //                    if (location != null)
    //                    {
    //                        Services.GeoServices _geoServices = new Core.Services.GeoServices(location);
    //                        await _geoServices.UpdateGeolocation();
    //                    }
    //                }, startTime, 0, TimerWait);
    //                _isStarted = true;
    //            }
    //            return StartCommandResult.NotSticky;
    //        }
    //        catch (Exception fail)
    //        {
    //            Log.Debug(TAG, $"Excepcion: {fail.Message}");
    //            return StartCommandResult.StickyCompatibility;
    //        }
    //    }

    //    public override void OnDestroy()
    //    {
    //        timer.Dispose();
    //        timer = null;
    //        _isStarted = false;

    //        TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
    //        Log.Debug(TAG, $"Servicio de GeoLocation destruido a las {DateTime.UtcNow} luego de ejecutarse por {runtime:c}.");
    //        base.OnDestroy();
    //    }

    //    public override void OnCreate()
    //    {
    //        base.OnCreate();
    //    }
    //}
}