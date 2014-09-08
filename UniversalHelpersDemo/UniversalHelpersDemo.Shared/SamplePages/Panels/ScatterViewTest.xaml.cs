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
    public sealed partial class ScatterViewTest : ViewBase
    {
        public ScatterViewTest()
        {
            this.InitializeComponent();
            this.Loaded += ScatterViewTest_Loaded;

        }

        void ScatterViewTest_Loaded(object sender, RoutedEventArgs e)
        {
            List<Ellipse> list = new List<Ellipse>();
            Ellipse el1 = new Ellipse()
            {
                Width = 100,
                Height = 100,
                Fill =
                    new SolidColorBrush(Colors.Red)
            };
            Ellipse el2 = new Ellipse()
            {
                Width = 100,
                Height = 100,
                Fill =
                    new SolidColorBrush(Colors.Green)
            };
            Ellipse el3 = new Ellipse()
            {
                Width = 100,
                Height = 100,
                Fill =
                    new SolidColorBrush(Colors.Yellow)
            };
            Ellipse el4 = new Ellipse()
            {
                Width = 100,
                Height = 100,
                Fill =
                    new SolidColorBrush(Colors.Blue)
            };

            list.Add(el1); list.Add(el2); list.Add(el3); list.Add(el4);

            scatterView.ItemsSource = list;
        }

       
    }
}
