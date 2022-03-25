using System;
using System.Collections.Generic;

namespace EMCatraca.Core.Dominio
{
    [Serializable]
    public class EventoCatraca
    {
        private EventoCatraca()
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime DataHora { get; }
        public SentidoGiro Giro { get; set; }
        public StatusLiberacaoCatraca Liberacao { get; set; }
        public Pessoa Pessoa { get; set; }
        public Dispositivo Dispositivo { get; set; }
        public string Mensagem1 { get; set; }
        public string Mensagem2 { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EventoCatraca catraca && Id.Equals(catraca.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
        }

        public override string ToString()
        {
            return $"{Id} {Giro} {Pessoa}";
        }

        public static EventoCatraca CrieAcessoLiberado(SentidoGiro giro, Pessoa pessoa, Dispositivo catraca)
        {
            if (pessoa == null) throw new ArgumentNullException(nameof(pessoa));
            if (catraca == null) throw new ArgumentNullException(nameof(catraca));

            return new EventoCatraca
            {
                Giro = giro,
                Liberacao = StatusLiberacaoCatraca.Liberado,
                Pessoa = pessoa,
                Dispositivo = catraca,
                Mensagem1 = "Acesso Liberado!"
            };
        }

        public static EventoCatraca CrieAcessoLiberadoComRestricao(SentidoGiro giro, Pessoa pessoa, Dispositivo catraca, string msgRestricao)
        {
            if (pessoa == null) throw new ArgumentNullException(nameof(pessoa));
            if (catraca == null) throw new ArgumentNullException(nameof(catraca));

            return new EventoCatraca
            {
                Giro = giro,
                Liberacao = StatusLiberacaoCatraca.LiberadoComRestricoes,
                Pessoa = pessoa,
                Dispositivo = catraca,
                Mensagem1 = "Acesso Liberado!"
            };
        }

        public static EventoCatraca CrieAcessoNegado(SentidoGiro giro, Pessoa pessoa, Dispositivo catraca)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            if (catraca == null)
            {
                throw new ArgumentNullException(nameof(catraca));
            }

            return new EventoCatraca
            {
                Giro = giro,
                Liberacao = StatusLiberacaoCatraca.Negado,
                Pessoa = pessoa,
                Dispositivo = catraca,
                Mensagem1 = "Acesso Negado!"
            };
        }

        public static EventoCatraca CrieAcessoMudancaEstado(Dispositivo catraca, string mensagem1)
        {
            if (catraca == null)
            {
                throw new ArgumentNullException(nameof(catraca));
            }

            return new EventoCatraca
            {
                Giro = SentidoGiro.Indefinido,
                Liberacao = StatusLiberacaoCatraca.MudancaEstado,
                Dispositivo = catraca,
                Mensagem1 = mensagem1
            };
        }
    }
}
