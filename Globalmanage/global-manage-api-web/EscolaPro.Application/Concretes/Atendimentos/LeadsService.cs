using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Atendimentos;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.Atendimentos;
using EscolaPro.Service.Dto.AtendimentoVO;
using EscolaPro.Service.Interfaces.Atendimentos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes.Atendimentos
{
    public class LeadsService : ILeadsService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public LeadsService(ILeadsRepository leadsRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _leadsRepository = leadsRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<DtoLeads> Inserir(string leads)
        {
            try
            {
                DtoLeads dtoLeads = new DtoLeads
                {
                    Texto = leads,
                    DataCriacao = DateTime.Now,
                    Origem = "Leadster",
                    Status = 0,
                };

                var retorno = await _leadsRepository.AddAsync(_mapper.Map<Leads>(dtoLeads));

                return _mapper.Map<DtoLeads>(retorno);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<DtoLeads>> BuscarTodos()
        {
            var leads = await _leadsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DtoLeads>>(leads);
        }

        public async Task<IEnumerable<DtoLeads>> BuscaLeadsPorStatus()
        {
            var leads = await _leadsRepository.GetAllAsync();

            var leadsFiltrados = leads.Where(l => l.Status == 0);

            return _mapper.Map<IEnumerable<DtoLeads>>(leadsFiltrados);
        }

        public async Task AtualizaStatusLeads(int id, int status)
        {
            try
            {
                var lead = await _leadsRepository.GetByIdAsync(id);

                var statusProcessado = 1;
                
                lead.Status = statusProcessado;
                
                lead.DataProcessamento = DateTime.Now;
                
                await _leadsRepository.UpdateAsync(lead);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DtoAtendimento>> ProcessaDadosLeads()
        {
            List<DtoAtendimento> listaAtendimentoLeads = new List<DtoAtendimento>();

            var statusNaoProcessado = 0;

            var leads = await _leadsRepository.BuscaLeadsPorStatus(statusNaoProcessado);

            if (!leads.Any())
            {
                foreach (var lead in leads)
                {
                    var json = lead.Texto;

                    DtoLeadJson leadConvertido = await ConvertaEmObjetoLead(json);

                    DtoAtendimento dtoAtendimento = new DtoAtendimento
                    {
                        NomedoCliente = leadConvertido.Nome,
                        Celular = leadConvertido.Celular,
                        ComonosConheceu = leadConvertido.ComoNosConheceu,
                        UnidadeCadastro = leadConvertido.UnidadeCadastro,
                        CanaldeAtendimento = leadConvertido.CanaldeAtendimentoLeads,
                        UsuarioCadastro = leadConvertido.UsuarioUnidade,
                        UsuarioLogado = leadConvertido.UsuarioUnidade
                    };

                    listaAtendimentoLeads.Add(dtoAtendimento);

                    var statusProcessado = 1;
                    await AtualizaStatusLeads(lead.Id, statusProcessado);
                }

                return listaAtendimentoLeads;
            }

            return listaAtendimentoLeads;
        }

        public async Task<DtoLeadJson> ConvertaEmObjetoLead(string json)
        {
            DtoLeadJson lead = JsonConvert.DeserializeObject<DtoLeadJson>(json);

            var ehUnidadeValida = int.TryParse(lead.Unidade, out var unidade);

            var unidadeInformada = ehUnidadeValida ? unidade : 2;

            var atendimentoLeads = 10;

            lead.Celular = string.IsNullOrEmpty(lead.Celular) ? lead.Celular : lead.Celular.Replace("(","").Replace(")","").Replace(" ","").Replace("-","");

            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuarioDaUnidade = usuarios.Where(u => u.PerfilUsuarioId == 8 && u.UnidadeId == unidadeInformada).FirstOrDefault();

            lead.UnidadeCadastro = unidadeInformada;
            lead.UsuarioUnidade = usuarioDaUnidade.Id;
            lead.CanaldeAtendimentoLeads = atendimentoLeads;
            lead.ComoNosConheceu = ComoConheceuEnum.Leads.GetHashCode();

            return lead;
        }
    }
}
