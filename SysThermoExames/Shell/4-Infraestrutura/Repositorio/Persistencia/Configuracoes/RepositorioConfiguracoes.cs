using Microsoft.Data.Sqlite;
using ModelPrincipal.Utilitarios;
using Repositorio.Conexao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Persistencia.Configuracoes
{
    public class RepositorioConfiguracoes : IRepositorioConfiguracoes
    {
        public RepositorioConfiguracoes()
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS TBCONFIGURACAO(ID int, PATH Varchar(100));";
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<DtoConfigModulos> ObtenhaTodos()
        {
            List<DtoConfigModulos> configuracoes = new List<DtoConfigModulos>();

            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = "SELECT * FROM ConfiguracaoModulo";
            SqliteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DtoConfigModulos configuracao = new DtoConfigModulos()
                {
                    PathModulos = dr.GetString("PathModulos")
                };
                configuracoes.Add(configuracao);
            }
            return configuracoes;
        }

        public void Crie(DtoConfigModulos configuracao)
        {
            configuracao.Id = GereCodigo();
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"INSERT INTO TBCONFIGURACAO (ID, PATH)
                                 VALUES ($ID, $PATH);";
            cmd.Parameters.AddWithValue("$ID", configuracao.Id);
            cmd.Parameters.AddWithValue("$PATH", configuracao.PathModulos);

            cmd.ExecuteNonQuery();
        }

        public void Atualize(DtoConfigModulos configuracao)
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"UPDATE TBCONFIGURACAO 
                                 SET PATH = '$PATH'
                                 WHERE ID = $ID;";

            cmd.Parameters.AddWithValue("$ID", configuracao.Id);
            cmd.Parameters.AddWithValue("$PATH", configuracao.PathModulos);
            cmd.ExecuteNonQuery();
        }

        public void RemovaTodos()
        {
            SqliteCommand cmd = DBHelper.ConexaoDb().CreateCommand();
            cmd.CommandText = $@"DELETE FROM TBCONFIGURACAO";
            cmd.ExecuteNonQuery();
        }
        public int GereCodigo()
        {
            var todosOperadores = ObtenhaTodos();
            var numeroOperadores = todosOperadores.Count();
            var codigosOperadores = ObtenhaTodos().Select(o => o.Id).ToArray();

            if (numeroOperadores == 0)
            {
                return 1;
            }

            if (codigosOperadores.Length == (int)numeroOperadores &&
                codigosOperadores.Max() == codigosOperadores.Length)
            {
                return codigosOperadores.Max() == 0 ? 1 : codigosOperadores.Max() + 1;
            }

            int[] codigos = new int[codigosOperadores.Length + 1];
            int f = 0;
            codigos[0] = 0;
            foreach (int i in codigosOperadores)
            {
                f++;
                codigos[f] = i;
            }
            Array.Sort(codigos);
            List<int> codigosfaltantes = new List<int>();

            for (int i = 0; i < numeroOperadores; i++)
            {
                for (int j = 0; j < codigos.Length - 1; j++)
                {
                    if (codigos[j] + 1 == codigos[j + 1])
                    {
                        continue;
                    }
                    else
                    {
                        if (!codigosfaltantes.Contains(codigos[j] + 1))
                        {
                            codigosfaltantes.Add(codigos[j] + 1);
                        }
                    }
                }

            }

            return codigosfaltantes.Min();
        }
    }
}
