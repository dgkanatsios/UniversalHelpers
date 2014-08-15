using System.Threading.Tasks;
using Windows.UI.Xaml;
namespace UniversalHelpers.AwaitableUI
{
    /// <summary>
    /// Contains extension methods to wait for FrameworkElement events.
    /// </summary>
    public static class FrameworkElementExtensions
    {
  



        /// <summary>
        /// Waits for the next layout update event.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns></returns>
        public static async Task WaitForLayoutUpdateAsync(this FrameworkElement frameworkElement)
        {
            await EventAsync.FromEvent<object>(
                eh => frameworkElement.LayoutUpdated += eh,
                eh => frameworkElement.LayoutUpdated -= eh);
        }

       
    }
}
