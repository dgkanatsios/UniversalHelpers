using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalHelpersDemo.Behaviors
{
    [TypeConstraint(typeof(TextBox))]
    public class SelectAllTextOnFocusBehavior : DependencyObject, IBehavior
    {

        private DependencyObject associatedObject;

        public DependencyObject AssociatedObject
        {
            get { return associatedObject; }
        }

        public void Attach(DependencyObject associatedObject)
        {
            this.associatedObject = associatedObject;
            (this.associatedObject as TextBox).GotFocus += TextEmailBehavior_GotFocus;
        }

        void TextEmailBehavior_GotFocus(object sender, RoutedEventArgs e)
        {
            (this.associatedObject as TextBox).SelectAll();
        }

        public void Detach()
        {
            (this.associatedObject as TextBox).GotFocus -= TextEmailBehavior_GotFocus;
        }
    }
}
