using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class ParametroService : IParametroService
    {
        private readonly IParametroRepository _parametroRepository;

        public ParametroService(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        public async Task<bool> Atualizar(Parametro parametro)
        {
            var parametroAtualizar = await _parametroRepository.GetByIdAsync(parametro.Id);
            parametroAtualizar.Valor = parametro.Valor;
            int id = await _parametroRepository.UpdateAsync(parametroAtualizar);

            return id > 0 ? true : false;
        }

        public async Task<string> BuscarParametroPorChave(string chave)
        {
            return await _parametroRepository.BuscarParametroPorChave(chave);
        }

        public async Task<string> BuscarParametroPorId(int id)
        {
            return await _parametroRepository.BuscarParametroPorId(id);
        }
    }
}
