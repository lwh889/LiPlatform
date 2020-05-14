using System;
using System.Windows.Forms;
using System.IO;
using log4net;

namespace LiLog
{
    /// <summary>
    /// 日志的记录方式，请修改appender-ref节点，对应ID为appender的name值
    /// LoggerName和配置文件【Log4Net.config】节点logger name="LogDefaultName"名称一致
    /// 如果要连接数据库，请更改配置文件【Log4Net.config】的数据库连接串，新增数据库，名称为LiLog，执行创建数据库表LogInfo
    /// </summary>
    public sealed class LogUtil : Attribute
    {
        /// <summary>
        /// 根据需要来命名不一样名称
        /// </summary>
        public static string LoggerName = "LogDefaultName";
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserID = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName = string.Empty;

        /// <summary>
        /// 主机IP
        /// </summary>
        public static string HostIP = string.Empty;
        /// <summary>
        /// 主机名称
        /// </summary>
        public static string HostName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        private static ILog iLog;

        /// <summary>
        /// 日志实体
        /// </summary>
        private static LogEntity logEntity;

        private static string _connectionStr;
        public static string ConnectionStr
        {
            get
            {
                return _connectionStr;
            }

            set
            {
                _connectionStr = value;
            }
        }
        /// <summary>
        /// 接口
        /// </summary>
        private static ILog log
        {
            get
            {
                string path = Application.StartupPath + @"\Log4Net.config";
                log4net.Config.XmlConfigurator.Configure(new FileInfo(path));

                if (iLog == null)
                {
                    iLog = log4net.LogManager.GetLogger(LoggerName);
                }
                else
                {
                    if (iLog.Logger.Name != LoggerName)
                    {
                        iLog = log4net.LogManager.GetLogger(LoggerName);
                    }
                }

                return iLog;
            }
        }

        public LogUtil(LogType logType, string message)
        {
            this.WriteLog(logType, UserID, UserName, message, null);
        }

        public LogUtil(LogType logType, string userID, string userName, string message)
        {
            this.WriteLog(logType, userID, userName, message, null);
        }


        public LogUtil(LogType logType, string userID, string userName, string message, Exception ex)
        {
            this.WriteLog(logType, userID, userName, message, ex);
        }

        private void WriteLog(LogType logType, string userID, string userName, string message, Exception ex)
        {
            UserID = userID;
            UserName = userName;

            switch (logType)
            {
                case LogType.Debug:
                    if (log.IsDebugEnabled)
                        log.Debug(BuildMessageMode(message), ex);
                    break;
                case LogType.Error:
                    if (log.IsErrorEnabled)
                        log.Error(BuildMessageMode(message), ex);
                    break;
                case LogType.Fatal:
                    if (log.IsFatalEnabled)
                        log.Fatal(BuildMessageMode(message), ex);
                    break;
                case LogType.Info:
                    if (log.IsInfoEnabled)
                        log.Info(BuildMessageMode(message), ex);
                    break;
                case LogType.Warn:
                    if (log.IsWarnEnabled)
                        log.Warn(BuildMessageMode(message), ex);
                    break;
            }
        }
        /// <summary>
        /// 修改数据库连接
        /// </summary>
        /// <param name="connectionStr"></param>
        public static void ModifyConnecctionStrInConfigureLog4Net(string connectionStr)
        {
            if (string.IsNullOrWhiteSpace(_connectionStr)) return;

            log4net.Repository.Hierarchy.Hierarchy hierarchy = log4net.LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
            if (hierarchy != null && hierarchy.Configured)
            {
                foreach (log4net.Appender.IAppender appender in hierarchy.GetAppenders())
                {
                    if (appender is log4net.Appender.AdoNetAppender)
                    {
                        var adoNetAppender = (log4net.Appender.AdoNetAppender)appender;
                        adoNetAppender.ConnectionString = connectionStr;
                        adoNetAppender.ActivateOptions();
                    }
                }
            }
        }

        /// <summary>
        /// 构造消息实体
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static LogEntity BuildMessageMode(string message)
        {
            //if (logEntity == null)
            //{
            //    logEntity = new LogEntity();
            //    logEntity.UserID = UserID;
            //    logEntity.UserName = UserName;
            //    logEntity.Message = message;
            //}
            //else
            //    logEntity.Message = message;


            logEntity = new LogEntity();
            logEntity.UserID = UserID;
            logEntity.UserName = UserName;
            logEntity.HostIP = HostIP;
            logEntity.HostName = HostName;
            logEntity.Message = message;

            return logEntity;
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug(string message)
        {
            if (log.IsDebugEnabled)
                log.Debug(BuildMessageMode(message));
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Debug(string message, Exception ex)
        {
            if (log.IsDebugEnabled)
                log.Debug(BuildMessageMode(message), ex);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message)
        {
            if (log.IsInfoEnabled)
                log.Info(BuildMessageMode(message));
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Info(string message, Exception ex)
        {
            if (log.IsInfoEnabled)
                log.Info(BuildMessageMode(message), ex);
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message)
        {
            if (log.IsErrorEnabled)
                log.Error(BuildMessageMode(message));
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Error(string message, Exception exception)
        {
            if (log.IsErrorEnabled)
                log.Error(BuildMessageMode(message), exception);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        public static void Warn(string message)
        {
            if (log.IsWarnEnabled)
                log.Warn(BuildMessageMode(message));
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Warn(string message, Exception ex)
        {
            if (log.IsWarnEnabled)
                log.Warn(BuildMessageMode(message), ex);
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message">消息</param>
        public static void Fatal(string message)
        {
            if (log.IsFatalEnabled)
                log.Fatal(BuildMessageMode(message));
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Fatal(string message, Exception ex)
        {
            if (log.IsFatalEnabled)
                log.Fatal(BuildMessageMode(message), ex);
        }
    }
}
