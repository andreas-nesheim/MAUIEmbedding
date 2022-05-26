using Android.App;
using Android.OS;
using Microsoft.Maui;
using Microsoft.Maui.Embedding;

namespace MAUIEmbedding.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
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

            ////Create a Maui Page
            //var myMauiPage = new MyMauiPage();

            ////Turn the Maui page into an Android View
            //var view = myMauiPage.ToPlatform(mauiContext);

            ////Use the Android View
            //SetContentView(view);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
    }
}