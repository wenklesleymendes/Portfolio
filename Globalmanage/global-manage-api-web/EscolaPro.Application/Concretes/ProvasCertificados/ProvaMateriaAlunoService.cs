using AutoMapper;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto.MatriculaAlunoVO;
using EscolaPro.Service.Interfaces.ProvasCertificados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EscolaPro.Service.Concretes.ProvasCertificados
{
    public class ProvaMateriaAlunoService : IProvaMateriaAlunoService
    {

        private readonly IProvaMateriaAlunoRepository _provaMateriaAlunoRepository;
        private readonly IMapper _mapper;

        public ProvaMateriaAlunoService(IProvaMateriaAlunoRepository provaMateriaAlunoRepository,
                                        IMapper mapper)
        {
            _provaMateriaAlunoRepository = provaMateriaAlunoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoProvaMateriaAluno>> BuscarPorProvaId(int provaAlunoId)
        {

            IEnumerable<ProvaMateriaAluno> materias = await _provaMateriaAlunoRepository.BuscarPorProvaId(provaAlunoId);
            if (materias?.Count() > 0)
                return _mapper.Map<IEnumerable<DtoProvaMateriaAluno>>(materias);
            else
                return null;
        }

        public async Task<int> ExcluirProvaMateria(int provaAlunoId)
        {
            try
            {
                var materias = await _provaMateriaAlunoRepository.BuscarPorProvaId(provaAlunoId);

                if (materias?.Count() > 0)
                    return await _provaMateriaAlunoRepository.RemoveRangeAsync(materias);
                else
                    return 0;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<DtoProvaMateriaAluno> Inserir(DtoProvaMateriaAluno dtoProvaMateriaAluno)
        {
            var retorno = await _provaMateriaAlunoRepository.AddAsync(_mapper.Map<ProvaMateriaAluno>(dtoProvaMateriaAluno));
            return _mapper.Map<DtoProvaMateriaAluno>(retorno);

        }

        public async Task<int> Inserir(IEnumerable<DtoProvaMateriaAluno> dtoProvaMateriaAlunos)
        {
            try
            {
                int retorno;
                if (dtoProvaMateriaAlunos.FirstOrDefault().Id == 0)
                    retorno = await _provaMateriaAlunoRepository.AddRangeAsync(_mapper.Map<IEnumerable<ProvaMateriaAluno>>(dtoProvaMateriaAlunos));
                else
                    retorno = await _provaMateriaAlunoRepository.UpdateRangeAsync(_mapper.Map<IEnumerable<ProvaMateriaAluno>>(dtoProvaMateriaAlunos));

                return retorno;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
