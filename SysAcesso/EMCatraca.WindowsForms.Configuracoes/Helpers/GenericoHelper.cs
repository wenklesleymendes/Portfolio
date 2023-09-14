using System;
using System.IO;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{

    public static class ExtensionMethods
    {
        public static T Clone<T>(this T origem, System.Windows.Forms.TabPage tpArquivo)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("O tipo de classe não pode ser serializada.", "EMCatraca.Configuracao.Generico.Clone");
            }

            if (Object.ReferenceEquals(origem, null))
            {
                return default(T);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, origem);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
