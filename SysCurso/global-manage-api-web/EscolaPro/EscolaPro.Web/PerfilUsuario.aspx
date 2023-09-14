<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PerfilUsuario.aspx.cs" Inherits="EscolaPro.Web.PerfilUsuario" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
        }

        .weekDays-selector input {
            display: none !important;
        }

            .weekDays-selector input[type=checkbox] + label {
                display: inline-block;
                border-radius: 6px;
                background: #dddddd;
                height: 40px;
                width: 200px;
                margin-right: 3px;
                line-height: 40px;
                text-align: center;
                cursor: pointer;
            }

            .weekDays-selector input[type=checkbox]:checked + label {
                background: #2b61b3;
                color: #ffffff;
            }

            .select2-container--default .select2-selection--multiple .select2-selection__choice{
                color:blue;
            }
    </style>


</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Criação de Perfis</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarPerfil">Adicionar Novo Perfil</a>
                <br />
                <br />

                <div class="form-group">

                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Nome do Perfil</th>
                                <th>Nível de Acesso</th>
                                <th>Liberar Acesso</th>
                                <th>Ativo</th>
                                <th>Horário de Acesso</th>
                                <th style="width: 8%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Supervisor</td>
                                <td>Controle Total</td>
                                <td>
                                    <ul>
                                        <li class="list-unstyled"><span class="label label-success">Alunos</span></li>
                                        <li class="list-unstyled"><span class="label label-success">Ticket</span></li>
                                    </ul>
                                </td>
                                <td><span class="label label-danger">Não</span></td>
                                <td>
                                    <ul>
                                        <li class="list-unstyled"><span class="label label-primary">Semana - 08:00 as 22:00</span></li>
                                        <li class="list-unstyled"><span class="label label-primary">Sábado - 07:00 as 13:00</span></li>
                                    </ul>
                                </td>
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



    <!-- Modal Criação de Perfil -->


    <div class="container">
        <div class="modal fade" id="modalCriarPerfil" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Perfil do Usuário</h4>
                    </div>


                    <div class="modal-body">
                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados do Perfil</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars">Nome do Perfil:</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Nome do Perfil">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 25%">
                                        <label for="cars">Nível de Acesso:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione o Nível de Acesso</option>
                                            <option value="1">Controle Total</option>
                                            <option value="3">Leitura</option>
                                            <option value="4">Ler e Editar</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Ativo:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Sim</option>
                                            <option value="2">Não</option>
                                        </select>
                                    </div>

                               

                                </div>
                            </div>
                        </div>




                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Permissões de Páginas</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                     <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Aluno</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Consultar alunos</option>
                                            <option>Cadastrar novo aluno</option>
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Comunicação</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Relatórios</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                    

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Portal do Administrador</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Cadastro de Usuários</option>
                                            <option>Permissões de Usuários</option>
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>


                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Ticket</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Painel de Ticket</option>
                                            <option>Administração de Ticket</option>
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Financeiro</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Provas</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Criar Agenda de Provas</option>
                                            <option>Lista de Passageiros</option>
                                            <option>Histórico de Viagem</option>
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Gerenciador</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Cursos e Turmas</option>
                                            <option>Unidades</option>
                                            <option>Planos de Pagamentos</option>
                                            <option>Promoções, Bolsa e Convênio</option>
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>


                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label>Portal do Aluno</label>
                                        <select class="form-control select2" multiple="multiple" data-placeholder="" style="width: 100%;">
                                            <option>Acesso Total</option>
                                        </select>
                                    </div>

                            </div>
                        </div>




                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Horário de Acesso</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">Segunda a Sexta:</label>

                                    <div class="col-sm-2">
                                        <input type="time" id="appt" name="appt" class="form-control"
                                            min="09:00" max="18:00" required>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                    <div class="col-sm-2">
                                        <input type="time" id="appt" name="appt" class="form-control"
                                            min="09:00" max="18:00" required>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">Sábado:</label>

                                    <div class="col-sm-2">
                                        <input type="time" id="appt" name="appt" class="form-control"
                                            min="09:00" max="18:00" required>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                    <div class="col-sm-2">
                                        <input type="time" id="appt" name="appt" class="form-control"
                                            min="09:00" max="18:00" required>
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

