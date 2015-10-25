using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using System.Reflection;

namespace UniversalHelpersDemo.Behaviors
{
    [TypeConstraint(typeof(FrameworkElement))]
    public class TapStoryboardBehavior : DependencyObject, IBehavior
    {

        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }



        public string MethodAfterStoryboard
        {
            get { return (string)GetValue(MethodAfterStoryboardProperty); }
            set { SetValue(MethodAfterStoryboardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MethodAfterStoryboard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodAfterStoryboardProperty =
            DependencyProperty.Register("MethodAfterStoryboard", typeof(string), typeof(TapStoryboardBehavior), new PropertyMetadata(null));



        public bool MethodInVM
        {
            get { return (bool)GetValue(MethodInVMProperty); }
            set { SetValue(MethodInVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MethodInVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodInVMProperty =
            DependencyProperty.Register("MethodInVM", typeof(bool), typeof(TapStoryboardBehavior), new PropertyMetadata(false));

        

        [CustomPropertyValueEditor(CustomPropertyValueEditor.Storyboard)]
        public Storyboard FeedbackStoryboard
        {
            get { return (Storyboard)GetValue(FeedbackStoryboardProperty); }
            set { SetValue(FeedbackStoryboardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FeedbackStoryboard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FeedbackStoryboardProperty =
            DependencyProperty.Register("FeedbackStoryboard", typeof(Storyboard), typeof(TapStoryboardBehavior), new PropertyMetadata(null));



        public object Tag
        {
            get { return (object)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register("Tag", typeof(object), typeof(TapStoryboardBehavior), new PropertyMetadata(null));



        public int AnimationDuration
        {
            get { return (int)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(int), typeof(TapStoryboardBehavior), new PropertyMetadata(300));



        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            (AssociatedObject as FrameworkElement).Tapped += AssociatedObject_Tap;
        }

        void AssociatedObject_Tap(object sender, TappedRoutedEventArgs e)
        {

            if (MethodAfterStoryboard != null)
            {
                FeedbackStoryboard.Completed -= FeedbackStoryboard_Completed;
                FeedbackStoryboard.Completed += FeedbackStoryboard_Completed;
            }

            FeedbackStoryboard.Begin();
        }

        void FeedbackStoryboard_Completed(object sender, object e)
        {
            FrameworkElement dob = (AssociatedObject as FrameworkElement).Parent as FrameworkElement;
            while (!(dob is Page))
            {
                dob = dob.Parent as FrameworkElement;
            }
            if (!MethodInVM)
            {
                
                if (Tag == null)
                    dob.GetType().GetTypeInfo().GetDeclaredMethod(MethodAfterStoryboard).Invoke(dob, null);
                else
                    dob.GetType().GetTypeInfo().GetDeclaredMethod(MethodAfterStoryboard).Invoke(dob, new object[] { Tag });
            }
            else//method lives in ViewModel
            {
                if (Tag == null)
                    dob.DataContext.GetType().GetTypeInfo().GetDeclaredMethod(MethodAfterStoryboard).Invoke(dob, null);
                else
                    dob.DataContext.GetType().GetTypeInfo().GetDeclaredMethod(MethodAfterStoryboard).Invoke(dob, new object[] { Tag });
            }

        }

        public void Detach()
        {
            if (FeedbackStoryboard != null)
            {
                FeedbackStoryboard.Stop();
                FeedbackStoryboard = null;
            }
            (AssociatedObject as FrameworkElement).Tapped -= AssociatedObject_Tap;
        }



    }

}
