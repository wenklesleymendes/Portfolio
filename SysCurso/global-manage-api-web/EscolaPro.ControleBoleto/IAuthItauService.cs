using EscolaPro.ControleBoleto.ItauResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.ControleBoleto
{
    public interface IAuthItauService
    {
        Task<AuthTokenItau> AutenticarItau();
    }
}
