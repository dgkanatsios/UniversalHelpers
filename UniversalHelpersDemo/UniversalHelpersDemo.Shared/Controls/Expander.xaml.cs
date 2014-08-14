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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace UniversalHelpersDemo.Controls
{
    public sealed partial class Expander : UserControl
    {
        public Expander()
        {
            this.InitializeComponent();
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set
            {
                SetValue(HeaderProperty, value);
                HeaderText.Text = value;
            }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(Expander), new PropertyMetadata("Header"));


        public object ExpanderContent
        {
            get { return (object)GetValue(ExpanderContentProperty); }
            set { SetValue(ExpanderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpanderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderContentProperty =
            DependencyProperty.Register("ExpanderContent", typeof(object), typeof(Expander), new PropertyMetadata(null));

        

        

        private void HideShowContent(object sender, RoutedEventArgs e)
        {
            if (contentPresenter.Visibility == Visibility.Visible)
                contentPresenter.Visibility = Visibility.Collapsed;
            else
                contentPresenter.Visibility = Visibility.Visible;
        }

       

    }
}
