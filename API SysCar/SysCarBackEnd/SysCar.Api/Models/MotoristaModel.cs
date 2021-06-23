using SysCar.Dominio;
using SysCar.Repositorio;
using System;
using System.Collections.Generic;

namespace SysCar.Models
{
    public class MotoristaModel
    {
        public List<MotoristaDTO> ObtenhaMotoristas()
        {
            try
            {
                var bd = new MotoristaDAO();
                return bd.ObtenhaMotoristas();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Motoristas: Erro => {ex.Message}");
            }
        }

        public List<MotoristaDTO> ObtenhaMotoristaPorNome(string nome)
        {
            try
            {
                var bd = new MotoristaDAO();
                return bd.ObtenhaMotoristaPorNome(nome);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Motoristas: Erro => {ex.Message}");
            }
        }

        public void Inserir(MotoristaDTO Motorista)
        {
            try
            {
                var bd = new MotoristaDAO();
                bd.Inserir(Motorista);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Inserir Motorista: Erro => {ex.Message}");
            }
        }

        public void Atualizar(MotoristaDTO Motorista)
        {
            try
            {
                MotoristaDAO bd = new MotoristaDAO();
                bd.Atualizar(Motorista);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Atualizar Motorista: Erro => {ex.Message}");
            }
        }

        public void Deletar(int id)
        {
            try
            {
                var bd = new MotoristaDAO();
                bd.Deletar(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Deletar Motorista: Erro => {ex.Message}");
            }
        }
    }
}