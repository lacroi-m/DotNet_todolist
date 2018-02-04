using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace Table
{
    public class CategoryManagerDatabase
    {
        private readonly SQLite.SQLiteConnection Database;
        public CategoryManagerDatabase(String dbPath)
        {
            this.Database = new SQLiteConnection(dbPath);
            this.Database.CreateTable<CategoryManager>();
        }

        public List<CategoryManager> GetCategoryAsync()
        {
            return (this.Database.Table<CategoryManager>().ToList());
        }

        public CategoryManager GetSpecific(int Id)
        {
            return (this.Database.Table<CategoryManager>().Where(i => i.Id == Id).FirstOrDefault());
        }

        public bool CheckExist(String ToInsert)
        {
            var Content = this.GetCategoryAsync();
            if (Content.Any(i => i.Title == ToInsert))
                return (true);
            return (false);
        }

        public int SaveAsync(CategoryManager ToSave)
        {
            var Content = this.GetCategoryAsync();
            if (this.CheckExist(ToSave.Title))
                return (this.Database.Update(ToSave));
            return (this.Database.Insert(ToSave));
        }

        public int DeleteAsync(CategoryManager ToDelete)
        {
            return (this.Database.Delete(ToDelete));
        }
    }

    public class CategoryManager
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Color { get; set; }
        public bool Checked { get; set; } = true;
    }

    public class CategoryManagerParam
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public Xamarin.Forms.Color Color { get; set; }
        public bool Checked { get; set; } = true;
    }
}
