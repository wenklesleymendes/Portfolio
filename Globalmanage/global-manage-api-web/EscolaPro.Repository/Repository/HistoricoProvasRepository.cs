using EscolaPro.Core.Model;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace EscolaPro.Repository.Repository
{
    public class HistoricoProvasRepository : DomainRepository<HistoricoProvas>, IHistoricoProvasRepository
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IProvaAlunoRepository _provaAlunoRepository;
        private readonly IMapper _mapper;

        public HistoricoProvasRepository(ApplicationContext dbContext, 
                                         IAlunoRepository alunoRepository,
                                         IMapper mapper,
                                         IProvaAlunoRepository provaAlunoRepository) 
                                         : base(dbContext)
        {
            _alunoRepository = alunoRepository;
            _provaAlunoRepository = provaAlunoRepository;
        }

        public async Task<List<HistoricoProvas>> ListaColegioAutorizadoExcel()
        {
            try
            {
                //var alunos = await _alunoRepository.GetAllAsync();

                var provaAluno = await _provaAlunoRepository.BuscarTodas();

                //IQueryable<HistoricoProvas> query = await Task.FromResult(GenerateQuery(null));

                List<HistoricoProvas> _historicoProvas = new List<HistoricoProvas>();

                //foreach (var item in provaAluno)
                //{
                //    var alunoMatricula = _alunoRepository.BuscarPorMatriculaId(item.MatriculaAlunoId);
                //    //var aluno = alunos.Where(x => x.Matriculas.Contains(x => x. item.MatriculaAlunoId))

                //    var dadosAluno = _alunoRepository.BuscarPorId(alunoMatricula.Id);

                //    var dadosAlunoMap = _mapper.Map<DtoAluno>(dadosAluno);


                //    string curso = "";
                //    if(dadosAlunoMap.Matriculas.FirstOrDefault().Curso.Descricao == "Ensino Fundamental")
                //    {
                //        curso = "000001-EJA ENSINO FUNDAMENTAL";
                //    }
                //    else if (dadosAlunoMap.Matriculas.FirstOrDefault().Curso.Descricao == "Alfabetização, Ensino Fundamental e Médio" ||
                //             dadosAlunoMap.Matriculas.FirstOrDefault().Curso.Descricao == "Ensino Fundamental e Médio" ||
                //             dadosAlunoMap.Matriculas.FirstOrDefault().Curso.Descricao == "Ensino Médio")
                //    {
                //        curso = "000002-EJA ENSINO MÉDIO";
                //    }
                //    else
                //    {
                //        curso = dadosAlunoMap.Matriculas.FirstOrDefault().Curso.Descricao;
                //    }

                //    HistoricoProvas historicoProvas = new HistoricoProvas
                //    {
                //        parceiro = "00032-André Coutinho",
                //        nomecompleto = dadosAlunoMap.Nome,
                //        datadenascimento = dadosAlunoMap.DataNascimento.ToString(),
                //        sexo = dadosAlunoMap.Sexo.ToString(),
                //        estadocivil = dadosAlunoMap.EstadoCivil.ToString(),
                //        naturalidade = dadosAlunoMap.Naturalidade.Descricao,
                //        nacionalidade = dadosAlunoMap.Naturalidade.Descricao,
                //        nomedamae = dadosAlunoMap.NomeMae,
                //        nomedopai = "",
                //        email = "inedsuporte@gmail.com",
                //        rg = dadosAlunoMap.RG,
                //        orgaoexpedidor = dadosAlunoMap.OrgaoExpedicao,
                //        ufrg = "",
                //        cpf = dadosAlunoMap.CPF,
                //        titulodeeleitor = dadosAlunoMap.TituloEleitoral,
                //        secaoeleitoral = dadosAlunoMap.Secao,
                //        enderecoresidencial = dadosAlunoMap.Endereco.Rua,
                //        numeroresidencial = dadosAlunoMap.Endereco.Numero,
                //        complementoresidencial = dadosAlunoMap.Endereco.Complemento,
                //        bairroresidencial = dadosAlunoMap.Endereco.Bairro,
                //        cidaderesidencial = dadosAlunoMap.Endereco.Cidade,
                //        ufresidencial = dadosAlunoMap.Endereco.Estado,
                //        cepresidencial = dadosAlunoMap.Endereco.CEP,
                //        //dddfixoresidencial = dadosAlunoMap.Contato.TelefoneFixo,
                //        telefonefixoresidencial = dadosAlunoMap.Contato.TelefoneFixo,
                //        //dddcelularresidencial = dadosAlunoMap.Contato.Celular,
                //        celularresidencial = dadosAlunoMap.Contato.Celular,
                //        curso = curso,
                //    };

                //    _historicoProvas.Add(historicoProvas);
                //}

                return _historicoProvas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
