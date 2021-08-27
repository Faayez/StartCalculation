using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace StartCalculation.Core.Utility
{
    public static class EnumUtility
    {
        public static string GetDisplayValue(this Enum input)
        {
            if (input is null)
            {
                return default;
            }

            return input.GetType().GetMember(input.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
        }
    }
}