using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalHelpersDemo.Behaviors
{
    public abstract class TextBoxValidatorBase : DependencyObject, IBehavior
    {
        public TextBoxValidatorBase()
        {
            ValidateMode = TextBoxValidateMode.Both;
        }

        public TextBoxValidateMode ValidateMode
        { get; set; }

        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(TextBoxValidatorBase), new PropertyMetadata("Error"));




        [CustomPropertyValueEditor(CustomPropertyValueEditor.ElementBinding)]
        public TextBlock ErrorTextBlock
        {
            get { return (TextBlock)GetValue(ErrorTextBlockProperty); }
            set { SetValue(ErrorTextBlockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorTextBlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorTextBlockProperty =
            DependencyProperty.Register("ErrorTextBlock", typeof(TextBlock), typeof(TextBoxValidatorBase), new PropertyMetadata(null));


        /// <summary>
        /// If true, will add error string to existing. Will replace if false.
        /// </summary>
        public bool AddErrorMessage
        {
            get { return (bool)GetValue(AddErrorMessageProperty); }
            set { SetValue(AddErrorMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddErrorMessageProperty =
            DependencyProperty.Register("AddErrorMessage", typeof(bool), typeof(TextBoxValidatorBase), new PropertyMetadata(true));

        

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == null)
                throw new ArgumentNullException("AssociatedObject");

            if (!(associatedObject is TextBox))
                throw new ArgumentException("Assosiated object must be TextBox");

            AssociatedObject = associatedObject as TextBox;

            (AssociatedObject as TextBox).LostFocus += textBox_LostFocus;
            (AssociatedObject as TextBox).TextChanged += textBox_TextChanged;
        }

        void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateMode == TextBoxValidateMode.TextChanged || ValidateMode == TextBoxValidateMode.Both)
                ShowHideError(Validate());
        }

        void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ValidateMode == TextBoxValidateMode.LostFocus || ValidateMode == TextBoxValidateMode.Both)
                ShowHideError(Validate());
        }

        public void ShowHideError(bool isValid)
        {
            if (ErrorTextBlock == null)
                throw new ArgumentException("ErrorTextBlock cannot be null");

            if (!isValid)
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                if (AddErrorMessage)
                    ErrorTextBlock.Text += ErrorMessage;
                else
                    ErrorTextBlock.Text = ErrorMessage;
            }
            else
            {
                ErrorTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        public bool IsValid
        {
            get { return Validate(); }
        }

        public void Detach()
        {
            (AssociatedObject as TextBox).LostFocus -= textBox_LostFocus;
            (AssociatedObject as TextBox).TextChanged -= textBox_TextChanged;
        }

        public abstract bool Validate();
    }

    public enum TextBoxValidateMode
    {
        TextChanged,
        LostFocus,
        Both
    }
}
