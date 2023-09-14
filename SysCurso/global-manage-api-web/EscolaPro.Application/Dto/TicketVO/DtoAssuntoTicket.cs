using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.UnidadeVO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoAssuntoTicket
    {
        public DtoAssuntoTicket()
        {
            this.FuncionarioAssuntoTicket = new List<DtoFuncionarioAssuntoTicket>();
        }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int TempoEmDias { get; set; }
        public int[] FuncionarioIds { get; set; }
        public ICollection<DtoFuncionarioAssuntoTicket> FuncionarioAssuntoTicket { get; set; }
        public DtoUnidadeTurma Unidade { get; set; }
                public int? UnidadeId { get; set; }
        public DtoCentroCusto CentroCusto { get; set; }
        public int? CentroCustoId { get; set; }


        public List<DtoUsuario> Usuarios
        {
            get { return this.FuncionarioAssuntoTicket?.Select(x => x.Usuario).ToList(); }
        }

       

    }
}
