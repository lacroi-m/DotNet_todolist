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
    public partial class CreateTask : ContentPage
    {
        private static List<Table.CategoryManager> AllCategory;

        async public void SaveTask(object sender, EventArgs args)
        {
            Table.TaskManager NewTask = new Table.TaskManager();
            NewTask.Title = TitleTask.Text;
            NewTask.Content = ContentTask.Text;
            NewTask.CategoryId = -1;
            NewTask.Todo = true;
            DateTime NewDate = TaskDate.Date;
            TimeSpan NewTime = TaskTime.Time;
            NewTask.ExpirationDate = new System.DateTime(NewDate.Year, NewDate.Month, NewDate.Day, NewTime.Hours, NewTime.Minutes, NewTime.Seconds, NewTime.Milliseconds);
            App.Database.SaveAsync(NewTask);
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.SetPage();
            TaskDate.MinimumDate = System.DateTime.Now;
            TaskDate.MaximumDate = System.DateTime.Now.AddYears(10);
            TaskDate.Format = "yyyy-MM-dd";
            TaskTime.Time = System.DateTime.Now.TimeOfDay;
            if (TaskTime.Time.Hours > 23)
                TaskDate.Date = System.DateTime.Now.AddDays(1);
            else
                TaskDate.Date = System.DateTime.Now;
            TaskTime.Format = "HH:mm";
        }

        private void SetPage()
        {
        }

        public CreateTask()
        {
            InitializeComponent();
            this.SetPage();
        }
    }
}