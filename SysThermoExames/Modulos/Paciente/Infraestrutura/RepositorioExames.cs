using MdPaciente.Dominio;
using Microsoft.Data.Sqlite;
using Repositorio.Conexao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MdPaciente.Infraestrutura
{
    class RepositorioExames : IRepositorioExames
    {
        public RepositorioExames()
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS PacienteExames(Id INTEGER not null primary key autoincrement, Nome VARCHAR(50), Idade INTEGER, DataNascimento VARCHAR(50), Sexo INTEGER, Data VARCHAR(50), MedicoRequisitante VARCHAR(50), TipoExame INTEGER, Medico VARCHAR(50), EmailMedico VARCHAR(50), Telefone VARCHAR(50), MotivoExame VARCHAR(50), SupeitaDiagnostico VARCHAR(50), TempoDor VARCHAR(50), MaFormacoes VARCHAR(50), Mestruacao TEXT, MestruacaoAusenteTempo TEXT, Sindromes TEXT, Enxaquecas TEXT, Disturbios TEXT, AlteracoesHormonais TEXT, ProcessosCirurgicosPrevios TEXT, ProcessosRelacionadosProtese TEXT, DadosImplantes TEXT, TipoInfiltraçao TEXT, DadosAnticoncepcional TEXT, DadosFarmaco TEXT, TiposFarmaco  TEXT)";
            cmd.ExecuteNonQuery();
        }
        public void Atualize(Exames exame)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"UPDATE PacienteExames
                                    SET Nome = $Nome,                                        
                                        Idade = $Idade,
                                        DataNascimento = $DataNascimento,
                                        Sexo = $Sexo,
                                        Data = $Data,
                                        MedicoRequsitante = $MedicoRequisitante,
                                        TipoExame = $TipoExame,
                                        Medico = $Medico,
                                        EmailMedico = $EmailMedico,
                                        Telefone = $Telefone,
                                        Motivo = $Motivo,
                                        SuspeitaDiagnostico = $SuspeitaDiagnostico,
                                        TempoDor = $TempoDor,
                                        MaFormacoes = $MaFormacoes
                                    WHERE Id = $Id;";

            cmd.Parameters.AddWithValue("$Nome", exame.Nome);
            cmd.Parameters.AddWithValue("$Idade", exame.Idade);
            cmd.Parameters.AddWithValue("$DataNascimento", exame.DataNascimento);
           // cmd.Parameters.AddWithValue("$Sexo", exame.Sexo);
           // cmd.Parameters.AddWithValue("$Data", exame.Data);
            cmd.Parameters.AddWithValue("$MedicoRequisitante", exame.MedicoRequisitante);
           // cmd.Parameters.AddWithValue("$TipoExame", exame.EnumTipoExame);
            cmd.Parameters.AddWithValue("$Medico", exame.Medico);
            cmd.Parameters.AddWithValue("$EmailMedico", exame.EmailMedico);
            cmd.Parameters.AddWithValue("$Telefone", exame.Telefone);
            cmd.Parameters.AddWithValue("$Motivo", exame.MotivoExame);
            cmd.Parameters.AddWithValue("$SuspeitaDiagnostico", exame.SuspeitaDiagnostico);
            cmd.Parameters.AddWithValue("$TempoDor", exame.TempoDor);
            cmd.Parameters.AddWithValue("$MaFormacoes", exame.MaFormacoes);
            cmd.Parameters.AddWithValue("$DorMomentoExame", exame.DorMomentoExame);
            cmd.Parameters.AddWithValue("$MenstruacaoAusente", exame.MenstruacaoAusente);
            cmd.Parameters.AddWithValue("$MenstruacaoAusenteTempo", exame.MenstruacaoAusenteTempo);
            cmd.Parameters.AddWithValue("$MenstruacaoIrregular", exame.MenstruacaoIrregular);
            cmd.Parameters.AddWithValue("$MenstruacaoDolorosa", exame.MenstruacaoDolorosa);
            cmd.Parameters.AddWithValue("$Diabetes", exame.Diabetes);
            cmd.Parameters.AddWithValue("$ArtriteReumatoide", exame.ArtriteReumatoide);
            cmd.Parameters.AddWithValue("$Raynaud", exame.Raynaud);
            cmd.Parameters.AddWithValue("$Gota", exame.Gota);
            cmd.Parameters.AddWithValue("$Lupus", exame.Lupus);
            cmd.Parameters.AddWithValue("$LitiaseBiliar", exame.LitiaseBiliar);
            cmd.Parameters.AddWithValue("$LitiaseRenal", exame.LitiaseRenal);
            cmd.Parameters.AddWithValue("$Dislipidemia", exame.Dislipidemia);
            cmd.Parameters.AddWithValue("$Sinusite", exame.Sinusite);
            cmd.Parameters.AddWithValue("$EnxaquecaFrontal", exame.EnxaquecaFrontal);
            cmd.Parameters.AddWithValue("$EnxaquecaTemporalDireita", exame.EnxaquecaTemporalDireita);
            cmd.Parameters.AddWithValue("$EnxaquecaTemporalEsquerda", exame.EnxaquecaTemporalEsquerda);
            cmd.Parameters.AddWithValue("$EnxaquecaAlternante", exame.EnxaquecaAlternante);
            cmd.Parameters.AddWithValue("$EnxaquecaNuca", exame.EnxaquecaNuca);
            cmd.Parameters.AddWithValue("$EnxaquecaTopo", exame.EnxaquecaTopo);
            cmd.Parameters.AddWithValue("$EnxaquecaTotal", exame.EnxaquecaTotal);
            cmd.Parameters.AddWithValue("$EnxaquecaTiara", exame.EnxaquecaTiara);
            cmd.Parameters.AddWithValue("$EnxaquecaPulsatil", exame.EnxaquecaPulsatil);
            cmd.Parameters.AddWithValue("$Fotofobia", exame.Fotofobia);
            cmd.Parameters.AddWithValue("$Fonofobia", exame.Fonofobia);
            cmd.Parameters.AddWithValue("$PerfumeOdores", exame.PerfumeOdores);
            cmd.Parameters.AddWithValue("$DormirMal", exame.DormirMal);
            cmd.Parameters.AddWithValue("$Fome", exame.Fome);
            cmd.Parameters.AddWithValue("$Alimentar", exame.Alimentar);
            cmd.Parameters.AddWithValue("$DescricaoAlimentar", exame.DescricaoAlimentar);
            cmd.Parameters.AddWithValue("$OvarioPolicisticos", exame.OvarioPolicisticos);
            cmd.Parameters.AddWithValue("$DisplasiaMamaria", exame.DisplasiaMamaria);
            cmd.Parameters.AddWithValue("$Endometriose", exame.Endometriose);
            cmd.Parameters.AddWithValue("$Varizes", exame.Varizes);
            cmd.Parameters.AddWithValue("$Acne", exame.Acne);
            cmd.Parameters.AddWithValue("$CPVesiculaBiliar", exame.CPVesiculaBiliar);
            cmd.Parameters.AddWithValue("$CPHisterectomia", exame.CPHisterectomia);
            cmd.Parameters.AddWithValue("$CPCornetosNasaisSinusite", exame.CPCornetosNasaisSinusite);
            cmd.Parameters.AddWithValue("$CPCancerMamaDireita", exame.CPCancerMamaDireita);
            cmd.Parameters.AddWithValue("$CPCancerMamaEsquerda", exame.CPCancerMamaEsquerda);
            cmd.Parameters.AddWithValue("$CPVarizes", exame.CPVarizes);
            cmd.Parameters.AddWithValue("$ProteseMamaria", exame.ProteseMamaria);
            cmd.Parameters.AddWithValue("$ProteseMetalOssoCorpo", exame.ProteseMetalOssoCorpo);
            cmd.Parameters.AddWithValue("$TelaMarlex", exame.TelaMarlex);
            cmd.Parameters.AddWithValue("$AmputacaoMembro", exame.AmputacaoMembro);
            cmd.Parameters.AddWithValue("$AmputacaoMembroEspecificada", exame.AmputacaoMembroEspecificada);
            cmd.Parameters.AddWithValue("$ImpAnticoncepcional", exame.ImpAnticoncepcional);
            //cmd.Parameters.AddWithValue("$DataImpAnticoncepcional", exame.DataImpAnticoncepcional.ToString());
            cmd.Parameters.AddWithValue("$DIUCobre", exame.DIUCobre);
            cmd.Parameters.AddWithValue("$DIUMirena", exame.DIUMirena);
            cmd.Parameters.AddWithValue("$DataCobre", exame.DataCobre.ToString());
            cmd.Parameters.AddWithValue("$DataMirena", exame.DataMirena.ToString());
            cmd.Parameters.AddWithValue("$InfiltracaoCorticoide", exame.InfiltracaoCorticoide);
            cmd.Parameters.AddWithValue("$LocalInfiltracao", exame.LocalInfiltracao);
            cmd.Parameters.AddWithValue("$NomeAnticoncepcional", exame.NomeAnticoncepcional);
            cmd.Parameters.AddWithValue("$TmpUsoAnos", exame.TempoUsoAnticoncepcionalAnos);
            cmd.Parameters.AddWithValue("$TmpUsoMeses", exame.TempoUsoAnticoncepcionalMeses);
            cmd.Parameters.AddWithValue("$Sinvastatina", exame.Sinvastatina);
            cmd.Parameters.AddWithValue("$SimilarSinvastatina", exame.SimilarSinvastatina);
            cmd.Parameters.AddWithValue("$TmpUsoSivAnos", exame.TempoUsoSinvastatinaAnos);
            cmd.Parameters.AddWithValue("$TmpUsoSivMeses", exame.TempoUsoSinvastatinaMeses);
            cmd.Parameters.AddWithValue("$Omeprazol", exame.Omeprazol);
            cmd.Parameters.AddWithValue("$Corticosteroide", exame.Corticosteroide);
            cmd.Parameters.AddWithValue("$Antiarritmico", exame.Antiarritmico);
            cmd.Parameters.AddWithValue("$HormonioTireoidiano", exame.HormonioTireoidiano);
            cmd.Parameters.AddWithValue("$Antidepressivo", exame.Antidepressivo);
            cmd.Parameters.AddWithValue("$Antihipertensivo", exame.Antihipertensivo);
            cmd.Parameters.AddWithValue("$Dieta", exame.Dieta);
            cmd.Parameters.AddWithValue("$DescricaoDieta", exame.DescricaoDieta);
            cmd.Parameters.AddWithValue("$Hiperproteica", exame.Hiperproteica);
            cmd.Parameters.AddWithValue("$Hipocalorica", exame.Hipocalorica);
            cmd.Parameters.AddWithValue("$Herbalife", exame.Herbalife);
            cmd.Parameters.AddWithValue("$StatusExame", exame.StatusExame);
            cmd.ExecuteNonQuery();
        }

        public Exames ConsulteExame(int id)
        {
            Exames exame = new Exames();
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT * FROM PacienteExames WHERE Id = {id};";

            SqliteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                exame.Id = dr.GetGuid("Id");
                exame.Nome = dr.GetString("Nome");
                exame.Idade = dr.GetInt32("Idade");
                exame.DataNascimento = dr.GetDateTime("DataNascimento");
                //exame.EnumSexo = dr.GetInt32("Sexo");
                //exame.Data = dr.GetDateTime("Data");
                exame.MedicoRequisitante = dr.GetString("MedicoRequisitante");
                //exame.EnumTipoExame = dr.GetInt32("TipoExame");
                exame.Medico = dr.GetString("Medico");
                exame.EmailMedico = dr.GetString("EmailMedico");
                exame.Telefone = dr.GetString("Telefone");
                exame.MotivoExame = dr.GetString("MotivoExame");
                exame.SuspeitaDiagnostico = dr.GetString("SuspeitaDiagnostico");
                exame.TempoDor = dr.GetString("TempoDor");
                exame.MaFormacoes = dr.GetString("MaFormacoes");
                exame.DorMomentoExame = dr.GetBoolean("DorMomentoExame");
                exame.MenstruacaoAusente = dr.GetBoolean("MenstruacaoAusente");
                exame.MenstruacaoAusenteTempo = dr.GetString("MenstruacaoAusenteTempo");
                exame.MenstruacaoIrregular = dr.GetBoolean("MenstruacaoIrregular");
                exame.MenstruacaoDolorosa = dr.GetBoolean("MenstruacaoDolorosa");
                exame.Diabetes = dr.GetBoolean("Diabetes");
                exame.ArtriteReumatoide = dr.GetBoolean("ArtriteReumatoide");
                exame.Raynaud = dr.GetBoolean("Raynaud");
                exame.Gota = dr.GetBoolean("Gota");
                exame.Lupus = dr.GetBoolean("Lupus");
                exame.LitiaseBiliar = dr.GetBoolean("LitiaseBiliar");
                exame.LitiaseRenal = dr.GetBoolean("LitiaseRenal");
                exame.Dislipidemia = dr.GetBoolean("Dislipidemia");
                exame.Sinusite = dr.GetBoolean("Sinusite");
                exame.EnxaquecaFrontal = dr.GetBoolean("EnxaquecaFrontal");
                exame.EnxaquecaTemporalDireita = dr.GetBoolean("EnxaquecaTemporalDireita");
                exame.EnxaquecaTemporalEsquerda = dr.GetBoolean("EnxaquecaTemporalEsquerda");
                exame.EnxaquecaAlternante = dr.GetBoolean("EnxaquecaAlternante");
                exame.EnxaquecaNuca = dr.GetBoolean("EnxaquecaNuca");
                exame.EnxaquecaTopo = dr.GetBoolean("EnxaquecaTopo");
                exame.EnxaquecaTotal = dr.GetBoolean("EnxaquecaTotal");
                exame.EnxaquecaTiara = dr.GetBoolean("EnxaquecaTiara");
                exame.EnxaquecaPulsatil = dr.GetBoolean("EnxaquecaPulsatil");
                exame.Fotofobia = dr.GetBoolean("Fotofobia");
                exame.Fonofobia = dr.GetBoolean("Fonofobia");
                exame.PerfumeOdores = dr.GetBoolean("PerfumeOdores");
                exame.DormirMal = dr.GetBoolean("DormirMal");
                exame.Fome = dr.GetBoolean("Fome");
                exame.Alimentar = dr.GetBoolean("Alimentar");
                exame.DescricaoAlimentar = dr.GetString("DescricaoAlimentar");
                exame.OvarioPolicisticos = dr.GetBoolean("OvarioPolicisticos");
                exame.DisplasiaMamaria = dr.GetBoolean("DisplasiaMamaria");
                exame.Endometriose = dr.GetBoolean("Endometriose");
                exame.Varizes = dr.GetBoolean("Varizes");
                exame.Acne = dr.GetBoolean("Acne");
                exame.CPVesiculaBiliar = dr.GetBoolean("CPVesiculaBiliar");
                exame.CPHisterectomia = dr.GetBoolean("CPHisterectomia");
                exame.CPCornetosNasaisSinusite = dr.GetBoolean("CPCornetosNasaisSinusite");
                exame.CPCancerMamaDireita = dr.GetBoolean("CPCancerMamaDireita");
                exame.CPCancerMamaEsquerda = dr.GetBoolean("CPCancerMamaEsquerda");
                exame.CPVarizes = dr.GetBoolean("CPVarizes");
                exame.ProteseMamaria = dr.GetBoolean("ProteseMamaria");
                exame.ProteseMetalOssoCorpo = dr.GetBoolean("ProteseMetalOssoCorpo");
                exame.TelaMarlex = dr.GetBoolean("TelaMarlex");
                exame.AmputacaoMembro = dr.GetBoolean("AmputacaoMembro");
                exame.AmputacaoMembroEspecificada = dr.GetString("AmputacaoMembroEspecificada");
                exame.ImpAnticoncepcional = dr.GetBoolean("ImpAnticoncepcional");
                //exame.DataImpAnticoncepcional = dr.GetDateTime("DataImpAnticoncepcional");
                exame.DIUCobre = dr.GetBoolean("DIUCobre");
                exame.DIUMirena = dr.GetBoolean("DIUMirena");
                exame.DataCobre = dr.GetDateTime("DataCobre");
                exame.DataMirena = dr.GetDateTime("DataMirena");
                exame.InfiltracaoCorticoide = dr.GetBoolean("InfiltracaoCorticoide");
                exame.LocalInfiltracao = dr.GetString("LocalInfiltracao");
                exame.NomeAnticoncepcional = dr.GetString("NomeAnticoncepcional");
                exame.TempoUsoAnticoncepcionalAnos = dr.GetString("TmpUsoAnos");
                exame.TempoUsoAnticoncepcionalMeses = dr.GetString("TmpUsoMeses");
                exame.Sinvastatina = dr.GetBoolean("Sinvastatina");
                exame.SimilarSinvastatina = dr.GetString("SimilarSinvastatina");
                exame.TempoUsoSinvastatinaAnos = dr.GetString("TmpUsoSivAnos");
                exame.TempoUsoSinvastatinaMeses = dr.GetString("TmpUsoSivMeses");
                exame.Omeprazol = dr.GetBoolean("Omeprazol");
                exame.Corticosteroide = dr.GetBoolean("Corticosteroide");
                exame.Antiarritmico = dr.GetBoolean("Antiarritmico");
                exame.HormonioTireoidiano = dr.GetBoolean("HormonioTireoidiano");
                exame.Antidepressivo = dr.GetBoolean("Antidepressivo");
                exame.Antihipertensivo = dr.GetBoolean("Antihipertensivo");
                exame.Dieta = dr.GetBoolean("Dieta");
                exame.DescricaoDieta = dr.GetString("DescricaoDieta");
                exame.Hiperproteica = dr.GetBoolean("Hiperproteica");
                exame.Hipocalorica = dr.GetBoolean("Hipocalorica");
                exame.Herbalife = dr.GetBoolean("Herbalife");
                exame.StatusExame = dr.GetBoolean("StatusExame");
            };

                return exame;

        }

        public IEnumerable<Exames> GetAll()
        {
            List<Exames> exames = new List<Exames>();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"SELECT * FROM PacienteExames;";
            SqliteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Exames exame = new Exames();
                {
                    exame.Id = dr.GetGuid("Id");
                    exame.Nome = dr.GetString("Nome");
                    exame.Idade = dr.GetInt32("Idade");
                    exame.DataNascimento = dr.GetDateTime("DataNascimento");
                    //exame.EnumSexo = dr.GetInt32("Sexo");
                    //exame.Data = dr.GetDateTime("Data");
                    exame.MedicoRequisitante = dr.GetString("MedicoRequisitante");
                    //exame.EnumTipoExame = dr.GetInt32("TipoExame");
                    exame.Medico = dr.GetString("Medico");
                    exame.EmailMedico = dr.GetString("EmailMedico");
                    exame.Telefone = dr.GetString("Telefone");
                    exame.MotivoExame = dr.GetString("MotivoExame");
                    exame.SuspeitaDiagnostico = dr.GetString("SuspeitaDiagnostico");
                    exame.TempoDor = dr.GetString("TempoDor");
                    exame.MaFormacoes = dr.GetString("MaFormacoes");
                    exame.DorMomentoExame = dr.GetBoolean("DorMomentoExame");
                    exame.MenstruacaoAusente = dr.GetBoolean("MenstruacaoAusente");
                    exame.MenstruacaoAusenteTempo = dr.GetString("MenstruacaoAusenteTempo");
                    exame.MenstruacaoIrregular = dr.GetBoolean("MenstruacaoIrregular");
                    exame.MenstruacaoDolorosa = dr.GetBoolean("MenstruacaoDolorosa");
                    exame.Diabetes = dr.GetBoolean("Diabetes");
                    exame.ArtriteReumatoide = dr.GetBoolean("ArtriteReumatoide");
                    exame.Raynaud = dr.GetBoolean("Raynaud");
                    exame.Gota = dr.GetBoolean("Gota");
                    exame.Lupus = dr.GetBoolean("Lupus");
                    exame.LitiaseBiliar = dr.GetBoolean("LitiaseBiliar");
                    exame.LitiaseRenal = dr.GetBoolean("LitiaseRenal");
                    exame.Dislipidemia = dr.GetBoolean("Dislipidemia");
                    exame.Sinusite = dr.GetBoolean("Sinusite");
                    exame.EnxaquecaFrontal = dr.GetBoolean("EnxaquecaFrontal");
                    exame.EnxaquecaTemporalDireita = dr.GetBoolean("EnxaquecaTemporalDireita");
                    exame.EnxaquecaTemporalEsquerda = dr.GetBoolean("EnxaquecaTemporalEsquerda");
                    exame.EnxaquecaAlternante = dr.GetBoolean("EnxaquecaAlternante");
                    exame.EnxaquecaNuca = dr.GetBoolean("EnxaquecaNuca");
                    exame.EnxaquecaTopo = dr.GetBoolean("EnxaquecaTopo");
                    exame.EnxaquecaTotal = dr.GetBoolean("EnxaquecaTotal");
                    exame.EnxaquecaTiara = dr.GetBoolean("EnxaquecaTiara");
                    exame.EnxaquecaPulsatil = dr.GetBoolean("EnxaquecaPulsatil");
                    exame.Fotofobia = dr.GetBoolean("Fotofobia");
                    exame.Fonofobia = dr.GetBoolean("Fonofobia");
                    exame.PerfumeOdores = dr.GetBoolean("PerfumeOdores");
                    exame.DormirMal = dr.GetBoolean("DormirMal");
                    exame.Fome = dr.GetBoolean("Fome");
                    exame.Alimentar = dr.GetBoolean("Alimentar");
                    exame.DescricaoAlimentar = dr.GetString("DescricaoAlimentar");
                    exame.OvarioPolicisticos = dr.GetBoolean("OvarioPolicisticos");
                    exame.DisplasiaMamaria = dr.GetBoolean("DisplasiaMamaria");
                    exame.Endometriose = dr.GetBoolean("Endometriose");
                    exame.Varizes = dr.GetBoolean("Varizes");
                    exame.Acne = dr.GetBoolean("Acne");
                    exame.CPVesiculaBiliar = dr.GetBoolean("CPVesiculaBiliar");
                    exame.CPHisterectomia = dr.GetBoolean("CPHisterectomia");
                    exame.CPCornetosNasaisSinusite = dr.GetBoolean("CPCornetosNasaisSinusite");
                    exame.CPCancerMamaDireita = dr.GetBoolean("CPCancerMamaDireita");
                    exame.CPCancerMamaEsquerda = dr.GetBoolean("CPCancerMamaEsquerda");
                    exame.CPVarizes = dr.GetBoolean("CPVarizes");
                    exame.ProteseMamaria = dr.GetBoolean("ProteseMamaria");
                    exame.ProteseMetalOssoCorpo = dr.GetBoolean("ProteseMetalOssoCorpo");
                    exame.TelaMarlex = dr.GetBoolean("TelaMarlex");
                    exame.AmputacaoMembro = dr.GetBoolean("AmputacaoMembro");
                    exame.AmputacaoMembroEspecificada = dr.GetString("AmputacaoMembroEspecificada");
                    exame.ImpAnticoncepcional = dr.GetBoolean("ImpAnticoncepcional");
                    //exame.DataImpAnticoncepcional = dr.GetDateTime("DataImpAnticoncepcional");
                    exame.DIUCobre = dr.GetBoolean("DIUCobre");
                    exame.DIUMirena = dr.GetBoolean("DIUMirena");
                    exame.DataCobre = dr.GetDateTime("DataCobre");
                    exame.DataMirena = dr.GetDateTime("DataMirena");
                    exame.InfiltracaoCorticoide = dr.GetBoolean("InfiltracaoCorticoide");
                    exame.LocalInfiltracao = dr.GetString("LocalInfiltracao");
                    exame.NomeAnticoncepcional = dr.GetString("NomeAnticoncepcional");
                    exame.TempoUsoAnticoncepcionalAnos = dr.GetString("TmpUsoAnos");
                    exame.TempoUsoAnticoncepcionalMeses = dr.GetString("TmpUsoMeses");
                    exame.Sinvastatina = dr.GetBoolean("Sinvastatina");
                    exame.SimilarSinvastatina = dr.GetString("SimilarSinvastatina");
                    exame.TempoUsoSinvastatinaAnos = dr.GetString("TmpUsoSivAnos");
                    exame.TempoUsoSinvastatinaMeses = dr.GetString("TmpUsoSivMeses");
                    exame.Omeprazol = dr.GetBoolean("Omeprazol");
                    exame.Corticosteroide = dr.GetBoolean("Corticosteroide");
                    exame.Antiarritmico = dr.GetBoolean("Antiarritmico");
                    exame.HormonioTireoidiano = dr.GetBoolean("HormonioTireoidiano");
                    exame.Antidepressivo = dr.GetBoolean("Antidepressivo");
                    exame.Antihipertensivo = dr.GetBoolean("Antihipertensivo");
                    exame.Dieta = dr.GetBoolean("Dieta");
                    exame.DescricaoDieta = dr.GetString("DescricaoDieta");
                    exame.Hiperproteica = dr.GetBoolean("Hiperproteica");
                    exame.Hipocalorica = dr.GetBoolean("Hipocalorica");
                    exame.Herbalife = dr.GetBoolean("Herbalife");
                    exame.StatusExame = dr.GetBoolean("StatusExame");
                };
                exames.Add(exame);
            }
            return exames;
        }

        public IEnumerable<Exames> GetPorFiltro(Func<Exames, bool> filtro)
        {
            return GetAll().ToList().Where(filtro);
        }

        public void Inserir(Exames exame)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"INSERT INTO PacienteExames
                                        (Nome, Idade, DataNascimento, Sexo, Data, MedicoRequisitante, TipoExame,
                                            Medico, EmailMedico, Telefone, MotivoExame, SupeitaDiagnostico, TempoDor,
                                            MaFormacoes, Mestruacao, MestruacaoAusenteTempo, Sindromes, Enxaquecas, Disturbios,
                                            AlteracoesHormonais, ProcessosCirurgicosPrevios, ProcessosRelacionadosProtese, DadosImplantes,
                                            TipoInfiltraçao, DadosAnticoncepcional, DadosFarmaco, TiposFarmaco)
                                 VALUES
                                        ($Nome, $Idade, $DataNascimento, $Sexo, $Data, $MedicoRequisitante, $TipoExame, $Medico,
                                            $EmailMedico, $Telefone, $MotivoExame, $SupeitaDiagnostico, $TempoDor, $MaFormacoes,
                                            $Mestruacao, $MestruacaoAusenteTempo, $Sindromes, $Enxaquecas, $Disturbios, $AlteracoesHormonais,
                                            $ProcessosCirurgicosPrevios, $ProcessosRelacionadosProtese, $DadosImplantes, $TipoInfiltraçao,
                                            $DadosAnticoncepcional, $DadosFarmaco, $TiposFarmaco);";
            cmd.Parameters.AddWithValue("$Nome", exame.Nome);
            cmd.Parameters.AddWithValue("$Idade", exame.Idade);
            cmd.Parameters.AddWithValue("$DataNascimento", exame.DataNascimento);
             // cmd.Parameters.AddWithValue("$Sexo", exame.EnumSexo);
            //cmd.Parameters.AddWithValue("$Data", exame.Data);
            cmd.Parameters.AddWithValue("$MedicoRequisitante", exame.MedicoRequisitante);
            //cmd.Parameters.AddWithValue("$TipoExame", exame.EnumTipoExame);
            cmd.Parameters.AddWithValue("$Medico", exame.Medico);
            cmd.Parameters.AddWithValue("$EmailMedico", exame.EmailMedico);
            cmd.Parameters.AddWithValue("$Telefone", exame.Telefone);
            cmd.Parameters.AddWithValue("$Motivo", exame.MotivoExame);
            cmd.Parameters.AddWithValue("$SuspeitaDiagnostico", exame.SuspeitaDiagnostico);
            cmd.Parameters.AddWithValue("$TempoDor", exame.TempoDor);
            cmd.Parameters.AddWithValue("$MaFormacoes", exame.MaFormacoes);
            cmd.Parameters.AddWithValue("$DorMomentoExame", exame.DorMomentoExame);
            cmd.Parameters.AddWithValue("$MenstruacaoAusente", exame.MenstruacaoAusente);
            cmd.Parameters.AddWithValue("$MenstruacaoAusenteTempo", exame.MenstruacaoAusenteTempo);
            cmd.Parameters.AddWithValue("$MenstruacaoIrregular", exame.MenstruacaoIrregular);
            cmd.Parameters.AddWithValue("$MenstruacaoDolorosa", exame.MenstruacaoDolorosa);
            cmd.Parameters.AddWithValue("$Diabetes", exame.Diabetes);
            cmd.Parameters.AddWithValue("$ArtriteReumatoide", exame.ArtriteReumatoide);
            cmd.Parameters.AddWithValue("$Raynaud", exame.Raynaud);
            cmd.Parameters.AddWithValue("$Gota", exame.Gota);
            cmd.Parameters.AddWithValue("$Lupus", exame.Lupus);
            cmd.Parameters.AddWithValue("$LitiaseBiliar", exame.LitiaseBiliar);
            cmd.Parameters.AddWithValue("$LitiaseRenal", exame.LitiaseRenal);
            cmd.Parameters.AddWithValue("$Dislipidemia", exame.Dislipidemia);
            cmd.Parameters.AddWithValue("$Sinusite", exame.Sinusite);
            cmd.Parameters.AddWithValue("$EnxaquecaFrontal", exame.EnxaquecaFrontal);
            cmd.Parameters.AddWithValue("$EnxaquecaTemporalDireita", exame.EnxaquecaTemporalDireita);
            cmd.Parameters.AddWithValue("$EnxaquecaTemporalEsquerda", exame.EnxaquecaTemporalEsquerda);
            cmd.Parameters.AddWithValue("$EnxaquecaAlternante", exame.EnxaquecaAlternante);
            cmd.Parameters.AddWithValue("$EnxaquecaNuca", exame.EnxaquecaNuca);
            cmd.Parameters.AddWithValue("$EnxaquecaTopo", exame.EnxaquecaTopo);
            cmd.Parameters.AddWithValue("$EnxaquecaTotal", exame.EnxaquecaTotal);
            cmd.Parameters.AddWithValue("$EnxaquecaTiara", exame.EnxaquecaTiara);
            cmd.Parameters.AddWithValue("$EnxaquecaPulsatil", exame.EnxaquecaPulsatil);
            cmd.Parameters.AddWithValue("$Fotofobia", exame.Fotofobia);
            cmd.Parameters.AddWithValue("$Fonofobia", exame.Fonofobia);
            cmd.Parameters.AddWithValue("$PerfumeOdores", exame.PerfumeOdores);
            cmd.Parameters.AddWithValue("$DormirMal", exame.DormirMal);
            cmd.Parameters.AddWithValue("$Fome", exame.Fome);
            cmd.Parameters.AddWithValue("$Alimentar", exame.Alimentar);
            cmd.Parameters.AddWithValue("$DescricaoAlimentar", exame.DescricaoAlimentar);
            cmd.Parameters.AddWithValue("$OvarioPolicisticos", exame.OvarioPolicisticos);
            cmd.Parameters.AddWithValue("$DisplasiaMamaria", exame.DisplasiaMamaria);
            cmd.Parameters.AddWithValue("$Endometriose", exame.Endometriose);
            cmd.Parameters.AddWithValue("$Varizes", exame.Varizes);
            cmd.Parameters.AddWithValue("$Acne", exame.Acne);
            cmd.Parameters.AddWithValue("$CPVesiculaBiliar", exame.CPVesiculaBiliar);
            cmd.Parameters.AddWithValue("$CPHisterectomia", exame.CPHisterectomia);
            cmd.Parameters.AddWithValue("$CPCornetosNasaisSinusite", exame.CPCornetosNasaisSinusite);
            cmd.Parameters.AddWithValue("$CPCancerMamaDireita", exame.CPCancerMamaDireita);
            cmd.Parameters.AddWithValue("$CPCancerMamaEsquerda", exame.CPCancerMamaEsquerda);
            cmd.Parameters.AddWithValue("$CPVarizes", exame.CPVarizes);
            cmd.Parameters.AddWithValue("$ProteseMamaria", exame.ProteseMamaria);
            cmd.Parameters.AddWithValue("$ProteseMetalOssoCorpo", exame.ProteseMetalOssoCorpo);
            cmd.Parameters.AddWithValue("$TelaMarlex", exame.TelaMarlex);
            cmd.Parameters.AddWithValue("$AmputacaoMembro", exame.AmputacaoMembro);
            cmd.Parameters.AddWithValue("$AmputacaoMembroEspecificada", exame.AmputacaoMembroEspecificada);
            cmd.Parameters.AddWithValue("$ImpAnticoncepcional", exame.ImpAnticoncepcional);
            //cmd.Parameters.AddWithValue("$DataImpAnticoncepcional", exame.DataImpAnticoncepcional);
            cmd.Parameters.AddWithValue("$DIUCobre", exame.DIUCobre);
            cmd.Parameters.AddWithValue("$DIUMirena", exame.DIUMirena);
            cmd.Parameters.AddWithValue("$DataCobre", exame.DataCobre);
            cmd.Parameters.AddWithValue("$DataMirena", exame.DataMirena);
            cmd.Parameters.AddWithValue("$InfiltracaoCorticoide", exame.InfiltracaoCorticoide);
            cmd.Parameters.AddWithValue("$LocalInfiltracao", exame.LocalInfiltracao);
            cmd.Parameters.AddWithValue("$NomeAnticoncepcional", exame.NomeAnticoncepcional);
            cmd.Parameters.AddWithValue("$TmpUsoAnos", exame.TempoUsoAnticoncepcionalAnos);
            cmd.Parameters.AddWithValue("$TmpUsoMeses", exame.TempoUsoAnticoncepcionalMeses);
            cmd.Parameters.AddWithValue("$Sinvastatina", exame.Sinvastatina);
            cmd.Parameters.AddWithValue("$SimilarSinvastatina", exame.SimilarSinvastatina);
            cmd.Parameters.AddWithValue("$TmpUsoSivAnos", exame.TempoUsoSinvastatinaAnos);
            cmd.Parameters.AddWithValue("$TmpUsoSivMeses", exame.TempoUsoSinvastatinaMeses);
            cmd.Parameters.AddWithValue("$Omeprazol", exame.Omeprazol);
            cmd.Parameters.AddWithValue("$Corticosteroide", exame.Corticosteroide);
            cmd.Parameters.AddWithValue("$Antiarritmico", exame.Antiarritmico);
            cmd.Parameters.AddWithValue("$HormonioTireoidiano", exame.HormonioTireoidiano);
            cmd.Parameters.AddWithValue("$Antidepressivo", exame.Antidepressivo);
            cmd.Parameters.AddWithValue("$Antihipertensivo", exame.Antihipertensivo);
            cmd.Parameters.AddWithValue("$Dieta", exame.Dieta);
            cmd.Parameters.AddWithValue("$DescricaoDieta", exame.DescricaoDieta);
            cmd.Parameters.AddWithValue("$Hiperproteica", exame.Hiperproteica);
            cmd.Parameters.AddWithValue("$Hipocalorica", exame.Hipocalorica);
            cmd.Parameters.AddWithValue("$Herbalife", exame.Herbalife);
            cmd.Parameters.AddWithValue("$StatusExame", true);
            cmd.ExecuteNonQuery();
        }

        public void Remova(int id)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $"DELETE FROM PacienteExames WHERE Id = {id};";
            cmd.ExecuteNonQuery();
        }
    }
}



	
