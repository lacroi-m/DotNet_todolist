using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace Table
{
    public class TaskManagerDatabase
    {
        private readonly SQLite.SQLiteConnection Database;
        public TaskManagerDatabase(String dbPath)
        {
            this.Database = new SQLiteConnection(dbPath);
            this.Database.CreateTable<TaskManager>();
        }

        public List<TaskManager> GetTaskAsync()
        {
            return (this.Database.Table<TaskManager>().ToList());
        }

        public TaskManager GetSpecific(int Id)
        {
            return (this.Database.Table<TaskManager>().Where(i => i.Id == Id).FirstOrDefault());
        }

        public bool CheckExist(String ToInsert)
        {
            var Content = this.GetTaskAsync();
            if (Content.Any(i => i.Title == ToInsert))
                return (true);
            return (false);
        }

        public int SaveAsync(TaskManager ToSave)
        {
            var Content = this.GetTaskAsync();
            if (this.CheckExist(ToSave.Title))
                return (this.Database.Update(ToSave));
            return (this.Database.Insert(ToSave));
        }

        public int DeleteAsync(TaskManager ToDelete)
        {
            return (this.Database.Delete(ToDelete));
        }
    }

    public class TaskManager
    {
        /*[PrimaryKey, AutoIncrement, Column("_id")]*/
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public int CategoryId { get; set; }
        public bool Todo { get; set; } = true;
        public DateTime ExpirationDate { get; set; }
    }

    public class TaskManagerParameter
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public int CategoryId { get; set; }
        public bool Todo { get; set; }
        public DateTime ExpirationDate { get; set; }
        public String TodoInfo { get; set; }
        public Xamarin.Forms.Color ExpirationDateColor { get; set; }
        public Xamarin.Forms.Color CategoryColor { get; set; }
        public void CopyTask(TaskManager MyTask)
        {
            this.Id = MyTask.Id;
            this.Title = MyTask.Title;
            this.Content = MyTask.Content;
            this.CategoryId = MyTask.CategoryId;
            this.Todo = MyTask.Todo;
            this.ExpirationDate = MyTask.ExpirationDate;
        }
    }
}
