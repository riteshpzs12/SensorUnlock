﻿using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using static Android.Provider.Settings;

namespace SensorData.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            if (!string.IsNullOrEmpty(App.DeviceId))
            {
                string id = Android.OS.Build.GetSerial();
                if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
                {
                    try
                    {
                        var context = Android.App.Application.Context;
                        id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                    }
                    catch (Exception ex)
                    {
                        id = "UNKNOWN";
                    }
                }
                App.DeviceId = id;
            } 
        }
    }
}
