using CoreApiPOC.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoreApiPOC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            NavigationPage.SetHasBackButton(this, false);

            MessagingCenter.Subscribe<Page>(this, "MessagingCenterNavigateToPage", (page) =>
            {
                NavigateToPage(page);
            });
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        public void NavigateToPage(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        public void NavigateToPage(Page page, string title)
        {
            page.Title = title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}