using EMCatraEMCatraca.WindowsForms.Configuracoes.Helpers;
using System;
using System.Reflection;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{
    public class PropertyAccessor : IPropertyAccessor
    {
        private readonly PropertyInfo propertyInfo = null;

        public PropertyAccessor(Type ObjectType, string PropertyName)
        {
            propertyInfo = ObjectType.GetProperty(PropertyName);
        }

        #region IPropertyAccessor Members

        public object Get(object target)
        {
            return propertyInfo.GetValue(target, null);
        }

        public void Set(object target, object value)
        {
            propertyInfo.SetValue(target, value, null);
        }

        #endregion
    }
}
