using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Helper
{
    public static class Logger
    {
        public static void InformationLog(string InfoMessage)
        {
            try
            {
                Log.Information(InfoMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ErrorLog(string ErrorMessage)
        {
            try
            {
                Log.Error(ErrorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Close()
        {
            try
            {
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ErrorLog(Exception ex, string v)
        {
            throw new NotImplementedException();
        }
    }
}
