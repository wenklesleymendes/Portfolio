using AutoMapper;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Atendimentos
{
    public class AtendimentoAgendamentoService : IAtendimentoAgendamentoService
    {
        private readonly IAtendimentoAgendamentoRepository _atendimentoAgendamentoRepository;
        private readonly IMapper _mapper;
        private readonly IAtendimentoRepository _atendimentoRepository;
        private readonly IAtendimentoService _atendimentoService;

        public AtendimentoAgendamentoService(IAtendimentoAgendamentoRepository atendimentoAgendamentoRepository,
            IAtendimentoRepository atendimentoRepository, IAtendimentoService atendimentoService, IMapper mapper)
        {
            _atendimentoAgendamentoRepository = atendimentoAgendamentoRepository;
            _atendimentoRepository = atendimentoRepository;
            _atendimentoService = atendimentoService;
            _mapper = mapper;
        }

        public async Task<DtoAtendimentoAgendamento> Inserir(DtoAtendimentoAgendamento dtoAtendimentoAgendamento)
        {
            DateTime? dataAgendamento = null;
            if (dtoAtendimentoAgendamento.DataAgendamento != null)
            {
                dataAgendamento = Convert.ToDateTime(dtoAtendimentoAgendamento.DataAgendamento.Split("T")[0] + " " + dtoAtendimentoAgendamento.HoraAgendamento);
            }

            var _atendimentoAgendamento = new AtendimentoAgendamento()
            {
                IdAtendimento = dtoAtendimentoAgendamento.IdAtendimento,
                Celular = dtoAtendimentoAgendamento.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""),
                DataAgendamento = dataAgendamento.ToString().Split(" ")[0],
                HoraAgendamento = dtoAtendimentoAgendamento.HoraAgendamento,
                DataeHoradoUltimoContato = DateTime.Now,
                TipoAgendamento = dtoAtendimentoAgendamento.TipoAgendamento,
                Situacao = dtoAtendimentoAgendamento.Situacao,
                Observacoes = dtoAtendimentoAgendamento.Observacoes,
                UsuarioCadastro = dtoAtendimentoAgendamento.UsuarioCadastro
            };

            if (EhSituacaoParaDesativarAtendimento(_atendimentoAgendamento.Situacao))
            {
                await AtualizaMotivoNaoAgendamento(_atendimentoAgendamento.IdAtendimento, _atendimentoAgendamento.Situacao);
            }

            var retorno = await _atendimentoAgendamentoRepository.Inserir(_atendimentoAgendamento);

            return _mapper.Map<DtoAtendimentoAgendamento>(retorno);
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> BuscarTodos()
        {
            var agendamentos = await _atendimentoAgendamentoRepository.BuscarTodos();
            var atendimentos = await _atendimentoRepository.GetAllAsync();

            var atendimentosAgendamentos = from agendamento in agendamentos
                                           join atendimento in atendimentos
                                           on agendamento.IdAtendimento equals atendimento.Id
                                           select new DtoAtendimentoAgendamento
                                           {
                                               Id = agendamento.Id,
                                               IdAtendimento = atendimento.Id,
                                               NomedoCliente = atendimento.NomedoCliente,
                                               HoraAgendamento = agendamento.HoraAgendamento,
                                               DataAgendamento = agendamento.DataAgendamento,
                                               DataeHoradoUltimoContato = agendamento.DataeHoradoUltimoContato.ToString(),
                                               TipoAgendamento = agendamento.TipoAgendamento,
                                               Situacao = agendamento.Situacao,
                                               Observacoes = agendamento.Observacoes,
                                               UsuarioCadastro = agendamento.UsuarioCadastro
                                           };
            
            return atendimentosAgendamentos;
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> BuscaPorUnidade(int idUnidade)
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            var agendamentos = await _atendimentoAgendamentoRepository.BuscarTodos();

            var agendamentosPorUnidade = from agendamento in agendamentos
                                         join atendimento in atendimentos
                                         on agendamento.IdAtendimento equals atendimento.Id
                                         where atendimento.UnidadeCadastro == idUnidade
                                         select new DtoAtendimentoAgendamento
                                         {
                                             Id = agendamento.Id,
                                             IdAtendimento = atendimento.Id,
                                             NomedoCliente = atendimento.NomedoCliente,
                                             HoraAgendamento = agendamento.HoraAgendamento,
                                             DataAgendamento = agendamento.DataAgendamento,
                                             DataeHoradoUltimoContato = agendamento.DataeHoradoUltimoContato.ToString().Replace(" ", " "),
                                             TipoAgendamento = agendamento.TipoAgendamento,
                                             Celular = agendamento.Celular,
                                             Situacao = agendamento.Situacao,
                                             Observacoes = agendamento.Observacoes,
                                             UsuarioCadastro = agendamento.UsuarioCadastro
                                         };

            var agendamentoFiltrado = agendamentosPorUnidade.Where(
                a => a.DataAgendamento == DateTime.Now.Date.ToString().Split(" ")[0])
                .OrderBy(a => a.HoraAgendamento);

            return agendamentoFiltrado;
        }

        public async Task<DtoAtendimentoAgendamento> BuscarPorId(int idAtendimentoAgendamento)
        {
            var agendamento = await _atendimentoAgendamentoRepository.BuscarPorIdAgendamento(idAtendimentoAgendamento);
            var retorno = _mapper.Map<DtoAtendimentoAgendamento>(agendamento);

            return retorno;
        }

        public async Task<DtoAtendimentoAgendamento> BuscaPorIdAtendimento(int idAtendimento)
        {
            var agendamento = await _atendimentoAgendamentoRepository.BuscaPorIdAtendimento(idAtendimento);

            return _mapper.Map<DtoAtendimentoAgendamento>(agendamento);
        }

        public async Task<IEnumerable<DtoAtendimentoAgendamento>> HistoricoAgendamentos(int idAtendimento)
        {
            var agendamento = await _atendimentoAgendamentoRepository.HistoricoTentativas(idAtendimento);

            var agendamentosOrdernadosData = agendamento.OrderByDescending(a => a.HoraAgendamento).OrderByDescending(a => a.DataAgendamento);

            return _mapper.Map<IEnumerable<DtoAtendimentoAgendamento>>(agendamentosOrdernadosData);
        }

        public async Task UpdateAgendamento(DtoAtendimentoAgendamento dtoAtendimentoAgendamento)
        {
            try
            {
                var _atendimentoAgendamento = new AtendimentoAgendamento()
                {
                    Id = dtoAtendimentoAgendamento.Id,
                    IdAtendimento = dtoAtendimentoAgendamento.IdAtendimento,
                    Celular = dtoAtendimentoAgendamento.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""),
                    DataAgendamento = dtoAtendimentoAgendamento.DataAgendamento,
                    HoraAgendamento = dtoAtendimentoAgendamento.HoraAgendamento,
                    DataeHoradoUltimoContato = DateTime.Now,
                    TipoAgendamento = dtoAtendimentoAgendamento.TipoAgendamento,
                    Situacao = dtoAtendimentoAgendamento.Situacao,
                    Observacoes = dtoAtendimentoAgendamento.Observacoes,
                    UsuarioCadastro = dtoAtendimentoAgendamento.UsuarioCadastro
                };

                await _atendimentoAgendamentoRepository.UpdateAsync(_atendimentoAgendamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Object>> FiltraAgendamentosPorData(string data)
        {
            var atendimentos = await _atendimentoRepository.GetAllAsync();
            var agendamentos = await _atendimentoAgendamentoRepository.BuscarTodos();

            var agendamentosPorUnidade = from agendamento in agendamentos
                                         join atendimento in atendimentos
                                         on agendamento.IdAtendimento equals atendimento.Id
                                         where agendamento.DataAgendamento == data
                                         select new 
                                         {
                                             Id = agendamento.Id,
                                             IdAtendimento = atendimento.Id,
                                             NomedoCliente = atendimento.NomedoCliente,
                                             HoraAgendamento = agendamento.HoraAgendamento,
                                             DataAgendamento = agendamento.DataAgendamento,
                                             DataeHoradoUltimoContato = agendamento.DataeHoradoUltimoContato.ToString().Replace(" ", " "),
                                             TipoAgendamento = agendamento.TipoAgendamento,
                                             Celular = agendamento.Celular,
                                             Situacao = agendamento.Situacao,
                                             Observacoes = agendamento.Observacoes,
                                             Unidade = atendimento.UnidadeCadastro
                                         };

            var agendamentoFiltrado = agendamentosPorUnidade.OrderBy(a => a.HoraAgendamento);

            return agendamentoFiltrado;
        }

        public async Task<bool> Deletar(int idAgendamento)
        {
            try
            {
                return await _atendimentoAgendamentoRepository.Excluir(idAgendamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private bool EhSituacaoParaDesativarAtendimento(SituacaAgendamento situacao) => situacao switch
        {
            SituacaAgendamento.TelefoneIncorretoNaoExiste => true,
            SituacaAgendamento.JaMatriculadoemNossaEscola => true,
            SituacaAgendamento.RealizouMatriculaemOutraEscola => true,
            SituacaAgendamento.NaoTemMaisInteresse => true,
            _ => false
        };

        private async Task AtualizaMotivoNaoAgendamento(int idAtendimento, SituacaAgendamento situacaoAgendamento)
        {
            var atendimento = await _atendimentoRepository.GetByIdAsync(idAtendimento);

            const int TelefoneIncorreOuInexitente = 8;
            const int matriculado = 9;            
            const int MatriculadoOutraInstuicao = 10;
            const int NaoTemMaisInteresse = 11;

            switch (situacaoAgendamento)
            {
                case SituacaAgendamento.TelefoneIncorretoNaoExiste:
                    atendimento.MotivodoNaoAgendamento = TelefoneIncorreOuInexitente;
                    break;
                case SituacaAgendamento.JaMatriculadoemNossaEscola:
                    atendimento.MotivodoNaoAgendamento = matriculado;
                    break;
                case SituacaAgendamento.RealizouMatriculaemOutraEscola:
                    atendimento.MotivodoNaoAgendamento = MatriculadoOutraInstuicao;
                    break;
                case SituacaAgendamento.NaoTemMaisInteresse:
                        atendimento.MotivodoNaoAgendamento = NaoTemMaisInteresse;
                    break;
            };

            await _atendimentoService.UpdateAtendimento(_mapper.Map<DtoAtendimento>(atendimento));
        }
    }
}
