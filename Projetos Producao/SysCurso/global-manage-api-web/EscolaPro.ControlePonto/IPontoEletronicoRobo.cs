using EscolaPro.Core.Model.ControlePontoEletronico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.ControlePonto
{
    public interface IPontoEletronicoRobo
    {
        Task<IEnumerable<PontoEletronico>> MontarArquivo(string path, string nomeArquivo);
    }
}
