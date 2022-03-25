using System;

namespace EMCatraca.Core.Dominio
{
    public class Auditoria
    {
        public Auditoria(FuncaoAcesso funcao, string operacao, string log)
        {
            Id = Guid.NewGuid().ToString("N");
            Data = DateTime.Now.Date;
            Hora = DateTime.Now.TimeOfDay;
            Operador = SessaoDoUsuario.Instancia.OperadorLogado ?? new Operador { Codigo = 1, EhAdministrador = true, Nome = "Admin" };
            Funcao = funcao;
            Operacao = operacao;
            Log = log;
        }

        public Auditoria() { }

        public string Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public Operador Operador { get; set; }
        public FuncaoAcesso Funcao { get; set; }
        public string Operacao { get; set; }
        public string Log { get; set; }
    }
}
