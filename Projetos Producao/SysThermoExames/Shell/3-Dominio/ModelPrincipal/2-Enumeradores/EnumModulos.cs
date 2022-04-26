namespace ModelPrincipal._2_Enumeradores
{
    public class EnumModulos : EnumBase
    {
        public bool EhAdicional { get; private set; }
        private EnumModulos(int id, string name, bool ehAdicional = false) : base(id, name)
        {
            EhAdicional = ehAdicional;
        }

        public EnumModulos Paciente => new EnumModulos(1, "Modulo Pacientes");
        public EnumModulos EdicaoDeImagens => new EnumModulos(2, "Edição de Imagens", true);
        public EnumModulos GeracaoDeRelatorios => new EnumModulos(3, "Geraração de Relatórios", true);

      
    }
}
