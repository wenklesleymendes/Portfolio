using EMCatraca.Core.Negocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TeraByte.Core.Negocio.Enumeradores
{
    [Serializable]
    public abstract class EnumeradorSeguro<T, K> : IEnumeradorSeguro<K>, IComparable where T : IEnumeradorSeguro<K>
    {
        private Func<string> _descricaoDelegate;
        private Func<string> _descricaoResumidaDelegate;

        protected EnumeradorSeguro(K codigo, Func<string> descricaoDelegate)
        {
            Codigo = codigo;
            _descricaoDelegate = descricaoDelegate;
            _descricaoResumidaDelegate = descricaoDelegate;
        }

        protected EnumeradorSeguro(K codigo, Func<string> descricaoDelegate, Func<string> descricaoResumidaDelegate)
        {
            Codigo = codigo;
            _descricaoDelegate = descricaoDelegate;
            _descricaoResumidaDelegate = descricaoResumidaDelegate;
        }

        protected EnumeradorSeguro(K codigo, string descricao)
            : this(codigo, () => descricao)
        {
        }

        protected EnumeradorSeguro(K codigo, string descricao, string descricaoResumida)
            : this(codigo, () => descricao, () => descricaoResumida)
        {
        }

        public string DescricaoResumida
        {
            get => _descricaoResumidaDelegate();
            protected set { _descricaoResumidaDelegate = () => value; }
        }

        public int CompareTo(object other)
        {
            if (other == null)
            {
                return 1;
            }

            if (!(other is EnumeradorSeguro<T, K>))
            {
                throw new ArgumentException("Item to compare is not a EnumeradorSeguro");
            }

            return ((IComparable)Codigo).CompareTo(((EnumeradorSeguro<T, K>)other).Codigo);
        }


        public K Codigo { get; protected set; }

        public string Descricao
        {
            get => _descricaoDelegate();
            protected set { _descricaoDelegate = () => value; }
        }

        public override bool Equals(object obj)
        {
            return obj is EnumeradorSeguro<T, K> && ((EnumeradorSeguro<T, K>)obj).Codigo.Equals(Codigo);
        }

        public bool Equals(EnumeradorSeguro<T, K> obj)
        {
            return (obj != null) && obj.Codigo.Equals(Codigo);
        }

        public override string ToString()
        {
            return Descricao;
        }

        public override int GetHashCode()
        {
            return Codigo.GetHashCode();
        }

        public static T Obtenha(K codigo)
        {
            return Obtenha<T>(codigo);
        }

        public static List<T> ObtenhaTodos()
        {
            return ObtenhaTodos<T>();
        }

        protected static TEnumerador Obtenha<TEnumerador>(K codigo) where TEnumerador : IEnumeradorSeguro<K>
        {
            var todos = ObtenhaTodos<TEnumerador>();

            foreach (var item in todos)
            {
                if (item.Codigo.Equals(codigo))
                {
                    return item;
                }
            }

            return default;
        }

        protected static List<TEnumerador> ObtenhaTodos<TEnumerador>() where TEnumerador : IEnumeradorSeguro<K>
        {
            var tipo = typeof(TEnumerador);
            return tipo.GetFields(BindingFlags.Static | BindingFlags.Public).Select(campo => (TEnumerador)campo.GetValue(null)).ToList();
        }
    }
}