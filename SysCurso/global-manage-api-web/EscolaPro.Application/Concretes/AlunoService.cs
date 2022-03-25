using AutoMapper;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.CadastroAluno;
using EscolaPro.Core.Model.PainelMatricula;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.DisparoSmsVO;
using EscolaPro.Service.Dto.FornecedorVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;
        private readonly IDisparoSmsService _disparoSmsService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IContatoRepository _contatoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMatriculaAlunoRepository _matriculaAlunoRepository;
        private readonly IMapper _mapper;
        private readonly IUnidadeRepository _unidadeRepository;

        public AlunoService(
            IAlunoRepository repository,
            IDisparoSmsService disparoSmsService,
            IEmailSenderService emailSenderService,
            IContatoRepository contatoRepository,
            IEnderecoRepository enderecoRepository,
            IUsuarioService usuarioService,
            IMatriculaAlunoRepository matriculaAlunoRepository,
            IMapper mapper,
            IUnidadeRepository unidadeRepository)
        {
            _repository = repository;
            _disparoSmsService = disparoSmsService;
            _emailSenderService = emailSenderService;
            _contatoRepository = contatoRepository;
            _enderecoRepository = enderecoRepository;
            _matriculaAlunoRepository = matriculaAlunoRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _unidadeRepository = unidadeRepository;
        }

        public async Task<DtoAluno> BuscarPorCPF(string cpf)
        {
            var aluno = await _repository.BuscarPorCPF(cpf);

            return _mapper.Map<DtoAluno>(aluno);
        }

        public async Task<DtoAluno> BuscarPorId(int idAluno)
        {
            var aluno = await _repository.BuscarPorId(idAluno);

            return _mapper.Map<DtoAluno>(aluno);
        }

        public async Task<IEnumerable<DtoAluno>> BuscarTodos()
        {
            var alunos = await _repository.GetAllAsync();

            List<DtoAluno> dtoAlunos = new List<DtoAluno>();

            foreach (var item in alunos.Where(x => !x.IsDelete))
            {
                var aluno = await _repository.BuscarPorId(item.Id);

                dtoAlunos.Add(_mapper.Map<DtoAluno>(aluno));
            }

            return dtoAlunos;
        }

        public async Task<bool> Excluir(int idAluno)
        {
            var aluno = await _repository.GetByIdAsync(idAluno);
            aluno.IsDelete = true;
            await _repository.UpdateAsync(aluno);
            return aluno.IsDelete;
        }

        public async Task<bool> ExcluirFoto(int alunoId)
        {
            try
            {
                var aluno = await _repository.BuscarPorId(alunoId);
                aluno.Foto = null;
                var sucesso = await _repository.UpdateAsync(aluno);
                return sucesso > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DtoAluno>> FiltrarAluno(DtoFiltrarAluno filtrarAluno)
        {
            try
            {
                var usuarioLogado = await _usuarioService.BuscarPorId(filtrarAluno.UsuarioId);

                var filtrar = _mapper.Map<FiltrarAluno>(filtrarAluno);

                if (!(usuarioLogado.PerfilUsuario?.VerTodasUnidades == true))
                {
                    filtrar.UnidadeId = usuarioLogado.Unidade.Id;
                }

                filtrar.VerTodosUnidades = usuarioLogado.PerfilUsuario.VerTodasUnidades;

                var alunos = await _repository.FiltrarAluno(filtrar);

                List<DtoAluno> alunoLista = new List<DtoAluno>();

                foreach (var item in alunos)
                {                    
                    var nomeUnidade = string.Empty;
                    var matriculasDistinct = item.Matriculas.DistinctBy(x=>x.UnidadeId);

                    if (filtrar.VerTodosUnidades)
                    {
                        if (filtrar.UnidadeId != null)
                        {
                            var unidadeFiltro = await _unidadeRepository.GetByIdAsync(filtrar.UnidadeId);
                            if (unidadeFiltro != null)
                                item.Unidade.Nome = unidadeFiltro.Nome;
                        }
                        else
                        {
                            foreach (var itemMatriculas in matriculasDistinct)
                            {
                                if (itemMatriculas.Unidade != null)
                                {
                                    var unidadeNome = itemMatriculas.Unidade.Nome;
                                    if (!string.IsNullOrEmpty(nomeUnidade))
                                        nomeUnidade = string.Concat(nomeUnidade, "\n", unidadeNome);
                                    else
                                        nomeUnidade = unidadeNome;
                                }
                            }
                            if (item.Unidade != null)
                                item.Unidade.Nome = nomeUnidade;
                        }
                    }
                    else
                    {
                        if(usuarioLogado.Unidade != null)
                            item.Unidade.Nome = usuarioLogado.Unidade.Nome;
                    }
                }

                if (filtrar.StatusDocumento.HasValue)
                {
                    foreach (var item in alunos)
                    {
                        var matriculasLista = await _matriculaAlunoRepository.BuscarMinhasMatriculas(item.Id, filtrarAluno.UsuarioId, true);

                        bool existeDocumentosPendentes = false;

                        foreach (var matricula in matriculasLista)
                        {
                            var documentosPendentes = await _matriculaAlunoRepository.ConsultarDocumentosPendentes(matricula);

                            existeDocumentosPendentes = documentosPendentes.DocumentosPendentes.Count() > 0 ? true : false;
                        }

                        if (filtrar.StatusDocumento.Value)
                        {
                            if (existeDocumentosPendentes)
                            {
                                alunoLista.Add(_mapper.Map<DtoAluno>(item));
                            }
                        }
                        else
                        {
                            if (!existeDocumentosPendentes)
                            {
                                alunoLista.Add(_mapper.Map<DtoAluno>(item));
                            }
                        }
                    }
                }
                else
                {
                    alunoLista.AddRange(_mapper.Map<List<DtoAluno>>(alunos));
                }

                //if (filtrarAluno.UnidadeId.HasValue)
                //{
                //    alunoLista = alunoLista.Where(x => x.UnidadeId == filtrarAluno.UnidadeId.Value).ToList();
                //}

                alunoLista = alunoLista.DistinctBy(x => x.Id).ToList();

                return alunoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAluno> Inserir(DtoAluno dtoAluno)
        {
            if (dtoAluno.Id == 0)
            {
                var aluno = await _repository.AddAsync(_mapper.Map<Aluno>(dtoAluno));

                try
                {
                    // if (aluno.Contato.RecebeSMS)
                    // {
                    //     await _disparoSmsService.Enviar(new SmsBody
                    //     {
                    //         mensagem = "Seu cadastro foi realizado com sucesso!",
                    //         numero = aluno.Contato.Celular
                    //     });
                    // }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (aluno.Contato.ReceberEmail)
                {
                    //await _emailSenderService.SendEmailAsync(aluno.Contato.Email, "Boas vindas", "Cadastro realizado com sucesso");
                }

                return _mapper.Map<DtoAluno>(aluno);
            }
            else
            {
                if (dtoAluno.Contato != null)
                {
                    dtoAluno.ContatoId = dtoAluno.Contato.Id;
                }

                if (dtoAluno.Endereco != null)
                {
                    dtoAluno.EnderecoId = dtoAluno.Endereco.Id;
                }

                var fotoAluno = await SelecionarFoto(dtoAluno.Id);

                var alunoAlterado = _mapper.Map<Aluno>(dtoAluno);

                if (fotoAluno != null)
                {
                    alunoAlterado.Foto = fotoAluno.Foto;
                    alunoAlterado.Extensao = fotoAluno.Extensao;
                }

                if (alunoAlterado.NaturalidadeId == 0)
                    alunoAlterado.NaturalidadeId = null;

                await _repository.UpdateAsync(alunoAlterado);

                await _contatoRepository.UpdateAsync(alunoAlterado.Contato);

                await _enderecoRepository.UpdateAsync(alunoAlterado.Endereco);

                return await BuscarPorId(dtoAluno.Id);
            }
        }

        public async Task<DtoAlunoFoto> SelecionarFoto(int alunoId)
        {
            try
            {
                var aluno = await _repository.SelecionarFoto(alunoId);

                return new DtoAlunoFoto { AlunoId = aluno.Id, Extensao = aluno.Extensao, Foto = aluno.Foto };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoAlunoFoto> UploadFoto(byte[] file, int alunoId, string extensao)
        {
            try
            {
                await _repository.UploadFoto(file, alunoId, extensao);

                return await SelecionarFoto(alunoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
