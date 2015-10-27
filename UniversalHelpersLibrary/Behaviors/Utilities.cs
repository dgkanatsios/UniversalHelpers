using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Linq;

namespace UniversalHelpers.Behaviors
{
    internal static class Utilities
    {
        public static CompositeTransform GetCompositeTransform(FrameworkElement element)
        {
            if (element.RenderTransform == null)
            {
                element.RenderTransform = new CompositeTransform();
                return element.RenderTransform as CompositeTransform;
            }
            else if (element.RenderTransform is CompositeTransform)
                return element.RenderTransform as CompositeTransform;
            else if (element.RenderTransform is TransformGroup)
            {
                var cts =
                       (element.RenderTransform as TransformGroup).Children.Where(x => x is CompositeTransform);
                if (cts.Count() == 0)
                    throw new ArgumentException("Element must have a composite transform");
                else
                    return cts.First() as CompositeTransform;
            }

            else
            {
                throw new ArgumentException("Element must have a composite transform");
            }
        }
    }
}
