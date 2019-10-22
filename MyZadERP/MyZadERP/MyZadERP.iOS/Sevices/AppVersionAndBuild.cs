using Foundation;
using MyZadERP.Interfaces;
using MyZadERP.iOS.Sevices;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionAndBuild))]
namespace MyZadERP.iOS.Sevices
{
    public class AppVersionAndBuild : IAppVersionAndBuild
    {

        public string GetVersion()
        {
            string version = $"Version: {NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString()}.{NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString()}";
            return version;
        }
    }
}