using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalHelpersDemoUWP.Helpers;
using UniversalHelpersDemoUWP.Common;
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

namespace UniversalHelpersDemoUWP
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
            Random r = new Random();
            List<Rectangle> list = new List<Rectangle>();
            Rectangle el1 = new Rectangle()
            {
                Width = r.Next(100,300),
                Height = r.Next(100,300),
                Fill =
                    new SolidColorBrush(Colors.Red)
            };
            Rectangle el2 = new Rectangle()
            {
                Width = r.Next(100,300),
                Height = r.Next(100,300),
                Fill =
                    new SolidColorBrush(Colors.Green)
            };
            Rectangle el3 = new Rectangle()
            {
                Width = r.Next(100,300),
                Height = r.Next(100,300),
                Fill =
                    new SolidColorBrush(Colors.Yellow)
            };
            Rectangle el4 = new Rectangle()
            {
                Width = r.Next(100,300),
                Height = r.Next(100,300),
                Fill =
                    new SolidColorBrush(Colors.Blue)
            };

            list.Add(el1); list.Add(el2); list.Add(el3); list.Add(el4);

            scatterView.ItemsSource = list;
        }

       
    }
}
