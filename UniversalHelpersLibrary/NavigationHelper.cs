using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UniversalHelpers
{
    public static class BackwardsNavigationHelper
    {
        public static bool IsFrameGoingBackwards { get; private set; }

        public static void GoHome(this Page page)
        {
            BackwardsNavigationHelper.IsFrameGoingBackwards = true;
            // Use the navigation frame to return to the topmost page
            if (page.Frame != null)
            {
                while (page.Frame.CanGoBack) page.Frame.GoBack();
            }
            BackwardsNavigationHelper.IsFrameGoingBackwards = false;

        }
    }
}
