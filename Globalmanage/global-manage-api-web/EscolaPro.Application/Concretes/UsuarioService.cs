using AutoMapper;
using EscolaPro.Core.Extensions;
using EscolaPro.Core.Model;
using EscolaPro.Core.Model.DadosFuncionario;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Interfaces.MatriculaAlunos;
using EscolaPro.Service.Dto;
using EscolaPro.Service.Dto.AlunosVO;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.FuncionarioVO;
using EscolaPro.Service.Dto.UnidadeVO;
using EscolaPro.Service.Dto.UsuarioVO;
using EscolaPro.Service.Interfaces;
using EscolaPro.Service.Interfaces.MatriculaAlunos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EscolaPro.Service.Concretes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsuarioService(
            IUsuarioRepository repository,
            ICentroCustoRepository centroCustoRepository,
            IUnidadeRepository unidadeRepository,
            IEnderecoRepository enderecoRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository,
            IFuncionarioRepository funcionarioRepository,
            IAlunoRepository alunoRepository,
            IEmailSenderService emailSenderService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _repository = repository;
            _centroCustoRepository = centroCustoRepository;
            _enderecoRepository = enderecoRepository;
            _unidadeRepository = unidadeRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _funcionarioRepository = funcionarioRepository;
            _emailSenderService = emailSenderService;
            _alunoRepository = alunoRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<DtoUsuario> Login(string email, string senha)
        {
            var usuario = await _repository.Login(email, senha);

            if (usuario != null)
            {
                if (!usuario.IsPrimeiroAcesso)
                {
                    usuario.IsPrimeiroAcesso = true;

                    await _repository.UpdateAsync(usuario);
                }

                var usuarioRetorno = await BuscarPorId(usuario.Id);

                DtoUsuario dtoUsuario = _mapper.Map<DtoUsuario>(usuarioRetorno);
                if (!dtoUsuario.IsAluno)
                    dtoUsuario.PerfilUsuario.BaixaManual = _appSettings.UsuariosBaixaManual.Contains(usuario.Id);

                return dtoUsuario;
            }
            else
            {
                return new DtoUsuario { };
            }
        }

        public async Task<DtoUsuario> Inserir(DtoUsuarioRequest model, bool aluno = false)
        {
            try
            {
                if (aluno)
                {
                    var alunoRetorno = await _alunoRepository.BuscarPorCPF(model.CPF);

                    Usuario usuarioAluno = _mapper.Map<Usuario>(model);

                    usuarioAluno.AlunoId = alunoRetorno.Id;
                    usuarioAluno.IsAluno = true;
                    usuarioAluno.UnidadeId = alunoRetorno.UnidadeId;
                    usuarioAluno.Password = Criptografia.CreateMD5(model.Password);

                    var usuario = await _repository.AddAsync(usuarioAluno);

                    return _mapper.Map<DtoUsuario>(usuario);
                }
                else
                {
                    return await CriarUsuarioSistema(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoUsuario>> BuscarTodos(int idUsuarioLogado)
        {
            try
            {
                var usuarioLista = await _repository.BuscarTodos(idUsuarioLogado);

                return _mapper.Map<IEnumerable<DtoUsuario>>(usuarioLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoUsuario> DesativarOuAtivar(int idUsuario)
        {
            var usuario = await _repository.GetByIdAsync(idUsuario);

            usuario.IsActive = usuario.IsActive = usuario.IsActive ? usuario.IsActive = false : usuario.IsActive = true;

            await _repository.UpdateAsync(usuario);

            return _mapper.Map<DtoUsuario>(usuario);
        }

        public async Task<bool> Excluir(int idPerfil)
        {
            var perfil = await _repository.GetByIdAsync(idPerfil);
            perfil.IsDelete = true;
            var id = await _repository.UpdateAsync(perfil);
            return id > 0 ? true : false;
        }

        public async Task<DtoUsuario> BuscarPorId(int idUsuario)
        {
            var usuario = await _repository.BuscarPorId(idUsuario);

            usuario.Password = string.Empty;

            var usuarioRetorno = _mapper.Map<DtoUsuario>(usuario);

            if (usuario.UserName != "admin")
            {
                if (usuario.FuncionarioId.HasValue)
                {
                    var funcionario = await _funcionarioRepository.BuscarPorId(usuario.FuncionarioId.Value);

                    usuarioRetorno.Funcionario = _mapper.Map<DtoFuncionarioResponse>(funcionario);

                    usuarioRetorno.Email = funcionario.Contato.Email;
                    usuarioRetorno.Celular = funcionario.Contato.Celular;
                }
            }

            if (usuario.IsAluno)
            {
                var aluno = await _alunoRepository.BuscarPorId(usuario.AlunoId.Value);

                usuarioRetorno.Aluno = _mapper.Map<DtoAluno>(aluno);

                usuarioRetorno.Funcionario = new DtoFuncionarioResponse();
                usuarioRetorno.Funcionario.Contato = _mapper.Map<DtoContatoSimples>(aluno.Contato);
                usuarioRetorno.Funcionario.CPF = aluno.CPF;
                usuarioRetorno.Funcionario.RG = aluno.RG;

                usuarioRetorno.Celular = aluno.Contato.Celular;
                usuarioRetorno.Email = aluno.Contato.Email;
            }

            return usuarioRetorno;
        }

        public string GerarUserName(Funcionario funcionario)
        {
            string[] nomePessoa = Helpers.CoreHelpers.Abreviatte(funcionario.Nome);



            string cpf = funcionario.CPF.Substring(0, 4);

            string userName = $"{nomePessoa.FirstOrDefault().ToLower()}.{nomePessoa.LastOrDefault().ToLower()}.{cpf}";

            return userName;
        }

        public async Task<IEnumerable<DtoUsuario>> FiltrarUsuario(DtoFiltrarUsuario dtoFiltrarUsuario)
        {
            try
            {
                var usuarioRetorno = await _repository.FiltrarUsuario(
                    dtoFiltrarUsuario.FuncionarioId,
                    dtoFiltrarUsuario.CentroCustoId,
                    dtoFiltrarUsuario.UnidadeId);

                List<DtoUsuario> usuariosLista = new List<DtoUsuario>();

                foreach (var item in usuarioRetorno)
                {
                    var usuario = await BuscarPorId(item.Id);

                    usuario.Password = string.Empty;

                    usuariosLista.Add(usuario);
                }

                return dtoFiltrarUsuario.EhAtendimento ? usuariosLista.Where(p => p.IsAluno == false) : usuariosLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoUsuario>> BuscarUsuarioPorUnidade(DtoFiltrarUsuario dtoFiltrarUsuario)
        {
            try
            {
                var usuarioRetorno = await _repository.BuscaUsuarioPorUnidade(
                    dtoFiltrarUsuario.FuncionarioId,
                    dtoFiltrarUsuario.CentroCustoId,
                    dtoFiltrarUsuario.UnidadeId);

                List<DtoUsuario> usuariosLista = new List<DtoUsuario>();

                foreach (var item in usuarioRetorno)
                {
                    var usuario = _mapper.Map<DtoUsuario>(item);

                    usuario.Password = string.Empty;

                    usuariosLista.Add(usuario);
                }

                return dtoFiltrarUsuario.EhAtendimento ? usuariosLista.Where(p => p.IsAluno == false) : usuariosLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoUsuario> FiltrarUsuario(string userName)
        {
            try
            {
                var usuarioRetorno = await _repository.BuscarUsuarioPorNome(userName);

                return _mapper.Map<DtoUsuario>(usuarioRetorno); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoUsuario> CriarUsuarioSistema(DtoUsuarioRequest model)
        {
            try
            {
                var retornoFuncionario = await _funcionarioRepository.BuscarPorCPF(model.CPF);

                var funcionario = await _funcionarioRepository.BuscarPorId(retornoFuncionario.Id);

                var perfilSelecionado = await _perfilUsuarioRepository.BuscarPorTipo(model.PerfilUsuarioId.Value);

                model.PerfilUsuarioId = perfilSelecionado.Id;

                if (model.Id == 0)
                {
                    string userName = GerarUserName(funcionario);

                    model.UserName = userName;

                    model.FuncionarioId = funcionario.Id;

                    var validar = await _repository.BuscarUsuarioPorNome(userName);

                    if (validar != null)
                    {
                        throw new Exception("Usuário já existente");
                    }

                    var retorno = await _repository.AddAsync(_mapper.Map<Usuario>(model));

                    return _mapper.Map<DtoUsuario>(retorno);
                }
                else
                {
                    var usuarioOld = await _repository.BuscarPorId(model.Id.Value);

                    if (string.IsNullOrEmpty(model.Password))
                    {
                        model.Password = usuarioOld.Password;
                    }

                    model.UserName = GerarUserName(funcionario);

                    model.FuncionarioId = funcionario.Id;

                    await _repository.UpdateAsync(_mapper.Map<Usuario>(model));

                    var retorno = await _repository.GetByIdAsync(model.Id);

                    return _mapper.Map<DtoUsuario>(retorno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DtoUsuario> BuscarPorAlunoId(int alunoId)
        {
            try
            {
                var usuario = await _repository.BuscarPorAlunoId(alunoId);

                return _mapper.Map<DtoUsuario>(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EsqueciMinhaSenha(string email)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DtoUsuario>> BuscarUsuarioAtendente()
        {
            try
            {
                var usuarioLista = await _repository.BuscarUsuarioAtendente();

                return _mapper.Map<IEnumerable<DtoUsuario>>(usuarioLista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUsuarioAdmin(int perfilId)
        {
            try
            {
                return _repository.CheckUsuarioAdmin(perfilId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<DtoUsuario> CriarUsuarioAdmin(DtoUsuarioRequest model)
        {
            try
            {
                var retorno = await _repository.AddAsync(_mapper.Map<Usuario>(model));

                return _mapper.Map<DtoUsuario>(retorno);
            }
            catch
            {
                throw;
            }
        }


    }
}
