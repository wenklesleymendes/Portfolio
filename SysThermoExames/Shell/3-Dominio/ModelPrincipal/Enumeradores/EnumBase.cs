using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelPrincipal.Enumeradores
{
    public class EnumBase : IComparable
    {
        public string Descricao { get; private set; }

        public int Codico { get; set; }

        protected EnumBase(int id, string name) => (Codico, Descricao) = (id, name);

        public override string ToString() => Descricao;

        public static IEnumerable<T> GetAll<T>() where T : EnumBase =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {            
            if (obj.GetType() != typeof(EnumBase))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());

            if(!typeMatches)
            {
                return false;
            }

            EnumBase other = (EnumBase)obj;
            var valueMatches = Codico.Equals(other.Codico);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Codico, Descricao);
        }

        public int CompareTo(object other) => Codico.CompareTo(((EnumBase)other).Codico);
    }
}
