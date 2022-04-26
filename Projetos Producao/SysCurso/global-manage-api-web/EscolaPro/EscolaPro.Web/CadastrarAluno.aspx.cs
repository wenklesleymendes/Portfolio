using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EscolaPro.Web
{
    public partial class CadastrarAluno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#mymodal').modal('show');</script> ", false);
        }

        protected void ButtonCreateSave_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateCancel_Click(object sender, EventArgs e)
        {

        }
    }
}