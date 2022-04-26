using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.MetodosDeExtensao
{
    public static class MetodosDeExtensaoBindingSource
    {
        /// <summary>
        /// Obtém uma lista da coleção de objetos do DataSource (Utilizar somente quando o conteúdo do DataSource for uma coleção)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        public static List<T> ObtenhaObjetos<T>(this BindingSource bindingSource)
        {
            var enumerable = bindingSource.DataSource as IEnumerable;
            return enumerable?.Cast<T>().ToList() ?? bindingSource.List?.Cast<T>().ToList();
        }
    }
}
