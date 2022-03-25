using EMCatraca.Core.Dominio;
using System.ComponentModel;

namespace EMCatraca.MonitorAcesso
{
    public class CatracaModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private EventoCatraca _ultimoEvento;
        private Dispositivo _catraca;

        public CatracaModelView(Dispositivo catraca)
        {
            _catraca = catraca;
        }

        public int CodigoCatraca => _catraca.Codigo;
        public string DescricaoCatraca => _catraca.Descricao;

        public string NomePessoa { get; private set; }
        public string Mensagem1Evento { get; private set; }
        public string Mensagem2Evento { get; private set; }
        public int Codigo { get; private set; }
        public StatusLiberacaoCatraca Resultado { get; private set; }
        public byte[] FotoEvento { get; internal set; }

        public void DispareEvento(EventoCatraca evento)
        {
            _ultimoEvento = evento;
            Codigo = _catraca.Codigo;
            NomePessoa = evento.Pessoa?.Nome;
            Mensagem1Evento = evento.Mensagem1;
            Mensagem2Evento = evento?.Mensagem2;
            Resultado = evento.Liberacao;
            FotoEvento = evento.Pessoa?.Foto;
            PropertyChanged?.Invoke(this, null);
        }
    }
}
