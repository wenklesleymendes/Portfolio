using ModelPrincipal.Entidades;
using Repositorio.Persistencia.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Processos.Operadores
{
    public class ProcessoOperadores
    {
        private readonly IRepositorioOperadores Repositorio = new RepositorioOperadores();

        public ProcessoOperadores()
        {
        }

        public void CadastreNovoOperador(Operador operador)
        {
            if (ValideOperador(operador))
            {
                Repositorio.CrieOuAtualize(operador);
            }
            else
            {
                throw new Exception("Já existe operador com este CPF");
            }

        }

        private bool ValideOperador(Operador operador)
        {

            return true;
        }

        public List<Operador> ConsulteTodosOperadores()
        {
            return Repositorio.GetAll().ToList();
        }

        public void AtualizaOperador(Operador operador)
        {
            Repositorio.Atualize(operador);
        }

        public void ExcluaOperador(int Codigo)
        {
            Repositorio.Remova(Codigo);
        }

        public string CriptografeSenha(string senha)
        {
            using (SHA512 criptografia = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                var hash = criptografia.ComputeHash(bytes);

                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}
