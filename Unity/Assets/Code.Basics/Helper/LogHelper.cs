using System;


public static class LogHandler
{
    public static Action<string> DebugHandler;
    public static Action<string> WarngHandler;
    public static Action<string> ErrorHandler;
    public static Action<Exception> ExceptionHandler;
}

namespace EGameFrame
{
    public static class Log
    {
        public static void Debug(string log)
        {
            LogHandler.DebugHandler?.Invoke(log);
        }

        public static void Warning(string log)
        {
            LogHandler.WarngHandler?.Invoke(log);
        }

        public static void Error(string log)
        {
            LogHandler.ErrorHandler?.Invoke(log);
        }

        public static void Exception(Exception e)
        {
            LogHandler.ExceptionHandler?.Invoke(e);
        }
    }
}

namespace ET
{
    public static class Log
    {
        public static void Msg(object log)
        {
            LogHandler.DebugHandler?.Invoke($"{log}");
        }

        public static void Info(string log)
        {
            LogHandler.DebugHandler?.Invoke(log);
        }

        public static void Debug(string log)
        {
            LogHandler.DebugHandler?.Invoke(log);
        }

        public static void Warning(string log)
        {
            LogHandler.WarngHandler?.Invoke(log);
        }

        public static void Warning(string message, params object[] args)
        {
            Warning(string.Format(message, args));
        }

        public static void Error(string log)
        {
            LogHandler.ErrorHandler?.Invoke(log);
        }

        public static void Error(Exception e)
        {
            LogHandler.ErrorHandler?.Invoke(e.ToString());
        }
    }
}