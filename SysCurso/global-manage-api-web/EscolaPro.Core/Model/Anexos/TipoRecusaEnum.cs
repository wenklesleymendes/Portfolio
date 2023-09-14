using System;
using System.Collections.Generic;
using System.Text;

namespace EscolaPro.Core.Model.Anexos
{
    public enum TipoRecusaEnum
    {
        //        R.G. não atualizado. Providenciar R.G atualizado com menos de 10 anos de emissão. 
        RGNaoAtualizado = 1,

        //Documento não legível ou incompleto. Providenciar documento que esteja legível, completo, frente e verso sem cortes. 
        DocumentoNaoLegivelOuIncompleto = 2,

        //Histórico ou certificado de conclusão do ensino fundamental falta a publicação em Diário Oficial ou publicação no sistema GDAE/SED. 
        HistoricoOuCertificadoConclusao = 3,

        //Histórico ou certificado de conclusão do ensino fundamental falta autenticação em cartório. Providenciar autenticação do documento em cartório. 
        HistoricoOuCertificadoFaltaAtenticacaoCartorio = 4,

        //O comprovante de endereço apresentado não possui o CEP. Providenciar comprovante de endereço com CEP. 
        ComprovanteEnderecoNaoPossuiCEP = 5,

        //Documentos apresentados possuem divergência nas informações. 
        DocumentosPossuemDivergencia = 6,

        //Documento apresentado não aceito para comprovação de alfabetização. Providenciar documento que seja aceito para comprovação de alfabetização. 
        DocumentoApresentadoNaoAceito = 7
    }
}
