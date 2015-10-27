using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using UniversalHelpers.Behaviors;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Linq;
using System.Collections.Specialized;
using Windows.Foundation.Collections;
using System.Diagnostics;
using UniversalHelpers.AwaitableUI;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace UniversalHelpers.Controls
{
    public class ScatterView : ItemsControl
    {

        public ScatterView()
            : base()
        {
            this.ItemsPanel = GetItemsPanelTemplate();
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
            await this.WaitForLayoutUpdateAsync();

            double rotation = 60 / this.Items.Count;

            for (int i = 0; i < this.Items.Count; i++)
            {
                DependencyObject dob = this.ContainerFromItem(this.Items[i]);
                FrameworkElement fe = null;
                if (VisualTreeHelper.GetChildrenCount(dob) == 0)
                    fe = dob as FrameworkElement;
                else
                    fe = VisualTreeHelper.GetChild(dob, 0) as FrameworkElement;
                DragElementBehavior behavior = new DragElementBehavior();
                behavior.CanRotate = CanRotate;
                behavior.CanScale = CanScale;
                behavior.HasInertiaOnTranslate = HasInertia;
                behavior.Container = this;
                behavior.ElementManipulationStarted += behavior_ElementManipulationStarted;
                fe.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                fe.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                fe.RenderTransform = new CompositeTransform();
                fe.RenderTransformOrigin = new Point(0.5, 0.5);
                (fe.RenderTransform as CompositeTransform).Rotation = -30 + rotation * i;

                Interaction.GetBehaviors(fe).Add(behavior);
            }

        }

        void behavior_ElementManipulationStarted(object sender, Windows.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
            //get max z-index
            int maxzindex = Items.Select(x => Canvas.GetZIndex(this.ContainerFromItem(x) as UIElement)).Max();
            DependencyObject parent = VisualTreeHelper.GetParent(sender as UIElement);
            if (parent is Grid)
                (sender as UIElement).SetValue(Canvas.ZIndexProperty, ++maxzindex);
            else //ContentPresenter
                parent.SetValue(Canvas.ZIndexProperty, ++maxzindex);
        }





        public bool HasInertia
        {
            get { return (bool)GetValue(HasInertiaProperty); }
            set
            {
                SetValue(HasInertiaProperty, value);
                FixBehavior();
            }
        }

        private void FixBehavior()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                FrameworkElement fe = GetFrameworkElementFromItem(i);

                var behavior = Interaction.GetBehaviors(fe).OfType<DragElementBehavior>().First();

                behavior.CanRotate = CanRotate;
                behavior.CanScale = CanScale;
                behavior.HasInertiaOnTranslate = HasInertia;
            }
        }

        // Using a DependencyProperty as the backing store for HasInertia.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasInertiaProperty =
            DependencyProperty.Register("HasInertia", typeof(bool), typeof(ScatterView), new PropertyMetadata(true));





        public bool CanRotate
        {
            get { return (bool)GetValue(CanRotateProperty); }
            set
            {
                SetValue(CanRotateProperty, value);
                FixBehavior();
            }
        }

        // Using a DependencyProperty as the backing store for CanRotate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanRotateProperty =
            DependencyProperty.Register("CanRotate", typeof(bool), typeof(ScatterView), new PropertyMetadata(true));



        public bool CanScale
        {
            get { return (bool)GetValue(CanScaleProperty); }
            set
            {
                SetValue(CanScaleProperty, value);
                FixBehavior();
            }
        }

        // Using a DependencyProperty as the backing store for CanScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanScaleProperty =
            DependencyProperty.Register("CanScale", typeof(bool), typeof(ScatterView), new PropertyMetadata(true));

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

    }
}
