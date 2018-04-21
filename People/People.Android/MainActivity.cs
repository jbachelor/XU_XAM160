using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using People.Droid.Helpers;
using People.Helpers;
using Prism;
using Prism.Ioc;

namespace People.Droid
{
    [Activity(Label = "People", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            Console.WriteLine($"**** {this.GetType().Name}.{nameof(RegisterTypes)}");
            container.RegisterSingleton<IFileAccessHelper, FileAccessHelper>();
        }
    }
}

