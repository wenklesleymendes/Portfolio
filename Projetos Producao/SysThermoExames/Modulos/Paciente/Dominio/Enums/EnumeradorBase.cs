using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MdPaciente.Dominio
{
    public class EnumeradorBase
    {
        public string Descricao { get; private set; }

        public int Id { get; private set; }

        protected EnumeradorBase(int id, string name) => (Id, Descricao) = (id, name);

        public override string ToString() => Descricao;

        public static IEnumerable<T> GetAll<T>() where T : EnumeradorBase =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(EnumeradorBase))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());

            if (!typeMatches)
            {
                return false;
            }

            EnumeradorBase other = (EnumeradorBase)obj;
            var valueMatches = Id.Equals(other.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Descricao);
        }

        public int CompareTo(object other) => Id.CompareTo(((EnumeradorBase)other).Id);
    }
}
