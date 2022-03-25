using EMCatraEMCatraca.WindowsForms.Configuracoes.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{
    public enum CompareOrder { Ascending, Descending }

    public class PropertyComparer : IComparer, IComparer<object>
    {
        private readonly string _propertyName;
        private readonly CompareOrder _ordem;
        private IPropertyAccessor _propertyAccessor;

        public PropertyComparer(string propertyName, CompareOrder ordem)
        {
            _propertyName = propertyName;
            _ordem = ordem;
        }

        #region IComparer<object> Members

        public int Compare(object x, object y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (_propertyAccessor == null)
            {
                _propertyAccessor = new PropertyAccessor(x.GetType(), _propertyName);
            }

            var a = _propertyAccessor.Get(x);
            var b = _propertyAccessor.Get(y);

            if (a != null && b == null)
            {
                return 1;
            }

            if (a == null && b != null)
            {
                return -1;
            }

            if (a == null && b == null)
            {
                return 0;
            }

            if (!(a is IComparable))
            {
                return 0;
            }

            if (a is DateTime dataA && b is DateTime dataB)
            {
                return _ordem == CompareOrder.Ascending ? DateTime.Compare(dataA, dataB) : DateTime.Compare(dataB, dataA);
            }

            return _ordem == CompareOrder.Ascending ? Comparer.DefaultInvariant.Compare(a, b) : Comparer.DefaultInvariant.Compare(b, a);
        }

        #endregion
    }

}
