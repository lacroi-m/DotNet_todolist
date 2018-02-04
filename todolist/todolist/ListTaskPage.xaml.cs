using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todolist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTaskPage : ContentPage
    {
        private void rec(List<Table.TaskManager> ContentTask, Table.FilterManager MyFilter, List<Table.TaskManagerParameter> Settings)
        {
            foreach (var current in ContentTask)
            {
                Table.TaskManagerParameter tmp = new Table.TaskManagerParameter();
                tmp.CopyTask(current);
                Table.CategoryManager tmpcategory = App.Database2.GetSpecific(tmp.CategoryId);
                if (((MyFilter.Todo == true && tmp.Todo == true)
                    || (MyFilter.Done == true && tmp.Todo == false))
                    && ((MyFilter.LateDate == true
                    && (DateTime.Compare(current.ExpirationDate, DateTime.Now) < 0))
                    || (MyFilter.UpDate == true
                    && !(DateTime.Compare(current.ExpirationDate, DateTime.Now) < 0))))
                {
                    if (tmpcategory != null)
                        tmp.CategoryColor = ColorManager.GetColorFromString(tmpcategory.Color);
                    else
                        tmp.CategoryColor = ColorManager.GetColorFromString(null);
                    if (DateTime.Compare(current.ExpirationDate, DateTime.Now) < 0)
                        tmp.ExpirationDateColor = Color.Red;
                    else
                        tmp.ExpirationDateColor = Color.White;
                    if (tmp.Todo == true)
                        tmp.TodoInfo = "Todo";
                    else
                        tmp.TodoInfo = "Done";
                    Settings.Add(tmp);
                }
            }
        }

        private void RefreshListDb()
        {
            List<Table.TaskManager> ContentTask = App.Database.GetTaskAsync();
            int NbTask = ContentTask.Count();
            Title = "Number of tasks: " + NbTask.ToString();
            List<Table.TaskManagerParameter> Settings = new List<Table.TaskManagerParameter>();
            Table.FilterManager MyFilter;
            if (App.Filter.CheckFilterExist() == false)
                App.Filter.SaveAsync(new Table.FilterManager());
            MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
            rec(ContentTask, MyFilter, Settings);
            TaskListView.ItemsSource = Settings;
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            TaskListView.IsRefreshing = true;
            RefreshListDb();
            TaskListView.IsRefreshing = false;
        }

        async void EditingTask(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var TaskToEdit = (Table.TaskManagerParameter)e.SelectedItem;
                int Id = TaskToEdit.Id;
                await Navigation.PushAsync(new EditingTaskPage(App.Database.GetSpecific(Id)));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshListDb();
        }

        private void SetPage()
        {
            ToolbarItem toolbarItem = new ToolbarItem {Icon = "add.png"};
            toolbarItem.Clicked += async (sender, e) => {
                await Navigation.PushAsync(new CreateTask() { BindingContext = new Table.TaskManager() });};
            ToolbarItems.Add(toolbarItem);
        }

        public ListTaskPage()
        {
            InitializeComponent();
            this.SetPage();
        }
    }
}