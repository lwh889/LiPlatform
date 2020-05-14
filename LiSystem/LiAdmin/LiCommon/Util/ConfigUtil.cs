using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LiCommon.Util
{
    /// <summary>
    /// 读取配置类
    /// </summary>
    public class ConfigUtil
    {
       /// <summary>
       /// 获取路径
       /// </summary>
        public static string defaultFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;


        /// <summary>
        /// 获取Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetKey(String key)
        {
            return GetKey(defaultFileName, key);
        }

        /// <summary>
        /// 获取Key值
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetKey(String configPath, String key)
        {
            Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
            {
                ExeConfigFilename = configPath
            }, ConfigurationUserLevel.None);


            if (ConfigurationInstance.AppSettings.Settings[key] != null)
                return ConfigurationInstance.AppSettings.Settings[key].Value;
            else

                return string.Empty;
        }

        /// <summary>
        /// 设置Key值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetKey( String key, String value)
        {
            return SetKey(defaultFileName, key, value);
        }

        /// <summary>
        /// 设置Key值
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetKey(String configPath, String key, String value)
        {
            try
            {
                Configuration ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = configPath
                }, ConfigurationUserLevel.None);

                if (ConfigurationInstance.AppSettings.Settings[key] != null)
                    ConfigurationInstance.AppSettings.Settings[key].Value = value;
                else
                    ConfigurationInstance.AppSettings.Settings.Add(key, value);
                ConfigurationInstance.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
