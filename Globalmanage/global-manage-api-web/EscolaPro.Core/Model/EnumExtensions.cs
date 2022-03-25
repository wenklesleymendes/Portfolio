using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace EscolaPro.Core.Model
{
    public static class EnumExtensions
    {
        public static String GetDisplayName(this Enum enumValue)
        {
            var enumMember = enumValue.GetType()
                            .GetMember(enumValue.ToString());

            DisplayAttribute displayAttrib = null;
            if (enumMember.Any())
            {
                displayAttrib = enumMember
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();
            }

            String name = null;
            Type resource = null;

            if (displayAttrib != null)
            {
                name = displayAttrib.Name;
                resource = displayAttrib.ResourceType;
            }

            return String.IsNullOrEmpty(name) ? enumValue.ToString()
                : resource == null ? name
                : new ResourceManager(resource).GetString(name);
        }
    }
}
