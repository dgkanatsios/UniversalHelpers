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
using Windows.UI;
using Windows.UI.Popups;
using UniversalHelpersDemo.Common;
using UniversalHelpersDemo.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StoryboardExtensionsTest : ViewBase
    {
        public StoryboardExtensionsTest()
        {
            this.InitializeComponent();
        
        }

        private async void Sequential_Click(object sender, RoutedEventArgs e)
        {
            await rectangle.TranslateToAsync(10, 10, 1);
            await rectangle.AnimateOpacityToAsync(0.3, 1);
            await rectangle.TranslateByAsync(30, -10, 1);
            await rectangle.RotateByAsync(45, 1);
            await rectangle.SkewByAsync(3, 3, 2);
            await rectangle.AnimateSolidColorFillToAsync(Colors.Red, 2, new Action(async () =>
            {
                await (new MessageDialog("Hi!")).ShowAsync();
            }));

        }

        private async void Parallel_Click(object sender, RoutedEventArgs e)
        {
            await rectangle.TranslateToAsync(10, 10, 1);
            await rectangle.AnimateOpacityToAsync(0.3, 1);
            await rectangle.TranslateToAsync(30, -10, 1);
            await rectangle.RotateByAsync(45, 1);
            await rectangle.SkewByAsync(3, 3, 2);
        }

    }
}
