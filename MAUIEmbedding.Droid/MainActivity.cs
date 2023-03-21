using Android.App;
using Android.OS;
using MAUIEmbedding.Pages;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;

namespace MAUIEmbedding.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.MaterialComponents.Light.DarkActionBar")]
    public class MainActivity : Activity
    {
        MauiContext mauiContext;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Setup MauiBits
            var builder = MauiApp.CreateBuilder();

            //Add Maui Controls
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();

            var mauiApp = builder.Build();

            //Create and save a Maui Context. This is needed for creating Platform UI
            mauiContext = new MauiContext(mauiApp.Services, this);

            //Create a Maui Page
            var myMauiPage = new MainPage();

            //Turn the Maui page into an Android View
            var view = myMauiPage.ToPlatform(mauiContext);

            // Explicitly turn it into a container view
            var containerView = myMauiPage.ToContainerView(mauiContext);

            //Use the Android View.
            // This will fail for some reason. Need to use the containerView.
            //SetContentView(view);

            SetContentView(containerView);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);
        }
    }
}