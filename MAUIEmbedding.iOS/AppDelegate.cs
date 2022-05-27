using Foundation;
using MAUIEmbedding.Pages;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using UIKit;

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

            // We can also turn it into a UIViewController
            var mauiCtrl = myMauiPage.ToUIViewController(MauiContext);

            // Using the old CreateViewController() method doesn't seem to work.
            // The app throws an error saying that you need to call Forms.Init()...
            var mauiCompatCtrl = myMauiPage.CreateViewController();

            Window.RootViewController = mauiCtrl;

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}