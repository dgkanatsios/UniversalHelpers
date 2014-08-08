using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UniversalHelpers;
using UniversalHelpersDemo.Shared.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MovementToElementTest : Page
    {
        public MovementToElementTest()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
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

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            //testButton.BeginTranslationToElementAsync(yellowRect, 3);

            testButton.BeginTranslationToElementsAsync(new List<Tuple<FrameworkElement, double>>() { 
            new Tuple<FrameworkElement, double> (yellowRect, 1),
            new Tuple<FrameworkElement, double> (greenRect,1),
            new Tuple<FrameworkElement, double> (redRect, 1),
            new Tuple<FrameworkElement, double> (greenRect, 1),
            new Tuple<FrameworkElement, double> (yellowRect, 1)
            });
        }
    }
}
