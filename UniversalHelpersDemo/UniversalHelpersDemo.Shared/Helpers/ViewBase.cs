using System;
using System.Collections.Generic;
using System.Text;
using UniversalHelpersDemo.Shared.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UniversalHelpersDemo.Helpers
{
    public class ViewBase : Page
    {
        public ViewBase()
        {
            this.navigationHelper = new NavigationHelper(this);

           this.Loaded+=ViewBase_Loaded;
        }

        private Button backButton;

        void ViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            Grid rootGrid = VisualTreeHelper.GetChild(Window.Current.Content, 0) as Grid;
            var backButton = (Button)VisualTreeHelper.GetChild(rootGrid, 1) as Button;


#if WINDOWS_APP
            if(!(this is MainPage))
            {
                backButton.Click -= backButton_Click; backButton.Click += backButton_Click;
                backButton.Visibility = Visibility.Visible;
            }
            else
            {
                backButton.Visibility = Visibility.Collapsed;
            }
#elif WINDOWS_PHONE_APP
            backButton.Visibility = Visibility.Collapsed;
#endif
            
        }

        private NavigationHelper navigationHelper;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

        }

        void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
    }
}
