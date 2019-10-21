using System;
using System.IO;
using System.Net;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using MyZadERP.Droid.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

using Application = Android.App.Application;
using Environment = System.Environment;
using Android.App.Job;

namespace MyZadERP.Droid
{
    [Activity(Label = "My Zad ERP", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private MyZadERP.App _app;
        //private Intent _geoService;
        //private bool _geoServiceStart;
        private bool _geoPermission;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                _geoPermission = false;
                GetPermissions();

                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                Platform.Init(this, savedInstanceState);
                Forms.Init(this, savedInstanceState);
                string fileName = "MyZadERP_db.db3";
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
                string fullPathFile = Path.Combine(folderPath, fileName);
                var pixelWidth = (int)Resources.DisplayMetrics.WidthPixels;
                var pixelHeight = (int)Resources.DisplayMetrics.HeightPixels;
                var screenPixelDensity = (double)Resources.DisplayMetrics.Density;

                //_geoServiceStart = false;
                /* Inicio de Servicio Geolocalización */
                //_geoService = new Intent(this, typeof(GeoServices));
                //_geoServiceStart = true;
                //StartService(_geoService);
                /* Fin */

                /* Inicio de JobScheduler */

                App.jobBuilder = this.CreateJobBuilderUsingJobId<GeoService>(1)
                    .SetPersisted(true)
                    .SetBackoffCriteria(30000, Android.App.Job.BackoffPolicy.Linear)
                    .SetRequiresDeviceIdle(false)
                    .SetOverrideDeadline(5000)
                    .SetMinimumLatency(500)
                    .SetRequiresCharging(false);


                App.jobInfo = App.jobBuilder.Build();  // creates a JobInfo object.

                App.jobBuilder.Dispose();

                App.jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);

                /* Fin */

                _app = new App(fullPathFile, (double)((pixelHeight - 0.5f) / screenPixelDensity), (double)((pixelWidth - 0.5f) / screenPixelDensity));
                LoadApplication(_app);
            }
            catch (Exception fail)
            {
                var context = Application.Context;
                Toast.MakeText(context, fail.Message, ToastLength.Long).Show();
            }
        }

        #region RuntimePermissions

        void GetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                GetPermissionsAsync();
            }
        }

        const int RequestLocationId = 0;

        readonly string[] PermissionsGroupLocation =
            {
                            Manifest.Permission.AccessCoarseLocation,
                            Manifest.Permission.AccessFineLocation,
             };

        bool GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                Toast.MakeText(this, "Permiso Especial Concedido", ToastLength.Short).Show();
                _geoPermission = true;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permisos");
                alert.SetMessage("La aplicación necesita permisos especiales para continuar.");
                alert.SetPositiveButton("Request Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroupLocation, RequestLocationId);
                    _geoPermission = true;
                });

                alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Cancelado!", ToastLength.Short).Show();
                    _geoPermission = false;
                });
                Dialog dialog = alert.Create();
                dialog.Show();

                alert.Dispose();
            }
            if (!_geoPermission)
                RequestPermissions(PermissionsGroupLocation, RequestLocationId);

            return _geoPermission;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "Permiso Especial concendido", ToastLength.Short).Show();
                            _geoPermission = true;
                        }
                        else
                        {
                            Toast.MakeText(this, "Permiso Especial denegado", ToastLength.Short).Show();
                            _geoPermission = false;
                        }
                    }
                    break;
            }
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }
}