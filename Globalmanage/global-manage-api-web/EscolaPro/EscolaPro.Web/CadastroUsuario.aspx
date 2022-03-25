<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EscolaPro.Web.CadastroUsuario" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
        }
    </style>

    <script>
        function ValidaCPF() {
            var ao_cpf = document.forms.form1.ao_cpf.value;
            var cpfValido = /^(([0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}))$/;
            ao_cpf = ao_cpf.replace(/(\d{3})(\d)/, "$1.$2"); //Coloca um ponto entre o terceiro e o quarto dígitos
            ao_cpf = ao_cpf.replace(/(\d{3})(\d)/, "$1.$2"); //Coloca um ponto entre o terceiro e o quarto dígitos
            //de novo (para o segundo bloco de números)
            ao_cpf = ao_cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2"); //Coloca um hífen entre o terceiro e o quarto dígitos

            var valorValido = document.getElementById("ao_cpf").value = ao_cpf;
        }
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Lista de Usuário</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCadastroUsuario">Adicionar Usuário</a>
                <br />
                <br />
                <div class="form-group">

                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>CPF</th>
                                <th>RG</th>
                                <th>Email</th>
                                <th>Ativo</th>
                                <th>Perfil de Acesso</th>
                                <th>Unidade</th>
                                <th>Departamento</th>
                                <th style="width: 8%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Ricardo Castro</td>
                                <td>222.222.111-44</td>
                                <td>11115564-X</td>
                                <td>ricardo@kaspper.com</td>
                                <td><span class="label label-danger">Não</span></td>
                                <td>Terceiro</td>
                                <td>Jundiaí</td>
                                <td>T.I</td>
                                <td>
                                    <a data-toggle="modal" data-target="#modalNovoPlano"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                </td>
                            </tr>
                            <tr>
                                <td>Francisto</td>
                                <td>555.444.111-44</td>
                                <td>2356555-X</td>
                                <td>francisto@escolamodelo.com</td>
                                <td><span class="label label-success">Sim</span></td>
                                <td>Supervisor</td>
                                <td>Campinas</td>
                                <td>Suporte Central</td>
                                <td>
                                    <a data-toggle="modal" data-target="#modalNovoPlano"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>


            </div>
        </div>
    </div>




    <!-- Modal Cadastro do Usuário -->


    <div class="container">
        <div class="modal fade" id="modalCadastroUsuario" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Informações do Usuário</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#dadosUsuario">Dados do Usuário</a></li>
                                <li><a data-toggle="tab" href="#pessioesUsuario">Perfil do Usuário</a></li>
                            </ul>

                            <div class="tab-content">
                                <div id="dadosUsuario" class="tab-pane fade in active">

                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title"></h3>

                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                            </div>
                                        </div>


                                        <div class="box-body">

                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">CPF:</label>
                                                    <br />
                                                    <form name="form1">
                                                        <input type="text" class="form-control" name="ao_cpf" id="ao_cpf" placeholder="CPF" maxlength="14" onblur="ValidaCPF();" />
                                                    </form>
                                                </div>


                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Data de Nascimento:</label>
                                                    <br />
                                                    <input type="date" class="form-control" id="inputEmail3">
                                                </div>



                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">RG:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="RG">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 20%">
                                                    <label for="cars">Ativo:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>

                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 35%">
                                                    <label for="cars">Nome:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Nome">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Email:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Email">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 22%">
                                                    <label for="cars">Usuario:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Usuário">
                                                </div>

                                            </div>

                                        </div>

                                    </div>
                                </div>


                                <div id="pessioesUsuario" class="tab-pane fade">


                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title"></h3>

                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                            </div>
                                        </div>

                                        <div class="box-body">

                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px; width: 40%">
                                                    <label for="cars">Perfil de Acesso:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione o Perfil</option>
                                                        <option value="1">Administrador</option>
                                                        <option value="2">Supervisor</option>
                                                        <option value="3">Atendente</option>
                                                        <option value="4">Financeiro</option>
                                                        <option value="5">Jurídico</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 25%">
                                                    <label for="cars">Unidade:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione a Unidade</option>
                                                        <option value="1">Campinas</option>
                                                        <option value="2">Jundiaí</option>
                                                        <option value="3">São Paulo</option>
                                                        <option value="4">Sorocaba</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 25%">
                                                    <label for="cars">Departamento:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione o Departamento</option>
                                                        <option value="1">Suporte Central</option>
                                                        <option value="2">Recursos Humanos</option>
                                                        <option value="3">Atendimento</option>
                                                        <option value="4">Financeiro</option>
                                                        <option value="5">Jurídico</option>
                                                        <option value="6">Terceiro</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 25%">
                                                    <label for="cars">Senha:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Senha">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 25%">
                                                    <label for="cars">Confirmar Senha:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Confirmar Senha">
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-footer">
                            <a class="btn btn-success pull-right">Salvar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
