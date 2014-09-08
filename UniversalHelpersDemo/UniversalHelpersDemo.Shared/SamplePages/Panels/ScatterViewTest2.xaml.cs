using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalHelpersDemo.Helpers;
using UniversalHelpersDemo.Shared.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScatterViewTest2 : ViewBase

    {
        public ScatterViewTest2()
        {
            this.InitializeComponent();
           
            this.Loaded += ScatterViewTest2_Loaded;
        }

        async void ScatterViewTest2_Loaded(object sender, RoutedEventArgs e)
        {

            scatterView.ItemsSource = await SampleItemToBind.GetSampleItemsToBindAsync();
        }

      

     

      
    }
}
