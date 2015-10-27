using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace UniversalHelpers.Helpers
{
    public class SampleItemToBind
    {
        public BitmapImage Photo { get; set; }
        public string Text { get; set; }

        public static async Task<IEnumerable<SampleItemToBind>> GetSampleItemsToBindAsync()
        {
            List<SampleItemToBind> ItemsToBind = new List<SampleItemToBind>();
            List<BitmapImage> Photos = await Utilities.GetAllPhotosAsync();
            for (int i = 0; i < 5; i++)
            {
                SampleItemToBind itb = new SampleItemToBind();
                itb.Text = "Hello world " + i.ToString();
                itb.Photo = Photos[i];
                ItemsToBind.Add(itb);
            }
            return ItemsToBind;
        }
    }
}
