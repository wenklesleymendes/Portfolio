using System;
using System.Globalization;

namespace EMCatraca.WindowsForms.Configuracoes.ObjetosValorados
{
    public struct Flutuante : IComparable, IComparable<Flutuante>, IConvertible
    {
        public static readonly Flutuante Zero = new Flutuante(0m);

        public static readonly CultureInfo PtBr = new CultureInfo("pt-br");

        private readonly decimal _valor;

        public Flutuante(decimal valor)
            : this()
        {
            _valor = valor;
        }

        public Flutuante(double valor)
            : this()
        {
            _valor = Convert.ToDecimal(valor);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Flutuante || obj is float || obj is double)
            {
                return _valor == Convert.ToDecimal(obj);
            }

            if (obj is string objString)
            {
                if (decimal.TryParse(objString, out var valorDecimal))
                {
                    return _valor == valorDecimal;
                }
            }

            return false;
        }

        public override int GetHashCode() => _valor.GetHashCode();

        public string ToStringSemCasas() => _valor.ToString("#,###,##0", PtBr);

        public string ToStringUmaCasa() => _valor.ToString("#,###,##0.0", PtBr);

        public override string ToString() => _valor.ToString("#,###,##0.00", PtBr);

        public string ToString(string formato) => _valor.ToString(formato, CultureInfo.InvariantCulture);

        public string ToStringCurrency() => _valor.ToString("#,###,##0.00", PtBr);

        public string ToStringPercent() => _valor.ToString(PtBr);

        public string ToStringPercentComSimbol() => $"{_valor.ToString(PtBr)}%";

        public string ToStringPercentDuasCasas() => _valor.ToString("#,###,##0.00", PtBr);

        public string ToStringPercentDuasCasasComSimbol() => $"{_valor.ToString("#,###,##0.00", PtBr)}%";

        public string ToStringPercentQuatroCasas() => _valor.ToString("#,###,##0.0000", PtBr);

        public string ToStringPercentQuatroCasasComSymbol() => $"{_valor.ToString("#,###,##0.0000", PtBr)}%";

        public string ToStringPercentAteQuatroCasasComSymbol() => $"{_valor.ToString("#,###,##0.####", PtBr)}%";

        public string ToStringCurrencySymbol() => string.Concat(PtBr.NumberFormat.CurrencySymbol, " ", _valor.ToString("#,###,##0.00", PtBr));

        public static explicit operator decimal(Flutuante valorFlutuante) => valorFlutuante._valor;

        public static implicit operator double(Flutuante valorFlutuante) => Convert.ToDouble(valorFlutuante._valor);

        public static implicit operator Flutuante(double valor) => new Flutuante(Convert.ToDecimal(valor));

        public static implicit operator Flutuante(decimal valor) => new Flutuante(valor);

        public static implicit operator Flutuante(int valor) => new Flutuante(Convert.ToDecimal(valor));

        public static implicit operator Flutuante(string valorString)
        {
            decimal.TryParse(valorString, NumberStyles.Any, CultureInfo.InvariantCulture, out var valorFlutuante);
            return new Flutuante(valorFlutuante);
        }

        public static bool operator ==(string valorString, Flutuante valorMoeda)
        {
            Flutuante valor = valorString;
            return valorMoeda.Equals(valor);
        }

        public static bool operator !=(string valorString, Flutuante valorMoeda)
        {
            Flutuante valor = valorString;
            return !valorMoeda.Equals(valor);
        }

        public static bool operator ==(double valorFlutuante, Flutuante valorMoeda)
        {
            Flutuante valor = valorFlutuante;
            return valorMoeda.Equals(valor);
        }

        public static bool operator !=(double valorFlutuante, Flutuante valorMoeda)
        {
            Flutuante valor = valorFlutuante;
            return !valorMoeda.Equals(valor);
        }

        #region IComparable<Flutuante> Members

        public int CompareTo(Flutuante other) => _valor.CompareTo(other._valor);

        #endregion

        #region IComparable Members

        public int CompareTo(object obj) => CompareTo((Flutuante)obj);

        #endregion

        #region IConvertible Members

        public TypeCode GetTypeCode() => TypeCode.Decimal;

        public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(_valor);

        public char ToChar(IFormatProvider provider) => Convert.ToChar(_valor);

        public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(_valor);

        public byte ToByte(IFormatProvider provider) => Convert.ToByte(_valor);

        public short ToInt16(IFormatProvider provider) => Convert.ToInt16(_valor);

        public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(_valor);

        public int ToInt32(IFormatProvider provider) => Convert.ToInt32(_valor);

        public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(_valor);

        public long ToInt64(IFormatProvider provider) => Convert.ToInt64(_valor);

        public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(_valor);

        public float ToSingle(IFormatProvider provider) => Convert.ToSingle(_valor);

        public double ToDouble(IFormatProvider provider) => Convert.ToDouble(_valor);

        public decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(_valor);

        public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(_valor);

        public string ToString(IFormatProvider provider) => Convert.ToString(_valor);

        public object ToType(Type conversionType, IFormatProvider provider) => Convert.ChangeType(_valor, conversionType);

        #endregion
    }
}
