using System;
using System.IO;
using Xamarin.Forms;
using todolist.Droid;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace todolist.Droid
{
    public class LocalFileHelper : DatabaseConf.ILocalFile
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}