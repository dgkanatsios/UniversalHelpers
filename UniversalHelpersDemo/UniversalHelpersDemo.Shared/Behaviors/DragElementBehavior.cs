using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalHelpersDemo.Behaviors;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace UniversalHelpersDemo.Shared.Behaviors
{
    //http://dotnetbyexample.blogspot.nl/2014/05/writing-behaviors-in-pcl-for-windows.html


    [TypeConstraint(typeof(FrameworkElement))]
    public class DragElementBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }



        [CustomPropertyValueEditor(CustomPropertyValueEditor.ElementBinding)]
        public Panel Container
        {
            get { return (Panel)GetValue(ContainerProperty); }
            set { SetValue(ContainerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Container.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainerProperty =
            DependencyProperty.Register("Container", typeof(Panel), typeof(DragElementBehavior), new PropertyMetadata(null));



        public bool HasInertiaOnTranslate
        {
            get { return (bool)GetValue(HasInertiaOnTranslateProperty); }
            set { SetValue(HasInertiaOnTranslateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasInertiaOnTranslate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasInertiaOnTranslateProperty =
            DependencyProperty.Register("HasInertiaOnTranslate", typeof(bool), typeof(DragElementBehavior), new PropertyMetadata(false));



        public bool CanRotate
        {
            get { return (bool)GetValue(CanRotateProperty); }
            set { SetValue(CanRotateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanRotate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanRotateProperty =
            DependencyProperty.Register("CanRotate", typeof(bool), typeof(DragElementBehavior), new PropertyMetadata(false));



        public bool CanScale
        {
            get { return (bool)GetValue(CanScaleProperty); }
            set { SetValue(CanScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanScaleProperty =
            DependencyProperty.Register("CanScale", typeof(bool), typeof(DragElementBehavior), new PropertyMetadata(false));



        public double MaxScale
        {
            get { return (double)GetValue(MaxScaleProperty); }
            set { SetValue(MaxScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxScaleProperty =
            DependencyProperty.Register("MaxScale", typeof(double), typeof(DragElementBehavior), new PropertyMetadata(2.0));



        public double MinScale
        {
            get { return (double)GetValue(MinScaleProperty); }
            set { SetValue(MinScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinScale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinScaleProperty =
            DependencyProperty.Register("MinScale", typeof(double), typeof(DragElementBehavior), new PropertyMetadata(0.5));

        

        private FrameworkElement element;

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;


            element = AssociatedObject as FrameworkElement;
            if (element == null)
                throw new ArgumentException("AssociatedObject must be FrameworkElement");

            element.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;

            if (HasInertiaOnTranslate) element.ManipulationMode |= ManipulationModes.TranslateInertia;

            if (CanRotate) element.ManipulationMode |= ManipulationModes.Rotate;

            if (CanScale) element.ManipulationMode |= ManipulationModes.Scale;

            element.ManipulationStarted += element_ManipulationStarted;
            element.ManipulationDelta += element_ManipulationDelta;
            element.ManipulationCompleted += element_ManipulationCompleted;
            
          

        }

        void element_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(element);
            if (Container != null)
                ValidatePosition(ct, null);
        }

        void element_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(element);

            ct.TranslateX += e.Delta.Translation.X;
            ct.TranslateY += e.Delta.Translation.Y;

            if (CanScale)
            {
                ct.ScaleX *= e.Delta.Scale;
                ct.ScaleY *= e.Delta.Scale;
                if (ct.ScaleX < MinScale)
                    ct.ScaleX = MinScale;
                if (ct.ScaleY < MinScale)
                    ct.ScaleY = MinScale;
                if (ct.ScaleY > MaxScale)
                    ct.ScaleY = MaxScale;
                if (ct.ScaleX > MaxScale)
                    ct.ScaleX = MaxScale;
            }
            if (CanRotate)
            {
                ct.Rotation += e.Delta.Rotation;
            }

            if (Container != null && e.IsInertial)
            {
                ValidatePosition(ct, e);
            }
        }

        private void ValidatePosition(CompositeTransform ct, ManipulationDeltaRoutedEventArgs deltaEventArgs = null)
        {

            Point point = element.TransformToVisual(Container).TransformPoint(new Point(0, 0));

            if (point.X <= 0)
            {
                if (deltaEventArgs != null) deltaEventArgs.Complete();
                ct.TranslateX = 0;
                Rect rect = element.TransformToVisual(Container).TransformBounds(
                  new Rect(new Point(0, 0), element.RenderSize));
                ct.TranslateX = -rect.Left;
            }
            if (point.X + element.ActualWidth >= Container.ActualWidth)
            {
                if (deltaEventArgs != null) deltaEventArgs.Complete();
                ct.TranslateX = 0;
                Rect rect = element.TransformToVisual(Container).TransformBounds(
                  new Rect(new Point(0, 0), element.RenderSize));
                ct.TranslateX = Container.ActualWidth - rect.Right;
            }
            if (point.Y <= 0)
            {
                if (deltaEventArgs != null) deltaEventArgs.Complete();
                ct.TranslateY = 0;
                Rect rect = element.TransformToVisual(Container).TransformBounds(
                  new Rect(new Point(0, 0), element.RenderSize));
                ct.TranslateY = -rect.Top;
            }
            if (point.Y + element.ActualHeight >= Container.ActualHeight)
            {
                if (deltaEventArgs != null) deltaEventArgs.Complete();
                ct.TranslateY = 0;
                Rect rect = element.TransformToVisual(Container).TransformBounds(
                  new Rect(new Point(0, 0), element.RenderSize));
                ct.TranslateY = Container.ActualHeight - rect.Bottom;
            }
        }

        void element_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        public void Detach()
        {
            element.ManipulationStarted -= element_ManipulationStarted;
            element.ManipulationDelta -= element_ManipulationDelta;
            element.ManipulationCompleted -= element_ManipulationCompleted;
        }

       
    }
}
