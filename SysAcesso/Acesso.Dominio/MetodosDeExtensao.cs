using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Acesso.Dominio
{
    public static class MetodosDeExtensao
    {
        public static string GetDescription<T>(this T value) where T : IConvertible
        {
            return (value is Enum) ?
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description ?? string.Empty : string.Empty;
        }
    }
}
