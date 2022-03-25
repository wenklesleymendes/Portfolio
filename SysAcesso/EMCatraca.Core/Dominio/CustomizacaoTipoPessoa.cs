using System;

namespace EMCatraca.Core.Dominio
{
    /// <summary>
    /// Customizaçõa o digito Tipo Pessoa
    /// </summary>
    [Serializable]
    public class CustomizacaoTipoPessoa
    {

        /// <remarks>
        /// <para>Aluno 1 para outro digito/para>
        /// <para>Valor padrão =<see langword="1"/> </para>
        /// </remarks>
        public int Aluno { get; set; }

        /// <remarks>
        /// <para>Professor 2 para outro digito/para>
        /// <para>Valor padrão =<see langword="2"/> </para>
        /// </remarks>
        public int Professor { get; set; }

        /// <remarks>
        /// <para>Profisisonal 3 para outro digito/para>
        /// <para>Valor padrão =<see langword="3"/> </para>
        /// </remarks>
        public int Profissional { get; set; }

        /// <remarks>
        /// <para>Responsavel 5 para outro digito/para>
        /// <para>Valor padrão =<see langword="5"/> </para>
        /// </remarks>
        public int Responsavel { get; set; }

        /// <remarks>
        /// <para>Autorizado Buscar Aluno 6 para outro digito/para>
        /// <para>Valor padrão =<see langword="6"/> </para>
        /// </remarks>
        /// 
        public int AutorizadoBuscarAluno { get; set; }

        public CustomizacaoTipoPessoa Cria()
        {
            var customizacaoTipoPessoa = new CustomizacaoTipoPessoa
            {
                Aluno = (int)TipoPessoa.Aluno,
                Professor = (int)TipoPessoa.Professor,
                Profissional = (int)TipoPessoa.Profissional,
                Responsavel = (int)TipoPessoa.Responsavel,
                AutorizadoBuscarAluno = (int)TipoPessoa.AutorizadoBuscarAluno
            };

            return customizacaoTipoPessoa;
        }
    }
}