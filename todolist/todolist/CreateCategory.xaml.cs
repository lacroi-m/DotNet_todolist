using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class CreateCategory : PopupPage
    {
        async public void SaveCategory(object sender, EventArgs args)
        {
            Table.CategoryManager NewCategory = new Table.CategoryManager();
            if (TitleCategory.Text != null && TitleCategory.Text != "")
            {
                NewCategory.Title = TitleCategory.Text;
                NewCategory.Color = ColorPicker.Items[ColorPicker.SelectedIndex];
                NewCategory.Checked = true;
                App.Database2.SaveAsync(NewCategory);
                await Navigation.PopAllPopupAsync();
            }
            else
                await DisplayAlert("Error", "Can't create a empty category", "ok");
        }

        async private void BtnCloseOnClicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAllPopupAsync();
        }

        public CreateCategory()
        {
            InitializeComponent();
            ColorPicker.Title = "Choose a category color";
            ColorManager.FeedColorPicker(ColorPicker);
        }
    }
}