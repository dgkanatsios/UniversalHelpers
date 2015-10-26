using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalHelpersDemo.Helpers;
using UniversalHelpersDemo.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpriteSheetTest : ViewBase
    {
        public SpriteSheetTest()
        {
            this.InitializeComponent();
            
            this.Loaded += SpriteSheetTest_Loaded;
        }

        void SpriteSheetTest_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSpriteSheet();
        }

 
        private void InitializeSpriteSheet()
        {
            BitmapImage spriteSheet = new BitmapImage(new Uri(this.BaseUri, "/Images/Sheet1.png"));
            UniversalHelpers.StoryboardHelpers.BeginSpriteSheetStoryboard(rectangle,
                6, 5, spriteSheet, 240, 296, 10);
        }

     
    }
}
