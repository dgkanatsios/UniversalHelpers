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
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExpanderTest : ViewBase
    {
        public ExpanderTest()
        {
            this.InitializeComponent();
           
            this.Loaded += ExpanderTest_Loaded;

        }

        void ExpanderTest_Loaded(object sender, RoutedEventArgs e)
        {
            csloadedExpander.ExpanderContent =
                new Ellipse() { Width = 100, Height = 100, Fill = new SolidColorBrush(Colors.Red) };
        }

    
    }
}
