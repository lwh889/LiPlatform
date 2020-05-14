﻿
using log4net.Layout.Pattern;
using System.Reflection;

namespace LiLog
{
    public class LiPatternCConverter : PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            else
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
        }

        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);

            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);

            return propertyValue;
        }

    }
}
