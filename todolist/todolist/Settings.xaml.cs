using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todolist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : MasterDetailPage
    {
        private static Table.FilterManager MyFilter;

        public void SwitchDatePassed(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
            MyFilter.LateDate = this.SwitchDatePassedName.IsToggled;
            App.Filter.SaveAsync(MyFilter);
            NavigationPage DetailPage = new NavigationPage(new ListTaskPage()) { BarBackgroundColor = Color.FromHex("#828282") };
            ChangeDetailPage(DetailPage);
        }

        public void ChangeDetailPage(NavigationPage NewPage)
        {
            Detail = NewPage;
        }

        public void Home(Object sender, EventArgs e)
        {
            NavigationPage DetailPage = new NavigationPage(new ListTaskPage()) { BarBackgroundColor = Color.FromHex("#828282") };
            ChangeDetailPage(DetailPage);
        }

        public void SwitchDateToDate(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            
            var MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
            MyFilter.UpDate = this.SwitchDateToDateName.IsToggled;
            App.Filter.SaveAsync(MyFilter);
            Home(sender, e);
        }


        public void SwitchDone(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
            App.Filter.SaveAsync(MyFilter);
            Home(sender, e);
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                NavigationPage DetailPage = new NavigationPage((Page)Activator.CreateInstance(item.TargetType)) { BarBackgroundColor = Color.FromHex("#828282") };
                ChangeDetailPage(DetailPage);
                this.ViewCategorie.SelectedItem = null;
                Home(sender, e);
            }
        }

        private void SetPage()
        {
            if (App.Filter.CheckFilterExist() == false)
                App.Filter.SaveAsync(new Table.FilterManager());
            MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
            this.SwitchDatePassedName.IsToggled = MyFilter.LateDate;
            this.SwitchDateToDateName.IsToggled = MyFilter.UpDate;
    
        }

        public Settings()
        {
            InitializeComponent();
            this.SetPage();
            NavigationPage DetailPage = new NavigationPage(new ListTaskPage()) { BarBackgroundColor = Color.FromHex("#828282") };
            ChangeDetailPage(DetailPage);
            this.ViewCategorie.ItemSelected += OnItemSelected;
            //Delete_Page();
        }
    }
}