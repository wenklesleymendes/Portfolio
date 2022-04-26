using EMCatraca.Neokoros;
using EMCatraca.RegrasAcesso.Neokoros.ColegioWrBioFacial;

namespace EMCatraca.RegrasAcesso.ColegioWrBioFacial
{
    public class ControladorBiometriaFacial : ControladorBioFacialNeokoros
    {
        protected override BioFacialNeokorosAbstract CrieBioFacailNeokoros()
        {
            return new  BioFacialNeokorosWr();
        }
    }
}
