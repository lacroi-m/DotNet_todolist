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
    public partial class EditingTaskPage : ContentPage
    {
        private static Table.TaskManager CurrentTask;
        private static Table.TaskManager ModifiedTask;


        async public void SaveTask(Object sender, EventArgs e)
        {
            ModifiedTask.Title = TitleTask.Text;
            ModifiedTask.Content = ContentTask.Text;
            App.Database.SaveAsync(ModifiedTask);
            await Navigation.PopAsync();
        }

        async public void RemoveTask(Object sender, EventArgs e)
        {
            App.Database.DeleteAsync(CurrentTask);
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TitleTask.Text = CurrentTask.Title;
            ContentTask.Text = CurrentTask.Content;
        }

        public EditingTaskPage(Table.TaskManager MyCurrentTask)
        {
            InitializeComponent();
            CurrentTask = MyCurrentTask;
            ModifiedTask = MyCurrentTask;
        }
    }
}