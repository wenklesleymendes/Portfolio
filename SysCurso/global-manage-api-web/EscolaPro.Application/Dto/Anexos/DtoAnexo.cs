using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Anexos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto
{
    public class DtoAnexo
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public byte[] Arquivo { get; set; }

        [JsonIgnore]
        public DateTime? DataAnexo { get; set; }
        public string ArquivoString { get; set; }
        public TipoAnexoEnum TipoAnexo { get; set; }
        public string Extensao { get; set; }
        public int? UnidadeId { get; set; }
        public int? AlunoId { get; set; }
        public int? FuncionarioId { get; set; }
        public int? FeriasFuncionarioId { get; set; }
        public int? PontoEletronicoId { get; set; }
        public int? MensagemTicketId { get; set; }
        public int? FornecedorId { get; set; }
        public int? FolhaPagamentoId { get; set; }
        public int? DespesaId { get; set; }
        public int? DestinatarioTicketId { get; set; }
        public int? PerguntaId { get; set; }
        public int? RespostaId { get; set; }
        public int? MatriculaAlunoId { get; set; }
        public int? MensagemAlunoProfessorId { get; set; }
        public int? SolicitacaoId { get; set; }
        public int? SolicitacaoAlunoId { get; set; }
        public bool IsDelete { get; set; }

        // Recusar Documento
        public string Mensagem { get; set; }
        public bool IsRecusado { get; set; }
        public TipoRecusaEnum TipoRecusa { get; set; }
    }

    public class AnexoRecusar
    {
        public int AnexoId { get; set; }
        public string Mensagem { get; set; }
        public TipoRecusaEnum TipoRecusa { get; set; }
    }
}
