using System;
using System.Collections.Generic;
using System.Text;
using UniversalHelpersDemoUWP.Common;
using UniversalHelpersDemoUWP;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.ApplicationModel;

namespace UniversalHelpersDemoUWP.Helpers
{
    public class ViewBase : Page
    {
        public ViewBase()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                this.navigationHelper = new NavigationHelper(this);
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                SystemNavigationManager.GetForCurrentView().BackRequested += ViewBase_BackRequested;
                this.Loaded += ViewBase_Loaded;
            }
        }

        private void ViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (this is MainPage)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void ViewBase_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private NavigationHelper navigationHelper;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

        }

 
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
    }
}
