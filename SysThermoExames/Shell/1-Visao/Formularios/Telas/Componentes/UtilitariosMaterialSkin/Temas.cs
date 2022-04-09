using MaterialSkin;
using MaterialSkin.Controls;

namespace Formularios.Telas._3_Componentes
{
    public static class Temas
    {
        public static void InicializeTemaPadrao(MaterialForm form)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(form);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Amber500, Primary.DeepOrange500, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
    }
}
