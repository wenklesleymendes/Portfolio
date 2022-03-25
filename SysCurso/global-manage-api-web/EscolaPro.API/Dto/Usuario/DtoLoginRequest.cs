using EscolaPro.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Usuario
{
    public class DtoLoginRequest
    {
        public string UserName { get; set; }
        private string senha;
        public string Senha
        {
            get
            {
                if (string.IsNullOrEmpty(senha))
                {
                    return "";
                }
                return Criptografia.CreateMD5(senha);
            }
            set
            {
                senha = value;
            }
        }
    }
}
