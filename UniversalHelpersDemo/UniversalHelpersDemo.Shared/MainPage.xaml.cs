﻿using System;
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
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalHelpersDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSpriteSheet();
        }

        private void InitializeSpriteSheet()
        {
            BitmapImage spriteSheet = new BitmapImage(new Uri(this.BaseUri, "/Images/Sheet1.png"));
            UniversalHelpers.StoryboardHelpers.BeginSpriteSheetStoryboard(rectangle,
                6, 5, spriteSheet, 240, 296, 10);
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

        private void Parallel_Click(object sender, RoutedEventArgs e)
        {
            rectangle.TranslateToAsync(10, 10, 1);
            rectangle.AnimateOpacityToAsync(0.3, 1);
            rectangle.TranslateToAsync(30, -10, 1);
            rectangle.RotateByAsync(45, 1);
            rectangle.SkewByAsync(3, 3, 2);
        }
    }

}
