using MdPaciente._3_Dominio;
using System;
using System.Collections.Generic;

namespace MdPaciente._4_Infraestrutura
{
    public interface IRepositorioPacientes
    {
        void Inserir(Paciente paciente);
        void Atualize(Paciente paciente);
        void Remova(int id);
        IEnumerable<Paciente> GetAll();
        IEnumerable<Paciente> GetPorFiltro(Func<Paciente, bool> filtro);
        Paciente ConsultePaciente(int id);
    }
}
