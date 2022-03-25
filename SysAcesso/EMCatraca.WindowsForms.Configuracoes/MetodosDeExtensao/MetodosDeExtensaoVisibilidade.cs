using System.Collections.Generic;

namespace EMCatraca.WindowsForms.Configuracoes.MetodosDeExtensao
{
    public static class MetodosDeExtensaoVisibilidade
    {
        public static void AddSemDuplicidade<T>(this IList<T> colecao, T itemParaAdicionar)
        {
            if (itemParaAdicionar != null && !colecao.Contains(itemParaAdicionar))
            {
                colecao.Add(itemParaAdicionar);
            }
        }

        public static void AddNaoNulo<T>(this IList<T> colecao, T itemParaAdicionar)
        {
            if (itemParaAdicionar != null)
            {
                colecao.Add(itemParaAdicionar);
            }
        }

        public static void AddNaoNulo<T>(this HashSet<T> colecao, T itemParaAdicionar)
        {
            if (itemParaAdicionar != null)
            {
                colecao.Add(itemParaAdicionar);
            }
        }

        public static void AddNaoNuloSemDuplicidade<Chave, Valor>(this IDictionary<Chave, Valor> colecao, Chave chave, Valor valor)
        {
            if (chave != null && valor != null && !colecao.ContainsKey(chave))
            {
                colecao.Add(chave, valor);
            }
        }
    }
}
