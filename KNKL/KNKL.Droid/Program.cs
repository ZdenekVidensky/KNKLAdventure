using System;
using System.Diagnostics;
using System.IO;
using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;
using Microsoft.Xna.Framework;

using CocosSharp;
using KNKL;
using KNKL.Core.SaveLoad;
using Ninject;

namespace KNKL.Droid
{

    [Activity(Label = "Klášter na kraji lesa"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class Program : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CCApplication application = new CCApplication();
			application.ApplicationDelegate = new AppDelegate();

            //Nabinduju do kontejneru SaveLoadGame tridu
            AppDelegate.Container = new StandardKernel();
            AppDelegate.Container.Bind<ISaveLoadXml>().To<SaveLoadXml>();

			this.SetContentView(application.AndroidContentView);

			application.StartGame();
        }        
    }
}

