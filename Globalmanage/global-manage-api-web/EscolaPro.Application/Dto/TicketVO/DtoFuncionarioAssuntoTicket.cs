using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Service.Dto.TicketVO
{
    public class DtoFuncionarioAssuntoTicket
    {
        public DtoFuncionarioAssuntoTicket()
        {
            this.Usuario = new DtoUsuario();
        }
        public int Id { get; set; }
        public DtoUsuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public int AssuntoTicketId { get; set; }
    }
}
