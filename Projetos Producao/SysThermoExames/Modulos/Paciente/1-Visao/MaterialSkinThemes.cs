using MaterialSkin;
using MaterialSkin.Controls;

namespace MdPaciente._1_Visao
{
    public static class MaterialSkinThemes
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
