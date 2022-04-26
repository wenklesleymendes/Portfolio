<%@ Page Title="Cadastrar Aluno" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAluno.aspx.cs" Inherits="EscolaPro.Web.CadastrarAluno" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">




    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Dados Pessoais</h3>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
        <form class="form-horizontal">

            <div class="box-body">

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Unidade.:</label>

                    <div class="col-sm-4">

                        <select class="custom-select form-control">
                            <option selected>Selecione uma unidade</option>
                            <option value="1">Campinas</option>
                            <option value="2">Jundiaí</option>
                            <option value="3">São Paulo</option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Nome</label>

                    <div class="col-sm-5">
                        <input type="email" class="form-control" id="inputEmail3" placeholder="Nome">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Data de Nasc.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="Data de Nascimento">
                    </div>

                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Nome da Mãe</label>

                    <div class="col-sm-5">
                        <input type="email" class="form-control" placeholder="Nome da Mãe">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Sexo.:</label>

                    <div class="col-sm-2">

                        <select class="custom-select form-control">
                            <option selected>Sexo.:</option>
                            <option value="1">Masculino</option>
                            <option value="2">Feminino</option>
                        </select>
                    </div>

                </div>



                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">CPF.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="000.000.000-00">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">RG.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="RG">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Órgão Exp.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="Órgão Exp.:">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Estado:</label>

                    <div class="col-sm-1">
                        <input type="email" class="form-control" placeholder="UF">
                    </div>


                </div>


                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Estado Civil.:</label>

                    <div class="col-sm-2">
                        <select class="custom-select form-control">
                            <option selected>Estado Civil</option>
                            <option value="1">Solteiro (a)</option>
                            <option value="2">Casado (a)</option>
                        </select>
                    </div>

                    <label for="inputEmail3" class="col-sm-2 control-label">Nacionalidade.:</label>

                    <div class="col-sm-2">
                        <select class="custom-select form-control">
                            <option selected>Nacionalidade</option>
                            <option value="1">Brasileiro</option>
                            <option value="2">Estrangeiro</option>
                        </select>
                    </div>

                    <label for="inputEmail3" class="col-sm-2 control-label">Naturalidade.:</label>

                    <div class="col-sm-2">
                        <select class="custom-select form-control">
                            <option selected>Naturalidade</option>
                            <option value="1">Campinas</option>
                            <option value="2">Jundiaí</option>
                            <option value="3">São Paulok</option>
                        </select>
                    </div>

                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Título Eleitoral.:</label>

                    <div class="col-sm-4">
                        <input type="email" class="form-control" placeholder="Título Eleitoral">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Zona.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="Zona">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Seção.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="Seção">
                    </div>
                </div>



                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Nome.:</label>

                    <div class="col-sm-4">
                        <input type="email" class="form-control" placeholder="Nome do Responsável">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">RG.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="RG do Responsável">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">CPF.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="CPF do Responsável">
                    </div>
                </div>


            </div>
            <!-- /.box-body -->
            <%--   <div class="box-footer">

                <button type="submit" class="btn btn-info pull-right">Salvar</button>
            </div>--%>
            <!-- /.box-footer -->
        </form>

        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Endereço</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <form class="form-horizontal">
                <div class="box-body">

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label">CEP.:</label>

                        <div class="col-sm-2">
                            <input type="email" class="form-control" placeholder="CEP">
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Rua.:</label>

                        <div class="col-sm-2">
                            <input type="email" class="form-control" placeholder="Rua">
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Número.:</label>

                        <div class="col-sm-1">
                            <input type="email" class="form-control" placeholder="Número">
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Complemento:</label>

                        <div class="col-sm-2">
                            <input type="email" class="form-control" placeholder="Complemento">
                        </div>


                    </div>

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label">Bairro.:</label>

                        <div class="col-sm-3">
                            <input type="email" class="form-control" placeholder="Bairro">
                        </div>

                        <label for="inputEmail3" class="col-sm-2 control-label">Cidade.:</label>

                        <div class="col-sm-3">
                            <input type="email" class="form-control" placeholder="Cidade">
                        </div>


                        <label for="inputEmail3" class="col-sm-1 control-label">Estado.:</label>

                        <div class="col-sm-1">

                            <select class="custom-select form-control">
                                <option selected>UF.:</option>
                                <option value="1">SP</option>
                                <option value="2">RJ</option>
                                <option value="3">MG</option>
                            </select>
                        </div>

                    </div>

                </div>
            </form>
        </div>


        <!-- Dados de contato -->

        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Contato</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <form class="form-horizontal">
                <div class="box-body">

                    <div class="form-group">


                        <label for="inputEmail3" class="col-sm-2 control-label">E-mail.:</label>

                        <div class="col-sm-3">
                            <input type="email" class="form-control" placeholder="E-mail">
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Envio de Email.:</label>

                        <div class="col-sm-1">
                            <select class="custom-select form-control">
                                <option value="1" style="color: red;">Ativo</option>
                                <option value="2" style="color: red">Inativo</option>
                            </select>
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Celular.:</label>

                        <div class="col-sm-2">
                            <input type="email" class="form-control" placeholder="Telefone">
                        </div>

                        <label for="inputEmail3" class="col-sm-1 control-label">Sms.:</label>

                        <div class="col-sm-1">
                            <select class="custom-select form-control">
                                <option value="1" style="color: red;">Ativo</option>
                                <option value="2" style="color: red">Inativo</option>
                            </select>
                        </div>


                    </div>

                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-2 control-label">Telefone fixo.:</label>

                        <div class="col-sm-3">
                            <input type="email" class="form-control" placeholder="Telefone">
                        </div>



                        <label for="inputEmail3" class="col-sm-2 control-label">Como nos conheceu?</label>

                        <div class="col-sm-3">

                            <select class="custom-select form-control">
                                <option selected>Selecione uma opção.:</option>
                                <option value="1">Facebook</option>
                                <option value="2">Site</option>
                                <option value="3">Instagram</option>
                            </select>
                        </div>

                    </div>

                </div>


                <div class="container">

                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Cadastrar Matrícula</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">
                                        <div class="box-body">


                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-sm-2 control-label">Matrícula.:</label>

                                                <div class="col-sm-2">
                                                    <input type="email" class="form-control" placeholder="Matrícula">
                                                </div>

                                                <label for="inputEmail3" class="col-sm-2 control-label">Unidade.:</label>

                                                <div class="col-sm-2">

                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione uma unidade</option>
                                                        <option value="1">Campinas</option>
                                                        <option value="2">Jundiaí</option>
                                                        <option value="3">São Paulo</option>
                                                    </select>
                                                </div>

                                                <label for="inputEmail3" class="col-sm-1 control-label">Status.:</label>

                                                <div class="col-sm-3">

                                                    <select class="custom-select form-control">
                                                        <option value="1">Ativado</option>
                                                        <option value="2">Desativado</option>
                                                    </select>
                                                </div>

                                            </div>


                                            <div class="form-group">

                                                <label for="inputEmail3" class="col-sm-2 control-label">Nome.:</label>

                                                <div class="col-sm-7">
                                                    <input type="email" class="form-control" placeholder="Nome">
                                                </div>




                                            </div>


                                            <div class="form-group">

                                                <label for="inputEmail3" class="col-sm-2 control-label">Curso.:</label>

                                                <div class="col-sm-3">
                                                    <input type="email" class="form-control" placeholder="Curso">
                                                </div>

                                                <label for="inputEmail3" class="col-sm-2 control-label">Ano.:</label>

                                                <div class="col-sm-2">
                                                    <input type="email" class="form-control" placeholder="Ano">
                                                </div>


                                                <label for="inputEmail3" class="col-sm-1 control-label">Semestre.:</label>

                                                <div class="col-sm-2">
                                                    <input type="email" class="form-control" placeholder="Semestre">
                                                </div>



                                            </div>

                                            <div class="form-group">

                                                <label for="inputEmail3" class="col-sm-2 control-label">E-mail.:</label>

                                                <div class="col-sm-3">
                                                    <input type="email" class="form-control" placeholder="E-mail">
                                                </div>

                                                <label for="inputEmail3" class="col-sm-2 control-label">Telefone.:</label>

                                                <div class="col-sm-3">
                                                    <input type="email" class="form-control" placeholder="Telefone">
                                                </div>
                                            </div>

                                            <div class="box-footer">

                                                <a class="btn btn-info pull-right" runat="server" href="~/PainelMatriculaAluno">Salvar</a>

                                            </div>

                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <!-- /.box-body -->
                <div class="box-footer">
                    <a class="btn btn-success pull-right" runat="server" href="~/PainelMatriculaAluno">Continuar</a>

                    <%--                <button type="submit" class="btn btn-info pull-right" data-toggle="modal" data-target="#myModal">Salvar</button>--%>
                </div>
                <!-- /.box-footer -->



            </form>
        </div>

    </div>

</asp:Content>
