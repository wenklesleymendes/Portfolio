using EscolaPro.Core.Model.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Repository.Scripts
{
    public class TicketScript
    {
        public static string Filtrar(FiltrarTicket filtrarTicket)
        {
            string sqlQueryFiltro = @"select DISTINCT Ticket.ID from Ticket ticket
                                        inner join DestinatarioTicket destinatarioTicket
                                        on ticket.Id = destinatarioTicket.TicketId ";

            //if (filtrarTicket.UnidadeId.HasValue || !string.IsNullOrEmpty(filtrarTicket.NomeResponsavel))
            //{
                sqlQueryFiltro = sqlQueryFiltro + @"inner join UsuarioDestinarioTicket usuarioDestinatario
                                        on destinatarioTicket.Id = usuarioDestinatario.DestinatarioTicketId
                                        inner join Funcionario funcionario
                                        on usuarioDestinatario.FuncionarioId = funcionario.Id";
            //}

            sqlQueryFiltro = sqlQueryFiltro + $" where ticket.IsDelete = 0 and DestinatarioTicket.UsuarioLogadoId = {filtrarTicket.UsuarioLogadoId}" +
                                              $" or usuarioDestinatario.FuncionarioId = {filtrarTicket.UsuarioLogadoId} ";

            if (filtrarTicket.PeriodoAberturaInicio.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.DataAbertura >= '{filtrarTicket.PeriodoAberturaInicio.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (filtrarTicket.PeriodoAberturaFim.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.DataAbertura <= '{filtrarTicket.PeriodoAberturaFim.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (!string.IsNullOrEmpty(filtrarTicket.NumeroProtocolo))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.NumeroProtocolo like '%{filtrarTicket.NumeroProtocolo}%'";
            }

            if (!string.IsNullOrEmpty(filtrarTicket.NomeResponsavel))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and funcionario.Nome like '%{filtrarTicket.NomeResponsavel}%'";
            }

            if (filtrarTicket.AssuntoTicketId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.AssuntoTicketId = '{filtrarTicket.AssuntoTicketId}'";
            }

            if (filtrarTicket.UnidadeId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and DestinatarioTicket.UnidadeId = '{filtrarTicket.UnidadeId}'";
            }

            if (filtrarTicket.DepartamentoId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"or DestinatarioTicket.DepartamentoId = '{filtrarTicket.DepartamentoId}'";
            }

            return sqlQueryFiltro;
        }
        public static string FiltrarTicketEncaminhados(FiltrarTicket filtrarTicket)
        {
            string sqlQueryFiltro = @"select DISTINCT Ticket.ID from Ticket ticket
                                        inner join DestinatarioTicket destinatarioTicket
                                        on ticket.Id = destinatarioTicket.TicketId ";

            //if (filtrarTicket.UnidadeId.HasValue || !string.IsNullOrEmpty(filtrarTicket.NomeResponsavel))
            //{
            sqlQueryFiltro = sqlQueryFiltro + @"inner join UsuarioDestinarioTicket usuarioDestinatario
                                        on destinatarioTicket.Id = usuarioDestinatario.DestinatarioTicketId
                                        inner join Funcionario funcionario
                                        on usuarioDestinatario.FuncionarioId = funcionario.Id";
            //}

            sqlQueryFiltro = sqlQueryFiltro + $" where ticket.IsDelete = 0 and usuarioDestinatario.FuncionarioId = {filtrarTicket.UsuarioLogadoId}";

            if (filtrarTicket.PeriodoAberturaInicio.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.DataAbertura >= '{filtrarTicket.PeriodoAberturaInicio.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (filtrarTicket.PeriodoAberturaFim.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.DataAbertura <= '{filtrarTicket.PeriodoAberturaFim.Value.Date.ToString("yyyy-MM-dd")}'";
            }

            if (!string.IsNullOrEmpty(filtrarTicket.NumeroProtocolo))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.NumeroProtocolo like '%{filtrarTicket.NumeroProtocolo}%'";
            }

            if (!string.IsNullOrEmpty(filtrarTicket.NomeResponsavel))
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and funcionario.Nome like '%{filtrarTicket.NomeResponsavel}%'";
            }

            if (filtrarTicket.AssuntoTicketId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and ticket.AssuntoTicketId = '{filtrarTicket.AssuntoTicketId}'";
            }

            if (filtrarTicket.UnidadeId.HasValue)
            {
                sqlQueryFiltro = sqlQueryFiltro + $"and DestinatarioTicket.UnidadeId = '{filtrarTicket.UnidadeId}'";
            }

            return sqlQueryFiltro;
        }
    }
}
