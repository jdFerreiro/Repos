using Android.App;
using Android.App.Job;
using Android.Locations;
using Android.Util;
using Android.Widget;
using MyZadERP.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

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
        static readonly int TimerWait = 10000;
        Timer timer;
        DateTime startTime;
        bool _isStarted = false;

        public override bool OnStartJob(JobParameters @params)
        {
            if (_isStarted) return false;

            App.EndTime = DateTime.Now;
            TimeSpan diffTime = App.EndTime - App.StartTime;
            Android.Content.Context context = Application.Context;

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
                        Log.Debug(TAG, $"Inicio del Servicio de GeoLocation, a las {startTime}.");

                        timer = new Timer(async delegate (object state)
                        {
                            var request = new GeolocationRequest(GeolocationAccuracy.Best);
                            startTime = DateTime.UtcNow;

                            //Xamarin.Essentials.Location location = await Geolocation.GetLastKnownLocationAsync();
                            //if (location == null)
                            //{
                            //    string Address = string.Empty;
                            //    var locations = await Geocoding.GetLocationsAsync(Address);
                            //    location = locations?.FirstOrDefault();
                            //}

                            //var request = new GeolocationRequest((GeolocationAccuracy)accuracy);
                            CancellationToken cts = new CancellationToken();
                            try
                            {
                                var location = await Geolocation.GetLocationAsync(request, cts).ConfigureAwait(false);

                                if (location != null)
                                {
                                    GeoServices _geoServices = new GeoServices(location);
                                    await _geoServices.UpdateGeolocation().ConfigureAwait(false);
                                    Log.Debug(TAG, $"Actualización en BBDD, a las {startTime}.");
                                }
                                else
                                {
                                    Log.Debug(TAG, $"No se pudo obtener la geolocalización a las {startTime}.");
                                }
                            }
                            catch(Exception fail)
                            {
                                Log.Debug(TAG, $"Excepcion: {fail.Message}");
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
                    _isStarted = false;
                    return _isStarted; // StartCommandResult.StickyCompatibility;
                }
            });

            return _isStarted;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            Log.Debug(TAG, $"Job Stop at {DateTime.UtcNow}");
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