using CoreApiPOC.Services;
using CoreApiPOC.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoreApiPOC
{
	public partial class App : Application
	{
        public bool UseMockServices { get; set; }

        public App ()
		{
			InitializeComponent();

            //MainPage = new CoreApiPOC.MainPage();
            InitApp();

            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }

        private void InitApp()
        {
            UseMockServices = true;
            ViewModelLocator.RegisterDependencies(UseMockServices);
        }


        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
