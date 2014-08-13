using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Popups;

namespace UniversalHelpers
{
    public static class RateHelpers
    {

        public static async Task CheckAndDisplayRateAppControlAsync()
        {

            //first run of the app
            bool value;
            if (!AppSettings.TryGetSetting<bool>(Constants.FeedbackReminderSetting, out value))
                AppSettings.StoreSettingBool(Constants.FeedbackReminderSetting, true);


            //if the user has opted out, just return
            if (!AppSettings.GetSettingBool(Constants.FeedbackReminderSetting))
                return;

            int timesAppRun;
            //this is true in the first run of the app
            if (!AppSettings.TryGetSetting<int>(Constants.TimesAppRunSetting, out timesAppRun))
                AppSettings.StoreSettingInt(Constants.TimesAppRunSetting, 0);

            //int32 overflow check
            if (timesAppRun == int.MaxValue) timesAppRun = 0;

            AppSettings.StoreSettingInt(Constants.TimesAppRunSetting, ++timesAppRun);

            if (timesAppRun % Constants.ShowRateReminderEveryXTimes == 0)
            {
                await ShowRateDialogAsync();
            }


        }

        private static async Task ShowRateDialogAsync()
        {
            MessageDialog md = new MessageDialog("Rate app content", "Rate app title");

            md.Commands.Add(new UICommand("Rate app", rate_RateNow_Click));
//you can't have more than 2 UICommands on WP
          #if WINDOWS_APP
            md.Commands.Add(new UICommand("Remind me later", rate_RemindLater_Click));
#endif
            md.Commands.Add(new UICommand("No thanks", rate_NoThanks_Click));
            // Set the command that will be invoked by default
            md.DefaultCommandIndex = 0;
            // Set the command to be invoked when escape is pressed
            md.CancelCommandIndex = 1;

            await md.ShowAsync();
        }

        private async static void rate_RateNow_Click(IUICommand command)
        {
            AppSettings.StoreSettingBool(Constants.FeedbackReminderSetting, false);
            string familyName = Package.Current.Id.FamilyName;
#if DEBUG
            await Launcher.LaunchUriAsync(new Uri("http://dev.windows.com"));
#else
            await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", familyName)));
#endif

        }

        private async static void rate_RemindLater_Click(IUICommand command)
        {

        }

        private async static void rate_NoThanks_Click(IUICommand command)
        {
            AppSettings.StoreSettingBool(Constants.FeedbackReminderSetting, false);

        }
    }
}
