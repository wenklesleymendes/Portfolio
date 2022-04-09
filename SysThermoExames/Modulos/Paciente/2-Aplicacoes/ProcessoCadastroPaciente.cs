﻿using MdPaciente._3_Dominio;
using MdPaciente._4_Infraestrutura;
using System.Collections.Generic;
using System.Linq;

namespace MdPaciente._2_Aplicacoes
{
    public class ProcessoPaciente
    {
        private readonly IRepositorioPacientes Repositorio = new RepositorioPacientes();

        public ProcessoPaciente()
        {
        }

        public void NovoPaciente(Paciente paciente)
        {
            Repositorio.Inserir(paciente);
        }

        public IEnumerable<Paciente> ConsulteTodosPacientes()
        {
            return Repositorio.GetAll();
        }

        public void Atualize(Paciente paciente)
        {
            Repositorio.Atualize(paciente);
        }

        public void ExcluaPaciente(Paciente paciente)
        {
            Repositorio.Remova(paciente.Codigo);
        }

        public IEnumerable<Paciente> ConsultePacientes(string nomeOuCPF)
        {
            return Repositorio.GetPorFiltro(o => o.Nome.Contains(nomeOuCPF));
        }

    }
}
