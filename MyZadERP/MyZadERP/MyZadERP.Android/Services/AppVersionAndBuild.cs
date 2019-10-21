using Android.Content.PM;
using MyZadERP.Droid.Services;
using MyZadERP.Interfaces;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionAndBuild))]
namespace MyZadERP.Droid.Services
{

    public class AppVersionAndBuild : IAppVersionAndBuild
    {

        PackageInfo appInfo;

        public AppVersionAndBuild()
        {
            var context = Android.App.Application.Context;
            appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }

        public string GetVersion()
        {
            string version = $"version {appInfo.VersionName}.{appInfo.VersionCode.ToString()}";
            return version;
        }
    }
}