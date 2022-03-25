using EscolaPro.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaPro.API.Dto.Usuario
{
    public class DtoInserirUsuarioRequest
    {
        private string password;
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
                }
                return Criptografia.CreateMD5(password);
            }
            set
            {
                password = value;
            }
        }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
    }
}
