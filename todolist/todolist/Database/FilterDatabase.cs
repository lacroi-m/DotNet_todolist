using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
namespace Table
{
    public class FilterDatabase
    {
        private readonly SQLite.SQLiteConnection Database;
        public FilterDatabase(String dbPath)
        {
            this.Database = new SQLiteConnection(dbPath);
            this.Database.CreateTable<FilterManager>();
        }

        public List<FilterManager> GetFilterAsync()
        {
            return (this.Database.Table<FilterManager>().ToList());
        }

        public FilterManager GetSpecific(int Id)
        {
            return (this.Database.Table<FilterManager>().Where(i => i.Id == Id).FirstOrDefault());
        }

        public bool CheckFilterExist()
        {
            var Content = this.GetFilterAsync();
            List<FilterManager> CheckUpdate = this.GetFilterAsync();
            if (CheckUpdate != null && CheckUpdate.Count > 0 && CheckUpdate.ElementAt(0) != null)
                return (true);
            return (false);
        }

        public int SaveAsync(FilterManager ToSave)
        {
            var Content = this.GetFilterAsync();
            List<FilterManager> CheckUpdate = this.GetFilterAsync();
            if (CheckUpdate != null && CheckUpdate.Count > 0 && CheckUpdate.ElementAt(0) != null)
                return (this.Database.Update(ToSave));
            return (this.Database.Insert(ToSave));
        }

        public int DeleteAsync(TaskManager ToDelete)
        {
            return (this.Database.Delete(ToDelete));
        }
    }

    public class FilterManager
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool UpDate { get; set; } = true;
        public bool LateDate { get; set; } = true;
        public bool Todo { get; set; } = true;
        public bool Done { get; set; } = true;
    }
}
