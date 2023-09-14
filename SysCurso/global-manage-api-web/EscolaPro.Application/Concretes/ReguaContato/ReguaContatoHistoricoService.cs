using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.ReguaContato;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EscolaPro.Service.Interfaces.ReguaContato;
using EscolaPro.Repository;

namespace EscolaPro.Service.Concretes.ReguaContato
{
    public class ReguaContatoHistoricoService : IReguaContatoHistoricoService
    {
        IReguaContatoHistoricoRepository _reguaContatoHistoricoRepository;
        public ReguaContatoHistoricoService(IReguaContatoHistoricoRepository reguaContatoHistoricoRepository)
        {
            _reguaContatoHistoricoRepository = reguaContatoHistoricoRepository;
        }

        public async Task<ReguaContatoHistorico> Inserir(ReguaContatoHistorico reguaContatoHistorico)
        {
            await _reguaContatoHistoricoRepository.AddAsync(reguaContatoHistorico);
            
            return reguaContatoHistorico;
        }
    }
}
