using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace UniversalHelpers.Helpers
{
    public static class Utilities
    {

        static Utilities()
        {

        }

        public static int GetNextRandom(int max)
        {
            return RandomGenerator.Next(0, max);
        }

        private static Random RandomGenerator = new Random();
       

        public static async Task<List<BitmapImage>> GetAllPhotosAsync()
        {
            List<BitmapImage> Photos = new List<BitmapImage>();
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var imagesFolder = await installedLocation.GetFolderAsync(@"Images\Photos");
            var files = await imagesFolder.GetFilesAsync();
            foreach (var item in files)
            {
                Photos.Add(await GetImageAsync(item));
            }
            return Photos;
        }


        public async static Task<BitmapImage> GetImageAsync(StorageFile storageFile)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await storageFile.OpenAsync(FileAccessMode.Read);
            bitmapImage.SetSource(stream);
            return bitmapImage;
        }
    }
}
