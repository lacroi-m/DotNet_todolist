using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

using System.Threading.Tasks;
namespace todolist
{
    public partial class App : Application
    {
        static Table.TaskManagerDatabase MyDb;
        static Table.CategoryManagerDatabase MyDb2;
        static Table.FilterDatabase FilterDb;

        public static Table.TaskManagerDatabase Database
        {
            get
            {
                String Name = "Todo.db3";

                if (MyDb == null)
                    MyDb = new Table.TaskManagerDatabase(DependencyService.Get<DatabaseConf.ILocalFile>().GetLocalFilePath(Name));
                return (MyDb);
            }
        }

        public static Table.CategoryManagerDatabase Database2
        {
            get
            {
                String Name = "Todo.db3";

                if (MyDb2 == null)
                    MyDb2 = new Table.CategoryManagerDatabase(DependencyService.Get<DatabaseConf.ILocalFile>().GetLocalFilePath(Name));
                return (MyDb2);
            }
        }

        public static Table.FilterDatabase Filter
        {
            get
            {
                String Name = "Todo.db3";

                if (FilterDb == null)
                    FilterDb = new Table.FilterDatabase(DependencyService.Get<DatabaseConf.ILocalFile>().GetLocalFilePath(Name));
                return (FilterDb);
            }
        }
        public void test()
        {
            while (true)
            {
               // App.Current.MainPage = new Settings();

                Task.Delay(6000);
            }
        }
        public App()
        {
            InitializeComponent();
            var t = Task.Factory.StartNew(() => test());
            App.Current.MainPage = new Settings();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
