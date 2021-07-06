using Acesso.Dominio.Pessoas.Tipo;
using Acesso.Interfaces.Excecoes;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.DB
{
    public class RepositorioProfessor : IRepositorioProfessor
    {
        public Professor ConsulteProfessor(int idProfessor)
        {
            var sql = $@"SELECT p.PROFCODIGO, p.PROFNOME, i.TIMGIMAGEM
                        FROM TBPROFESSOR p LEFT JOIN TBIMAGEM i
                        ON p.PROFFOTO = i.TIMGID
                        WHERE p.PROFCODIGO = {idProfessor}";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.Read())
                {
                    var professor = new Professor
                    {
                        Id = dr.GetInteger("PROFCODIGO"),
                        Nome = dr.GetString("PROFNOME"),
                        Foto = dr.GetBytes("TIMGIMAGEM"),
                        TipoPessoa = TipoPessoa.Professor
                    };

                    return professor;
                }
            }
            throw new AcessoNegadoException("Registro não encontrado!");
        }

        public IEnumerable<Professor> ConsulteTodosProfessorAtivos()
        {
            var sql = $@"SELECT PROFCODIGO, PROFNOME 
                        FROM TBPROFESSOR 
                        WHERE PROFATIVO = 'S' 
                        ORDER BY PROFNOME;";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                if (dr.HasRows)
                {
                    var professores = new List<Professor>();
                    while (dr.Read())
                    {
                        var professor = new Professor
                        {
                            Id = dr.GetInteger("PROFCODIGO"),
                            Nome = dr.GetString("PROFNOME"),
                            TipoPessoa = TipoPessoa.Professor
                        };
                        professores.Add(professor);
                    }
                    return professores;
                }
            }
            throw new AcessoNegadoException("Professores não encontrados!");
        }

        public bool ProfessorEstaAtivo(int idProfessor)
        {
            var sql = $@"SELECT 1
                        FROM TBPROFESSOR
                        WHERE PROFATIVO='S'
                        AND PROFCODIGO = {idProfessor}";

            using (var dr = DBHelper.Instancia.ExecuteReader(sql))
            {
                return dr.Read();
            }
        }
    }
}
