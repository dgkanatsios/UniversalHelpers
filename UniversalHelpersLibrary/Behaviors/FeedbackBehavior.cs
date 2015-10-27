using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace UniversalHelpers.Behaviors
{
    [TypeConstraint(typeof(FrameworkElement))]
    public class FeedbackBehavior : DependencyObject, IBehavior
    {



        public double ScaleWhenMoving
        {
            get { return (double)GetValue(ScaleWhenMovingProperty); }
            set { SetValue(ScaleWhenMovingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleWhenMoving.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleWhenMovingProperty =
            DependencyProperty.Register("ScaleWhenMoving", typeof(double), typeof(FeedbackBehavior), new PropertyMetadata(1.1));



        public double OpacityOnPressed
        {
            get { return (double)GetValue(OpacityOnPressedProperty); }
            set { SetValue(OpacityOnPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpacityOnPressed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpacityOnPressedProperty =
            DependencyProperty.Register("OpacityOnPressed", typeof(double), typeof(FeedbackBehavior), new PropertyMetadata(0.3));



        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            (AssociatedObject as FrameworkElement).PointerMoved += ImageFeedbackBehavior_PointerMoved;
            (AssociatedObject as FrameworkElement).PointerExited += ImageFeedbackBehavior_PointerExited;
            (AssociatedObject as FrameworkElement).PointerPressed += ImageFeedbackBehavior_PointerPressed;
            (AssociatedObject as FrameworkElement).PointerReleased += ImageFeedbackBehavior_PointerReleased;
            ScaleWhenMoving = 1.1;
        }

        void ImageFeedbackBehavior_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(AssociatedObject as FrameworkElement);
            ct.ScaleX = ct.ScaleY = 1.0;
            (AssociatedObject as FrameworkElement).Opacity = 1;
        }

        void ImageFeedbackBehavior_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(AssociatedObject as FrameworkElement);
            ct.ScaleX = ct.ScaleY = 1.0;
            (AssociatedObject as FrameworkElement).Opacity = OpacityOnPressed;
        }

        void ImageFeedbackBehavior_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(AssociatedObject as FrameworkElement);


            ct.ScaleX = ct.ScaleY = 1.0;
            (AssociatedObject as FrameworkElement).Opacity = 1;
        }

        void ImageFeedbackBehavior_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            CompositeTransform ct = Utilities.GetCompositeTransform(AssociatedObject as FrameworkElement);

            ct.ScaleX = ct.ScaleY = ScaleWhenMoving;
            (AssociatedObject as FrameworkElement).Opacity = 1;
        }

        public void Detach()
        {
            (AssociatedObject as FrameworkElement).PointerMoved -= ImageFeedbackBehavior_PointerMoved;
            (AssociatedObject as FrameworkElement).PointerExited -= ImageFeedbackBehavior_PointerExited;
            (AssociatedObject as FrameworkElement).PointerPressed -= ImageFeedbackBehavior_PointerPressed;
            (AssociatedObject as FrameworkElement).PointerReleased -= ImageFeedbackBehavior_PointerReleased;
        }
    }
}
