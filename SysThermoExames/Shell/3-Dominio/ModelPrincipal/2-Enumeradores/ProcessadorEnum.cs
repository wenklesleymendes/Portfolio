namespace ModelPrincipal._2_Enumeradores
{
    public class ProcessadorEnum
    {
        public int ObtenhaIndexSexo(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return EnumSexo.Homem.GetHashCode();

                case 1:
                    return EnumSexo.HomemTrans.GetHashCode();

                case 2:
                    return EnumSexo.Mulher.GetHashCode();

                case 3:
                    return EnumSexo.MulherTrans.GetHashCode();

                default:
                    return EnumSexo.SemIndex.GetHashCode();
            }
        }

        public EnumSexo ObtenhaEnumeradorSexo(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return EnumSexo.Homem;

                case 1:
                    return EnumSexo.HomemTrans;

                case 2:
                    return EnumSexo.Mulher;

                case 3:
                    return EnumSexo.MulherTrans;

                default:
                    return EnumSexo.SemIndex;
            }
        }

        public int ObtenhaIndexEtnia(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return EnumEtnia.Asiatico.GetHashCode();

                case 1:
                    return EnumEtnia.Caucasiano.GetHashCode();

                default:
                    return EnumEtnia.SemIndex.GetHashCode();
            }
        }

        public EnumEtnia ObtenhaEnumeradorEtnia(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    return EnumEtnia.Asiatico;

                case 1:
                    return EnumEtnia.Caucasiano;

                default:
                    return EnumEtnia.SemIndex;
            }
        }
    }
}
