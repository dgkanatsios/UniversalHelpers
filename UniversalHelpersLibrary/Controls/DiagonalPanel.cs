using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalHelpers.AwaitableUI;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace UniversalHelpers.Controls
{
    public sealed class DiagonalPanel : ItemsControl
    {
        public DiagonalPanel()
            : base()
        {
            this.ItemsPanel = GetItemsPanelTemplate();
            this.LayoutUpdated += DiagonalPanel_LayoutUpdated;
        }

        async void DiagonalPanel_LayoutUpdated(object sender, object e)
        {
            await CalculatePositionsAsync();
        }

        private ItemsPanelTemplate GetItemsPanelTemplate()
        {
            string xaml = @"<ItemsPanelTemplate   xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                            <Grid>
                               
                            </Grid>
                    </ItemsPanelTemplate>";
            return XamlReader.Load(xaml) as ItemsPanelTemplate;
        }

        protected async override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);

            await CalculatePositionsAsync();

        }

        private async Task CalculatePositionsAsync()
        {
            await this.WaitForLayoutUpdateAsync();
            if (Items.Count == 0) return;

            double width = this.Width;
            if (double.IsNaN(width))
                width = this.ActualWidth;

            double height = this.Height;
            if (double.IsNaN(height))
                height = ActualHeight;

            double itemWidth = GetFrameworkElementFromItem(0).ActualWidth;
            double itemHeight = GetFrameworkElementFromItem(0).ActualHeight;

            double distanceX = width / Items.Count;
            double distanceY = height / Items.Count;
            double startX = 0.0, startY = 0.0;


            switch (Orientation)
            {
                case DiagonalPanelOrientation.TopLeftToBottomRight:
                    startX = 0.0;
                    startY = 0.0;
                    distanceX *= 1;
                    distanceY *= 1;
                    break;
                case DiagonalPanelOrientation.TopRightToBottomLeft:
                    startX = width - itemWidth ;
                    startY = 0.0;
                    distanceX *= -1;
                    distanceY *= 1;
                    break;
                case DiagonalPanelOrientation.BottomLeftToTopRight:
                    startX = 0.0;
                    startY = height - itemHeight ;
                    distanceX *= 1;
                    distanceY *= -1;
                    break;
                case DiagonalPanelOrientation.BottomRightToTopLeft:
                    startX = width - itemWidth ;
                    startY = height - itemHeight ;
                    distanceX *= -1;
                    distanceY *= -1;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                FrameworkElement fe = GetFrameworkElementFromItem(i);
                fe.RenderTransformOrigin = new Point(0.5, 0.5);
                fe.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                fe.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                fe.RenderTransform = new CompositeTransform();
                

                CompositeTransform ct = fe.RenderTransform as CompositeTransform;
                ct.TranslateX = i * distanceX + startX;
                ct.TranslateY = i * distanceY + startY;
            }

        }

        private FrameworkElement GetFrameworkElementFromItem(int i)
        {
            DependencyObject dob = this.ContainerFromItem(this.Items[i]);
            FrameworkElement fe = null;
            if (VisualTreeHelper.GetChildrenCount(dob) == 0)
                fe = dob as FrameworkElement;
            else
                fe = VisualTreeHelper.GetChild(dob, 0) as FrameworkElement;
            return fe;
        }



        public DiagonalPanelOrientation Orientation
        {
            get { return (DiagonalPanelOrientation)GetValue(OrientationProperty); }
            set
            {
                SetValue(OrientationProperty, value);
                CalculatePositionsAsync();
            }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(DiagonalPanelOrientation), typeof(DiagonalPanel), new PropertyMetadata(DiagonalPanelOrientation.TopLeftToBottomRight));


    }

    public enum DiagonalPanelOrientation
    {
        TopLeftToBottomRight,
        TopRightToBottomLeft,
        BottomLeftToTopRight,
        BottomRightToTopLeft
    }
}
