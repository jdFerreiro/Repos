    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCMWeb.Interfaces;
using Foundation;
using UIKit;
    
namespace BCMWeb.iOS.Services
{
    public class BatteryService : IBatteryService
    {
        public bool DownloadBatteryPermited()
        {
            switch (UIDevice.CurrentDevice.BatteryState)
            {
                case UIDeviceBatteryState.Full:
                    return true;
                case UIDeviceBatteryState.Charging:
                case UIDeviceBatteryState.Unplugged:
                    if (UIDevice.CurrentDevice.BatteryLevel >= 60)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}