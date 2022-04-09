using MdPaciente.Dominio;
using System;
using System.Collections.Generic;

namespace MdPaciente.Infraestrutura
{
    public interface IRepositorioExames
    {
        
            void Inserir(Exames exame);
            void Atualize(Exames paciente);
            void Remova(int id);
            IEnumerable<Exames> GetAll();
            IEnumerable<Exames> GetPorFiltro(Func<Exames, bool> filtro);
            Exames ConsulteExame(int id);
        
    }
}
