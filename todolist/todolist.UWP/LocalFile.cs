using System.IO;
using Todo.UWP;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Todo.UWP
{
    public class FileHelper : DatabaseConf.ILocalFile
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}