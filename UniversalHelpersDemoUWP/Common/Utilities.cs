using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalHelpersDemoUWP.Common
{
    public static class CommonUtilities
    {
        private static bool? _IsHardwareButtonsAPIPresent;
        public static bool IsHardwareButtonsAPIPresent
        {
            get
            {
                if (!_IsHardwareButtonsAPIPresent.HasValue)
                {
                    _IsHardwareButtonsAPIPresent =
       Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
                }
                return _IsHardwareButtonsAPIPresent.Value;
            }

        }
    }
}
