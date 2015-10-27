using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalHelpers.Behaviors
{
    [TypeConstraint(typeof(TextBox))]
    public class TextRequiredFieldBehavior : TextBoxValidatorBase
    {
        public override bool Validate()
        {
            return !(string.IsNullOrEmpty((AssociatedObject as TextBox).Text));
        }
    }

    [TypeConstraint(typeof(TextBox))]
    public class TextNumericDoubleBehavior : TextBoxValidatorBase
    {
        public override bool Validate()
        {
            double d;
            return double.TryParse((AssociatedObject as TextBox).Text, out d);
        }
    }

    [TypeConstraint(typeof(TextBox))]
    public class TextEmailBehavior : TextBoxValidatorBase
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public override bool Validate()
        {
            return new Regex(validEmailPattern, RegexOptions.IgnoreCase).IsMatch((AssociatedObject as TextBox).Text);
        }
    }

    [TypeConstraint(typeof(TextBox))]
    public class TextNumericIntegerBehavior : TextBoxValidatorBase
    {
        public override bool Validate()
        {
            int d;
            return int.TryParse((AssociatedObject as TextBox).Text, out d);
        }
    }

    [TypeConstraint(typeof(TextBox))]
    public class TextMinimumLengthBehavior : TextBoxValidatorBase
    {

        public int MinimumLength
        {
            get { return (int)GetValue(MinimumLengthProperty); }
            set { SetValue(MinimumLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinimumLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumLengthProperty =
            DependencyProperty.Register("MinimumLength", typeof(int), typeof(TextMinimumLengthBehavior), new PropertyMetadata(0));

        public override bool Validate()
        {
            return (AssociatedObject as TextBox).Text.Trim().Length >= MinimumLength;
        }
    }

    
}
