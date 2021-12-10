using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotesApplication.Views;

namespace NotesApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TabbedShell());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
