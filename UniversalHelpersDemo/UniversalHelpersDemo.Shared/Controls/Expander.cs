using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace UniversalHelpersDemo.Controls
{
    [TemplatePart(Name = HeaderTextName, Type = typeof(TextBlock))]
    [TemplatePart(Name = OpenButtonName, Type = typeof(Button))]
    [TemplatePart(Name = ExpanderContentName, Type = typeof(ContentPresenter))]
    [TemplateVisualState(Name = "Expanded", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "NotExpanded", GroupName = "CommonStates")]
    public sealed class Expander : Control
    {

        private const string HeaderTextName = "PART_HeaderText";
        private const string OpenButtonName = "PART_OpenButton";
        private const string ExpanderContentName = "PART_ExpanderContent";
        private Button openButton;
        private ContentPresenter expanderContentName;

        public Expander()
        {
            this.DefaultStyleKey = typeof(Expander);
        }

        protected override void OnApplyTemplate()
        {
            openButton = GetTemplateChild(OpenButtonName) as Button;
            if (openButton != null)
            {
                openButton.Click += openButton_Click;
            }
            expanderContentName = GetTemplateChild(ExpanderContentName) as ContentPresenter;
            base.OnApplyTemplate();
        }

        void openButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeExpanded(true);
        }

        private void ChangeExpanded(bool changeDP)
        {
            if (IsEnabled)
            {
                if (IsExpanded)
                {
                    VisualStateManager.GoToState(this, "NotExpanded", true);
                    if (changeDP) SetValue(IsExpandedProperty, false);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Expanded", true);
                    if (changeDP) SetValue(IsExpandedProperty, true);
                }
            }
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set
            {
                SetValue(IsExpandedProperty, value);
                ChangeExpanded(false);
                if (ExpandedChanged != null)
                    ExpandedChanged(this, EventArgs.Empty);
            }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(Expander), new PropertyMetadata(true));



        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set
            {
                SetValue(HeaderProperty, value);
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

        public EventHandler ExpandedChanged;

    }
}
