using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryboardLibrary
{
    public static class AppSettings
    {

        private static object lockerObject = new object();

        static Windows.Storage.ApplicationDataContainer Settings =
            Windows.Storage.ApplicationData.Current.LocalSettings;


        public static void StoreSettingString(string settingName, string value)
        {

            try
            {
                lock (lockerObject)
                {
                    Settings.Values[settingName] = value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static void StoreSettingInt(string settingName, int value)
        {

            try
            {
                lock (lockerObject)
                {
                    Settings.Values[settingName] = value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public static void StoreSettingEnum(string settingName, Enum value)
        {

            try
            {
                lock (lockerObject)
                {
                    Settings.Values[settingName] = value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static void StoreSettingBool(string settingName, bool value)
        {

            try
            {
                lock (lockerObject)
                {
                    Settings.Values[settingName] = value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static bool GetSettingBool(string settingName)
        {
            if (Settings.Values.ContainsKey(settingName))
            {
                return (bool)Settings.Values[settingName];
            }
            return default(bool);

        }


        public static int GetSettingInt(string settingName)
        {
            if (Settings.Values.ContainsKey(settingName))
            {
                return (int)Settings.Values[settingName];
            }
            return default(int);

        }

        public static T GetSettingEnum<T>(string settingName)
        {
            if (Settings.Values.ContainsKey(settingName))
            {
                return (T)Enum.Parse(typeof(T), Settings.Values[settingName] as string, true);
            }
            return default(T);

        }

        public static string GetSettingString(string settingName)
        {
            if (Settings.Values.ContainsKey(settingName))
            {

                return (string)Settings.Values[settingName];
            }
            return default(string);

        }


        public static bool TryGetSetting<TValue>(string settingName, out TValue value)
        {
            if (Settings.Values.ContainsKey(settingName))
            {
                value = (TValue)Settings.Values[settingName];
                return true;
            }
            value = default(TValue);
            return false;
        }
    }
}
