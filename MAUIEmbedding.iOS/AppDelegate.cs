using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using UIKit;
using Microsoft.Maui.Embedding;
using MAUIEmbedding.Pages;
using Microsoft.Maui.Platform;

namespace MAUIEmbedding.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow? Window
        {
            get;
            set;
        }

        public static MauiContext MauiContext;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // create a UIViewController with a single UILabel
            var vc = new UIViewController();

            vc.View!.AddSubview(new UILabel(Window!.Frame)
            {
                BackgroundColor = UIColor.SystemBackground,
                TextAlignment = UITextAlignment.Center,
                Text = "Hello, iOS!",
                AutoresizingMask = UIViewAutoresizing.All,
            });

            Window.RootViewController = vc;

            // make the window visible
            Window.MakeKeyAndVisible();


            //Setup MauiBits
            var builder = MauiApp.CreateBuilder();

            //Add Maui Controls
            builder.UseMauiEmbedding<Application>();

            //iOS/Mac need to register the Window
            builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(UIKit.UIWindow), Window!));

            var mauiApp = builder.Build();

            //Create and save a Maui Context. This is needed for creating Platform UI
            MauiContext = new MauiContext(mauiApp.Services);

            //Create a Maui Page
            var myMauiPage = new MainPage();

            //Turn the Maui page into an iOS UIView
            var view = myMauiPage.ToPlatform(MauiContext);

            //vc.View!.AddSubview(view);

            return true;
        }
    }
}