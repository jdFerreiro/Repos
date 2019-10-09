using BCMWeb.Interfaces;
using BCMWeb.iOS.Services;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;


namespace BCMWeb.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        static BatteryService batteryService = new BatteryService();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations

                containerRegistry.RegisterInstance<IBatteryService>(batteryService);
            }
        }
    }
}
