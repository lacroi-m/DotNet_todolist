using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todolist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTaskPage : ContentPage
    {
        private void RefreshListDb()
        {
            List<Table.TaskManager> ContentTask = App.Database.GetTaskAsync();

            int NbTask = ContentTask.Count();
            if (NbTask > 1)
                this.Title = NbTask.ToString() + " tasks";
            else
                this.Title = NbTask.ToString() + " task";

            List<Table.TaskManagerParameter> Settings = new List<Table.TaskManagerParameter>();
            Table.FilterManager MyFilter;
            if (App.Filter.CheckFilterExist() == false)
                App.Filter.SaveAsync(new Table.FilterManager());
            MyFilter = App.Filter.GetFilterAsync().ElementAt(0);
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
                    && !(DateTime.Compare(current.ExpirationDate, DateTime.Now) < 0)))
                    /*&& (tmpcategory.Checked == true)*/)
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
            TaskListView.ItemsSource = Settings;
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            TaskListView.IsRefreshing = true;
            this.RefreshListDb();
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
            this.RefreshListDb();
        }

        private void SetPage()
        {
            ToolbarItem toolbarItem = new ToolbarItem
            {
                Text = "+"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new CreateTask() { BindingContext = new Table.TaskManager() });
            };
            ToolbarItems.Add(toolbarItem);
        }

        public ListTaskPage()
        {
            InitializeComponent();
            this.SetPage();
        }
    }
}