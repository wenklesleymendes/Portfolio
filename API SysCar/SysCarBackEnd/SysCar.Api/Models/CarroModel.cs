using SysCar.Dominio;
using SysCar.Repositorio;
using System;
using System.Collections.Generic;

namespace SysCar.Models
{
    public class CarroModel
    {
        public List<CarroDTO> ObtenhaCarros(int? id = null)
        {
            try
            {
                var bd = new CarroDAO();
                return bd.ObtenhaCarros(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Carros: Erro => {ex.Message}");
            }
        }

        public List<CarroDTO> ObtenhaCarrosPorMarcaEhCor(string cor, string marca)
        {
            try
            {
                var bd = new CarroDAO();
                return bd.ObtenhaCarrosMarcaEhCor(cor, marca);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Carros: Erro => {ex.Message}");
            }
        }

        public void Inserir(CarroDTO carro)
        {
            try
            {
                var bd = new CarroDAO();
                bd.Inserir(carro);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Inserir carro: Erro => {ex.Message}");
            }
        }

        public void Atualizar(CarroDTO carro)
        {
            try
            {
                CarroDAO bd = new CarroDAO();
                bd.Atualizar(carro);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Atualizar carro: Erro => {ex.Message}");
            }
        }

        public void Deletar(int id)
        {
            try
            {
                var bd = new CarroDAO();
                bd.Deletar(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Deletar carro: Erro => {ex.Message}");
            }
        }
    }
}