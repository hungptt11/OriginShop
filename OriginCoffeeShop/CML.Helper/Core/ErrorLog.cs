﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CML.Helper.Core
{
    public sealed class ErrorLog
    {
        const string logger = "ILogger";
        private static readonly ILog _logger;
        private static ILog GetLogger(string name)
        {
            ILog log = LogManager.GetLogger(name);
            return log;
        }
        static ErrorLog()
        {
            _logger = GetLogger(logger);
        }
        public static void Log(string message, LogType type = LogType.Error)
        {
            switch (type)
            {
                case LogType.Info:
                    _logger.InfoFormat(message);
                    break;
                case LogType.Warn:
                    _logger.WarnFormat(message);
                    break;
                case LogType.Error:
                    _logger.ErrorFormat(message);
                    break;
                case LogType.Fatal:
                    _logger.FatalFormat(message);
                    break;
                case LogType.Debug:
                    _logger.DebugFormat(message);
                    break;
            }
        }
        public static void Log(Exception ex)
        {
            Log(Environment.NewLine + " --> Source: " + ex.Source +
                Environment.NewLine + " --> Message: " + ex.Message +
                Environment.NewLine + " --> StackTrace: " + (ex.StackTrace.Contains(" in ") ? ex.StackTrace.Replace(" in ","#").Split('#')[0].Trim() : ex.StackTrace.Trim()));
        }
    }
    public enum LogType { Info, Warn, Error, Fatal, Debug }
}
