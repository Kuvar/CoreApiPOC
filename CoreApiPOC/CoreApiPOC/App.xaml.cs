namespace CoreApiPOC
{
    using Services;
    using ViewModels.Base;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public bool UseMockServices { get; set; }
        public static new int Id { get; set; }
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static bool IsAdmin { get; set; }
        public static string Token { get; set; }

        public App()
        {
            InitializeComponent();
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            IsAdmin = false;
            Token = string.Empty;

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

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
