using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todolist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCategoryPage : ContentPage
    {
        private void RefreshListDb()
        {
            List<Table.CategoryManager> ContentCategory = App.Database2.GetCategoryAsync();
            List<Table.CategoryManagerParam> ContentCategory2 = new List<Table.CategoryManagerParam>();

            int NbCategory = ContentCategory.Count();
            this.Title = NbCategory.ToString() + " category";

            foreach (var current in ContentCategory.ToList())
            {
                bool Passed = false;
                if (current.Title == null)
                {
                    ContentCategory.Remove(current);
                    Passed = true;
                }
                if (current.Color == null)
                    current.Color = "LightGrey";
                if (Passed == false)
                {
                    Table.CategoryManagerParam CurrentCategory = new Table.CategoryManagerParam();
                    CurrentCategory.Checked = current.Checked;
                    CurrentCategory.Color = ColorManager.GetColorFromString(current.Color);
                    CurrentCategory.Id = current.Id;
                    CurrentCategory.Title = current.Title;
                    ContentCategory2.Add(CurrentCategory);
                }
            }
            this.Title = ContentCategory2.Count().ToString() + " category";
            if (ContentCategory2 != null)
                CategoryListView.ItemsSource = ContentCategory2;
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            CategoryListView.IsRefreshing = true;
            this.RefreshListDb();
            CategoryListView.IsRefreshing = false;
        }

        private void SetPage()
        {
            ToolbarItem toolbarItem = new ToolbarItem { Icon = "add.png" };
            toolbarItem.Clicked += async (sender, e) =>
            {
                App.Current.MainPage = new CreateCategory();
                await Navigation.PushPopupAsync(new CreateCategory() { IsAnimating = false });
                await PopupNavigation.PushAsync(new CreateCategory());
                await Navigation.PushAsync(new CreateCategory() { BindingContext = new Table.CategoryManager() });
            };
            ToolbarItems.Add(toolbarItem);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.RefreshListDb();
        }

        public ListCategoryPage()
        {
            InitializeComponent();
            this.SetPage();
        }
    }
}