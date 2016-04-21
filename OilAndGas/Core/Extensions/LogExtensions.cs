using System;
using System.Reflection;
using log4net;

namespace Core.Extensions
{
    public static class LogExtensions
    {
        public static void LogFeatureUsage(this ILog log, string featureName)
        {
            log.InfoFormat("Usage of feature: {0}", featureName);
        }

        public static void LogMethodArguments(this ILog log, string methodName, params Func<object>[] expr)
        {
            var logmessage = String.Format("Method's {0}  arguments >> ", methodName);
            if (expr.Length == 0)
            {
                logmessage = String.Concat(logmessage, "No arguments.");
            }
            else
            {
                try
                {
                    foreach (var par in expr)
                    {
                        // get IL code behind the delegate
                        var methodBody = par.Method.GetMethodBody();
                        
                        if (methodBody == null) 
                            continue;
                        
                        var il = methodBody.GetILAsByteArray();
                        
                        // bytes 2-6 represent the field handle
                        var fieldHandle = BitConverter.ToInt32(il, 2);
                        
                        // resolve the handle
                        var field = par.Target.GetType().Module.ResolveField(fieldHandle);

                        logmessage = String.Concat(logmessage, String.Format("{0}: {1}. ", field.Name, par() ?? "@null"));
                    }
                }
                catch
                {
                    logmessage = String.Concat(logmessage," FAILED reading arguments.");
                    // suppress the errors while logging.
                }
            }
            log.Info(logmessage);
        }
    }
}
