using SuperShop.IService;
using System;
using System.Reflection;

namespace SuperShop.Service
{
    public class LogService : ILogService
    {
        public LogService()
        {
        }

        public string UpdateDifference(dynamic Previous, dynamic New)
        {
            string res = string.Empty;

            Type type = Previous.GetType();

            foreach (PropertyInfo property in type.GetProperties())
            {
                object originalValue = property.GetValue(Previous, null);
                object newValue = property.GetValue(New, null);

                if (!object.Equals(originalValue, newValue))
                {
                    string originalText = (originalValue != null) ?
                        originalValue.ToString() : "[NULL]";

                    string newText = (newValue != null) ?
                        newValue.ToString() : "[NULL]";

                    res += $"{property.Name}: {originalText} Changed Into : {newText}\n";
                }
            }

            return res;
        }
    }
}
