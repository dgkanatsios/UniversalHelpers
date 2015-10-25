using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace UniversalHelpers
{
    public static class StoryboardHelpers
    {


        /// <summary>
        /// Begins a storyboard and waits for it to complete.
        /// Found in http://awaitableui.codeplex.com/
        /// </summary>
        public static async Task BeginAsync(this Storyboard storyboard)
        {
            await EventAsync.FromEvent<object>(
                eh => storyboard.Completed += eh,
                eh => storyboard.Completed -= eh,
                storyboard.Begin);
        }

        /// <summary>
        /// Animates the opacity of an element
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="finalOpacity">Final Opacity</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> AnimateOpacityToAsync(this FrameworkElement element, double finalOpacity,
            double durationInSeconds, Action OnComplete = null)
        {
            ValidateForNull(element);
            ValidateCompositeTransform(element);
            if (element.RenderTransform == null) element.RenderTransform = new CompositeTransform();

            Storyboard sb = new Storyboard();
            DoubleAnimation opacityAnimation = CreateDoubleAnimation(element,
                durationInSeconds, finalOpacity,
                "(FrameworkElement.Opacity)");

            sb.Children.Add(opacityAnimation);
            await sb.BeginAsync();
            return sb;
        }

        /// <summary>
        /// Translates an element to absolute values
        /// </summary>
        /// <param name="element">Element to translate</param>
        /// <param name="toX">Final X translation</param>
        /// <param name="toY">Final Y translation</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> TranslateToAsync(this FrameworkElement element, double toX, double toY,
            double durationInSeconds, Action OnComplete = null)
        {
            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation xAnimation = CreateDoubleAnimation(element,
                durationInSeconds, toX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.TranslateX)");


            DoubleAnimation yAnimation = CreateDoubleAnimation(element,
                durationInSeconds, toY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.TranslateY)");

            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Translates an element by relative values
        /// </summary>
        /// <param name="element">Element to translate</param>
        /// <param name="deltaX">Relative X translation</param>
        /// <param name="deltaY">Relative Y translation</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> TranslateByAsync(this FrameworkElement element, double deltaX, double deltaY,
           double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation xAnimation = CreateDoubleAnimation(element,
                durationInSeconds, deltaX + (element.RenderTransform as CompositeTransform).TranslateX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.TranslateX)");


            DoubleAnimation yAnimation = CreateDoubleAnimation(element,
                durationInSeconds, deltaY + (element.RenderTransform as CompositeTransform).TranslateY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.TranslateY)");

            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Rotates an element to absolute angles
        /// </summary>
        /// <param name="element">Element to rotate</param>
        /// <param name="angles">Final rotation</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> RotateToAsync(this FrameworkElement element, double angles,
          double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation rotationAnimation = CreateDoubleAnimation(element,
                durationInSeconds, angles,
                "(FrameworkElement.RenderTransform).(CompositeTransform.Rotation)");


            sb.Children.Add(rotationAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Rotates an element by relative angles
        /// </summary>
        /// <param name="element">Element to rotate</param>
        /// <param name="deltaAngles">Relative angles rotation</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> RotateByAsync(this FrameworkElement element, double deltaAngles,
         double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation rotationAnimation = CreateDoubleAnimation(element,
                durationInSeconds, deltaAngles + (element.RenderTransform as CompositeTransform).Rotation,
                "(FrameworkElement.RenderTransform).(CompositeTransform.Rotation)");


            sb.Children.Add(rotationAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Scales an element to absolute values
        /// </summary>
        /// <param name="element">Element to scale</param>
        /// <param name="scaleX">Relative scaleX increase</param>
        /// <param name="scaleY">Relative scaleX increase</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> ScaleToAsync(this FrameworkElement element, double scaleX, double scaleY,
         double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation scaleXAnimation = CreateDoubleAnimation(element,
                durationInSeconds, scaleX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.ScaleX)");

            DoubleAnimation scaleYAnimation = CreateDoubleAnimation(element,
                durationInSeconds, scaleY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.ScaleY)");


            sb.Children.Add(scaleXAnimation);
            sb.Children.Add(scaleYAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Scales an element by relative values
        /// </summary>
        /// <param name="element">Element to scale</param>
        /// <param name="scaleDeltaX">Final X scale</param>
        /// <param name="scaleDeltaY">Final Y scale</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> ScaleByAsync(this FrameworkElement element, double scaleDeltaX,
            double scaleDeltaY,
        double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation scaleXAnimation = CreateDoubleAnimation(element,
                durationInSeconds, scaleDeltaX + (element.RenderTransform as CompositeTransform).ScaleX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.ScaleX)");

            DoubleAnimation scaleYAnimation = CreateDoubleAnimation(element,
                durationInSeconds, scaleDeltaY + (element.RenderTransform as CompositeTransform).ScaleY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.ScaleY)");


            sb.Children.Add(scaleXAnimation);
            sb.Children.Add(scaleYAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Skews an element by absolute values
        /// </summary>
        /// <param name="element">Element to skew</param>
        /// <param name="skewX">Final X skew</param>
        /// <param name="skewY">Final Y skew</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> SkewToAsync(this FrameworkElement element, double skewX, double skewY,
        double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation skewXAnimation = CreateDoubleAnimation(element,
                durationInSeconds, skewX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.SkewX)");

            DoubleAnimation skewYAnimation = CreateDoubleAnimation(element,
                durationInSeconds, skewY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.SkewY)");


            sb.Children.Add(skewXAnimation);
            sb.Children.Add(skewYAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Skews an element by relative values
        /// </summary>
        /// <param name="element">Element to skew</param>
        /// <param name="skewDeltaX">Final X skew</param>
        /// <param name="skewDeltaY">Final Y skew</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> SkewByAsync(this FrameworkElement element, double skewDeltaX, double skewDeltaY,
      double durationInSeconds, Action OnComplete = null)
        {

            ValidateForNull(element);
            ValidateCompositeTransform(element);

            Storyboard sb = new Storyboard();

            DoubleAnimation skewXAnimation = CreateDoubleAnimation(element,
                durationInSeconds, skewDeltaX + (element.RenderTransform as CompositeTransform).SkewX,
                "(FrameworkElement.RenderTransform).(CompositeTransform.SkewX)");

            DoubleAnimation skewYAnimation = CreateDoubleAnimation(element,
                durationInSeconds, skewDeltaY + (element.RenderTransform as CompositeTransform).SkewY,
                "(FrameworkElement.RenderTransform).(CompositeTransform.SkewY)");


            sb.Children.Add(skewXAnimation);
            sb.Children.Add(skewYAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Animates a shape to specific color
        /// </summary>
        /// <param name="shape">Shape to have color changed</param>
        /// <param name="color">Final color</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> AnimateSolidColorFillToAsync(this Shape shape, Color color,
            double durationInSeconds, Action OnComplete = null)
        {
            ValidateForNull(shape);
            Storyboard sb = new Storyboard();

            ColorAnimation colorAnimation = CreateColorAnimation(shape,
                durationInSeconds, color,
                "(Shape.Fill).(SolidColorBrush.Color)");


            sb.Children.Add(colorAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Animates a panel's background
        /// </summary>
        /// <param name="panel">The panel to have its color animated</param>
        /// <param name="color">Final color</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public static async Task<Storyboard> AnimateSolidColorBackgroundToAsync(this Panel panel, Color color,
          double durationInSeconds, Action OnComplete = null)
        {
            ValidateForNull(panel);
            Storyboard sb = new Storyboard();

            ColorAnimation colorAnimation = CreateColorAnimation(panel,
                durationInSeconds, color,
                "(Panel.Background).(SolidColorBrush.Color)");


            sb.Children.Add(colorAnimation);
            await sb.BeginAsync();
            if (OnComplete != null) OnComplete();
            return sb;
        }

        /// <summary>
        /// Simulates a thread sleep
        /// </summary>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <returns>Storyboard created</returns>
        public async static Task<Storyboard> BeginSleepStoryboardAsync(double durationInSeconds)
        {
            Storyboard sb = new Storyboard();
            sb.Duration = TimeSpan.FromSeconds(durationInSeconds);
            await sb.BeginAsync();
            return sb;
        }

        /// <summary>
        /// Translates an element to another element's position
        /// </summary>
        /// <param name="element">Element to translate</param>
        /// <param name="target">Target element</param>
        /// <param name="durationInSeconds">Duration in seconds</param>
        /// <param name="OnComplete">[Optional] Action to perform on complete</param>
        /// <returns>Storyboard created</returns>
        public async static Task<Storyboard> BeginTranslationToElementAsync(this FrameworkElement element,
            FrameworkElement target, double durationInSeconds, Action OnComplete = null)
        {
            ValidateForNull(element);
            ValidateCompositeTransform(element);
            Point point = target.TransformToVisual(element).TransformPoint(new Point(0, 0));

            CompositeTransform ct = element.RenderTransform as CompositeTransform;
            return await TranslateToAsync(element, point.X + ct.TranslateX,
                point.Y + ct.TranslateY, durationInSeconds, OnComplete);


        }

        /// <summary>
        /// Translates an element sequentially to other elements' positions
        /// </summary>
        /// <param name="element">Element to translate</param>
        /// <param name="targetsTime">List of elements and specific durations</param>
        /// <returns>Task created</returns>
        public async static Task BeginTranslationToElementsAsync(this FrameworkElement element,
            List<Tuple<FrameworkElement, double>> targetsTime)
        {
            ValidateForNull(element);
            ValidateCompositeTransform(element);
            foreach (var item in targetsTime)
            {
                await BeginTranslationToElementAsync(element, item.Item1, item.Item2, null);
            }
        }

        /// <summary>
        /// Initiates a spritesheet animation
        /// </summary>
        /// <param name="shape">Shape to animate on (will create an ImageBrush)</param>
        /// <param name="spriteSheetColumns">Spritesheet columns</param>
        /// <param name="spriteSheetRows">Spritesheet rows</param>
        /// <param name="image">The spritesheet image</param>
        /// <param name="width">Width of the sprite on the spritesheet</param>
        /// <param name="height">Height of the sprite on the spritesheet</param>
        /// <param name="keyframeTime">Time that each keyframe should have</param>
        /// <returns>Storyboard created</returns>
        public static Storyboard BeginSpriteSheetStoryboard(Shape shape, int spriteSheetColumns, int spriteSheetRows, BitmapImage image,
            double width, double height, int keyframeTime)
        {
            ImageBrush ib = new ImageBrush() { Stretch = Stretch.None, AlignmentX = AlignmentX.Left, AlignmentY = AlignmentY.Top };
            ib.Transform = new CompositeTransform();
            ib.ImageSource = image;

            shape.Fill = ib;

            Storyboard sb = new Storyboard();
            sb.RepeatBehavior = RepeatBehavior.Forever;

            ObjectAnimationUsingKeyFrames frm = new ObjectAnimationUsingKeyFrames();
            ObjectAnimationUsingKeyFrames frm2 = new ObjectAnimationUsingKeyFrames();
            frm.BeginTime = new TimeSpan(0, 0, 0);
            frm2.BeginTime = new TimeSpan(0, 0, 0);


            int time = 0;
            for (int j = 0; j < spriteSheetRows; j++)
            {
                for (int i = 0; i < spriteSheetColumns; i++)
                {
                    DiscreteObjectKeyFrame dokf = new DiscreteObjectKeyFrame();
                    dokf.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
                    dokf.Value = -(i * width);
                    frm.KeyFrames.Add(dokf);


                    DiscreteObjectKeyFrame dokf2 = new DiscreteObjectKeyFrame();
                    dokf2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(time));
                    dokf2.Value = -(j * height);
                    frm2.KeyFrames.Add(dokf2);
                    time += keyframeTime;
                }
            }
            Storyboard.SetTarget(frm, shape.Fill);
            Storyboard.SetTarget(frm2, shape.Fill);
            Storyboard.SetTargetProperty(frm, "(ImageBrush.Transform).(CompositeTransform.TranslateX)");
            Storyboard.SetTargetProperty(frm2, "(ImageBrush.Transform).(CompositeTransform.TranslateY)");
            sb.Children.Add(frm);
            sb.Children.Add(frm2);
            sb.Begin();
            return sb;
        }

        private static DoubleAnimation CreateDoubleAnimation(FrameworkElement element,
            double durationInSeconds, double finalValue,
            string property)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = finalValue;
            doubleAnimation.Duration = TimeSpan.FromSeconds(durationInSeconds);

            Storyboard.SetTarget(doubleAnimation, element);
            Storyboard.SetTargetProperty(doubleAnimation, property);
            return doubleAnimation;
        }


        private static ColorAnimation CreateColorAnimation(FrameworkElement element,
           double durationInSeconds, Color finalColor,
           string property)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = finalColor;
            colorAnimation.Duration = TimeSpan.FromSeconds(durationInSeconds);

            Storyboard.SetTarget(colorAnimation, element);
            Storyboard.SetTargetProperty(colorAnimation, property);
            return colorAnimation;
        }



        private static void ValidateForNull(FrameworkElement element)
        {
            if (element == null)
                throw new ArgumentNullException("FrameworkElement");


        }

        private static void ValidateCompositeTransform(FrameworkElement element)
        {
            if (element.RenderTransform == null || !(element.RenderTransform is CompositeTransform))
                throw new ArgumentException("Element must have CompositeTransform");
        }



    }
}
