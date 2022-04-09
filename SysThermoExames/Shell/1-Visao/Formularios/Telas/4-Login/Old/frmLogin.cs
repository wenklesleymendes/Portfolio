//using Formularios.Telas._1_Principal;
//using Formularios.Telas._2_Operadores;
//using Formularios.Telas._3_Componentes;
//using MaterialSkin.Controls;
//using ModelPrincipal._1_Entidades;
//using Processos.Operadores;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Formularios.Telas._4_Login
//{
//    public partial class frmLogin : MaterialForm
//    {
//        //TODO: Implementar recuperação de senha
//        private ProcessoLogin processoDeLogin = new ProcessoLogin();
//        private bool StatusDB { get; set; }

//        public frmLogin()
//        {
//            VerifiqueStatusMongo();
//            if (!StatusDB)
//            {
//                Application.Exit();
//            }

//            InitializeComponent();
            
//        }

//        private void frmLogin_Load(object sender, EventArgs e)
//        {


//            VerifiqueAdm();
//            Temas.InicializeTemaPadrao(this);


//        }

//        private void VerifiqueAdm()
//        {
//            if(processoDeLogin.PossuiAdministrador())
//            {
//                return;
//            }

//            MessageBox.Show("O sistema não possui administrador cadastrado. Você será redirecionado para a tela de cadastro");
//            new frmCadastroOperador(true).ShowDialog();
//        }

//        private void VerifiqueStatusMongo(bool EhModulo = false)
//        {
//            string mensagem = EhModulo ?
//                "Não é possível abrir os módulos sem uma conexão ao banco de dados" :
//                "Não foi possível confirmar o status da conexão com o MongoDB. " +
//                "Verifique se o servidor está sendo executado e tente novamente.";
//            try
//            {
//                new Repositorio.Conexao.StatusDeConexao().VerifiqueConexao();
//                StatusDB = true;
//            }
//            catch (Exception ex)
//            {
//                StatusDB = false;
//                MessageBox.Show(mensagem);
//            }
//        }

//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            var login = txtboxLogin.Text;
//            var senha = txtboxSenha.Text;
//            if(!ValideLogin())
//            {
//                return;
//            }

//            var operadorLogado = processoDeLogin.RealizeLogin(login, senha);

//            if(operadorLogado != null)
//            {
//                new frmPrincipal(operadorLogado).Show();
//                this.Hide();
//                return;
//            }

//            MessageBox.Show("Login incorreto. Verifique as informações e tente novamente.");
//            txtboxSenha.Clear();
                                    
//        }

//        private bool ValideLogin()
//        {
//            if(txtboxLogin.Text != "" && txtboxSenha.Text != "" && txtboxSenha.Text.Length > 5 && txtboxLogin.Text.Length > 2)
//            {
//                return true;
//            }

//            if(EhAdministradorPrincipal())
//            {
//                return true;
//            }

//            string mensagem = string.Empty;

//            if(txtboxLogin.Text == "")
//            {
//                mensagem = string.Concat(mensagem, "O campo login é obrigatório \n");
//            }

//            if(txtboxLogin.Text.Length < 2)
//            {
//                mensagem = string.Concat(mensagem, "O campo login deve conter três caracteres ou mais\n");
//            }

//            if (txtboxSenha.Text == "")
//            {
//                mensagem = string.Concat(mensagem, "O campo senha é obrigatório \n");
//            }

//            if (txtboxSenha.Text.Length < 5)
//            {
//                mensagem = string.Concat(mensagem, "O campo senha deve conter cinco caracteres ou mais \n");
//            }

//            MessageBox.Show(mensagem);
//            return false;
//        }

//        private bool EhAdministradorPrincipal()
//        {
//            return processoDeLogin.EhAdministradorPrincipal(txtboxLogin.Text, txtboxSenha.Text);

//        }
//    }
//}
