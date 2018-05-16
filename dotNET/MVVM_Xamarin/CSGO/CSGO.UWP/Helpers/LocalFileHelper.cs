using CSGO.Helpers;
using CSGO.UWP.Helpers;
using System.IO;
using Xamarin.Forms;
using Windows.Storage;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace CSGO.UWP.Helpers
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}