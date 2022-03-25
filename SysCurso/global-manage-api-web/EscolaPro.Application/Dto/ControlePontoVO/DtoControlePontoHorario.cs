using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Core.Model.Funcionario;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EscolaPro.Service.Dto.ControlePontoVO
{
    public class DtoControlePontoHorario
    {
        public int Id { get; set; }
        public DateTime? Entrada1 { get; set; }
        public DateTime? Saida1 { get; set; }

        public DateTime? Entrada2 { get; set; }
        public DateTime? Saida2 { get; set; }

        public DateTime? Entrada3 { get; set; }
        public DateTime? Saida3 { get; set; }

        public DateTime? Entrada4 { get; set; }
        public DateTime? Saida4 { get; set; }

        public TipoOcorrenciaPontoEnum? TipoOcorrenciaPonto { get; set; }
        public string? Observacao { get; set; }
        public ICollection<DtoAnexo?> Anexo { get; set; }
        public int? UsuarioId { get; set; }
        public int? FuncionarioId { get; set; }
        public int? FeriasId { get; set; }
        public bool ApenasFerias { get; set; }
        public RegimeContratacaoEnum? RegimeContratacao { get; set; }
        public string? NumeroPIS { get; set; }
        public string DataCadastrado { get; set; }
        public bool Pago { get; set; }
        public string SaldoDevedor
        {
            get
            {
                TimeSpan saldoDevedor = new TimeSpan(0, 0, 0);

                TimeSpan entra1 = saldoDevedor;
                TimeSpan saida1 = saldoDevedor;

                TimeSpan entra2 = saldoDevedor;
                TimeSpan saida2 = saldoDevedor;

                TimeSpan entra3 = saldoDevedor;
                TimeSpan saida3 = saldoDevedor;

                TimeSpan entra4 = saldoDevedor;
                TimeSpan saida4 = saldoDevedor;


                if (Entrada1.HasValue)
                    entra1 = new TimeSpan(Entrada1.Value.Hour, Entrada1.Value.Minute, Entrada1.Value.Second);

                if (Saida1.HasValue)
                    saida1 = new TimeSpan(Saida1.Value.Hour, Saida1.Value.Minute, Saida1.Value.Second);

                if (Entrada2.HasValue)
                    entra2 = new TimeSpan(Entrada2.Value.Hour, Entrada2.Value.Minute, Entrada2.Value.Second);

                if (Saida2.HasValue)
                    saida2 = new TimeSpan(Saida2.Value.Hour, Saida2.Value.Minute, Saida2.Value.Second);

                if (Entrada3.HasValue)
                    entra3 = new TimeSpan(Entrada3.Value.Hour, Entrada3.Value.Minute, Entrada3.Value.Second);

                if (Saida3.HasValue)
                    saida3 = new TimeSpan(Saida3.Value.Hour, Saida3.Value.Minute, Saida3.Value.Second);

                if (Entrada4.HasValue)
                    entra4 = new TimeSpan(Entrada4.Value.Hour, Entrada4.Value.Minute, Entrada4.Value.Second);

                if (Saida4.HasValue)
                    saida4 = new TimeSpan(Saida4.Value.Hour, Saida4.Value.Minute, Saida4.Value.Second);


                var soma1 = entra1.Subtract(saida1);
                var soma2 = entra2.Subtract(saida2);
                var soma3 = entra3.Subtract(saida3);
                var soma4 = entra4.Subtract(saida4);

                var resultado = soma1 + soma2 + soma3 + soma4;

                bool isSabado = false;

                TimeSpan cargaHorarioTrabalhada;

                // Verifica se a data é sabado
                if (Entrada1.HasValue)
                {
                    if (Convert.ToDateTime(Entrada1.Value.Date).DayOfWeek == DayOfWeek.Saturday)
                    {
                        isSabado = true;
                    }
                }

                switch (RegimeContratacao)
                {
                    case RegimeContratacaoEnum.CLT_SEG_SEX:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SEX:

                        cargaHorarioTrabalhada = new TimeSpan(8, 48, 0);

                        if (TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta || TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR)
                        {
                            saldoDevedor = cargaHorarioTrabalhada * -1;
                        }
                        else
                        {
                            if (Entrada1.Value.DayOfWeek != DayOfWeek.Sunday && Entrada1.Value.DayOfWeek != DayOfWeek.Saturday)
                            {
                                if (cargaHorarioTrabalhada > resultado)
                                {
                                    saldoDevedor = cargaHorarioTrabalhada.Add(resultado);
                                }
                                else
                                {
                                    saldoDevedor = cargaHorarioTrabalhada.Subtract(resultado);
                                }
                            }
                            else
                            {
                                saldoDevedor = resultado;
                            }
                        }
                        break;
                    case RegimeContratacaoEnum.CLT_SEG_SAB:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_CLT_SEG_SAB:

                        if (isSabado)
                        {
                            cargaHorarioTrabalhada = new TimeSpan(5, 0, 0);
                        }
                        else
                        {
                            cargaHorarioTrabalhada = new TimeSpan(7, 48, 0);
                        }

                        if (TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta || TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR)
                        {
                            saldoDevedor = cargaHorarioTrabalhada * -1;
                        }
                        else
                        {
                            if (cargaHorarioTrabalhada > resultado)
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Add(resultado);
                            }
                            else
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Subtract(resultado);
                            }
                        }
                        break;
                    case RegimeContratacaoEnum.ESTAGIO_SEG_SEX:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SEX:
                        cargaHorarioTrabalhada = new TimeSpan(6, 0, 0);

                        if (TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta || TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR)
                        {
                            saldoDevedor = cargaHorarioTrabalhada * -1;
                        }
                        else
                        {
                            if (cargaHorarioTrabalhada > resultado)
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Add(resultado);
                            }
                            else
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Subtract(resultado);
                            }
                        }
                        break;
                    case RegimeContratacaoEnum.ESTAGIO_SEG_SAB:
                    case RegimeContratacaoEnum.AUTONOMO_PRE_ESTAGIO_SEG_SAB:

                        cargaHorarioTrabalhada = new TimeSpan(5, 0, 0);

                        if (TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta || TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR)
                        {
                            saldoDevedor = cargaHorarioTrabalhada * -1;
                        }
                        else
                        {
                            if (cargaHorarioTrabalhada > resultado)
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Add(resultado);
                            }
                            else
                            {
                                saldoDevedor = cargaHorarioTrabalhada.Subtract(resultado);
                            }
                        }
                        break;
                    default:
                        break;
                }

                TimeSpan saldoFinal = new TimeSpan();

                if (TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Falta || TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.DSR)
                {
                    saldoFinal = saldoDevedor;
                }
                else
                {
                    saldoFinal = saldoDevedor * -1;
                }

                string retorno = string.Empty;

                if (saldoFinal != TimeSpan.Zero)
                {
                    retorno = ((saldoFinal < TimeSpan.Zero) ? "-" : "") + saldoFinal.ToString(@"hh\:mm");
                }

                if (TipoOcorrenciaPonto.HasValue)
                {
                    if(TipoOcorrenciaPonto == TipoOcorrenciaPontoEnum.Ferias)
                    {
                        retorno = "Período de férias";
                    }
                }

                if(retorno == "-00:00")
                {
                    retorno = "";
                }

                return retorno;
            }
        }

    }
}
