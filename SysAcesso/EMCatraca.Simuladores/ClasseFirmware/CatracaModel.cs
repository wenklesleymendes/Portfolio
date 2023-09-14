using static EMCatraca.Simuladores.ClasseFirmware.Enumeradores;

namespace EMCatraca.Simuladores.ClasseFirmware
{
    public class CatracaModel
    {
        private EnumeradorEstadoDaCatraca _estado;
        private string _dados;
        private CatracaEventos eventos = new CatracaEventos();
        public CatracaEventos Eventos { get { return eventos; } }


        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Ip { get; set; }
        public string Porta { get; set; }
        public bool EhParaGravarLog { get; set; }
        public EnumeradorEstadoDaCatraca EstadoCatraca
        {
            get { return _estado; }
            set
            {
                _estado = value;
                eventos.AtualizaStatus(0);
            }
        }

        public string Dados
        {
            get { return _dados; }
            set
            {
                _dados = value;
                eventos.AtualizaDadosRecebidos(0);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is CatracaModel catraca &&
                   Codigo == catraca.Codigo;
        }

        public override int GetHashCode()
        {
            return 1745598366 + Codigo.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Codigo}, {Descricao}, {Ip}, {Porta}, {EhParaGravarLog}";
        }
    }
}
