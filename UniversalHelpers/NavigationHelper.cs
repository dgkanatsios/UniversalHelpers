using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace StoryboardLibrary
{
    public static class NavigationHelper
    {
        public static bool IsFrameGoingBackwards { get; set; }

        public static void GoHome(this Page page)
        {
            NavigationHelper.IsFrameGoingBackwards = true;
            // Use the navigation frame to return to the topmost page
            if (page.Frame != null)
            {
                while (page.Frame.CanGoBack) page.Frame.GoBack();
            }
            NavigationHelper.IsFrameGoingBackwards = false;

        }
    }
}
