using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Helper
{
   public static class EnumExtensions
    {
        public static T ParseEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
    }
}
