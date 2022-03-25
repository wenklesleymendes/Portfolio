<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CriarAgenda.aspx.cs" Inherits="EscolaPro.Web.CriarAgenda" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        .item2 {
            grid-area: menu;
        }

        .item3 {
            grid-area: main;
        }



        .item5 {
            grid-area: footer;
        }

        .grid-container-pagamento {
            display: grid;
            /*grid-template-areas: 'header header header header header header' 'menu main main main right right' 'menu footer footer footer footer footer';*/
            grid-template-areas: 'menu menu menu main main main' 'menu menu menu footer footer footer';
            grid-gap: 5px;
            padding: 5px;
        }

            .grid-container-pagamento > div {
                text-align: center;
                padding: 1px 0;
            }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Criar Agenda de Provas</h3>
            <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalEditar"><i class="fa fa-plus" style="margin-right: 5px"></i>Criar Agenda</a>
        </div>

        <form class="form-horizontal">

            <div class="box-body">

                <table id="example2" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 30%">Descrição</th>
                            <th style="width: 25%">Unidade</th>
                            <th>Data da Agenda</th>
                            <th>Vagas para Prova</th>
                            <th style="width:8%"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Prova Geral</td>
                            <td>Campinas</td>
                            <td>28/03/2020</td>
                            <td>45</td>
                            <td>
                                <a data-toggle="modal" data-target="#modalEditar"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </form>
    </div>


    <div class="container">

        <div class="modal fade" id="modalEditar" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Agenda de Prova</h4>
                    </div>
                    <div class="modal-body">

                        <form class="form-horizontal">

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Informações da Agenda</h3>
                                </div>

                                <div class="box-body">
                                    <div class="form-group">

                                        <label for="inputEmail3" class="col-sm-5 control-label">Inicio da Inscrição.:</label>

                                        <div class="col-sm-6">

                                            <div class="input-group">

                                                <i class="fa fa-calendar" style="margin: 10px"></i>

                                                <input id="date" type="date">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label for="inputEmail3" class="col-sm-5 control-label">Término da Inscrição.:</label>

                                        <div class="col-sm-6">

                                            <div class="input-group">

                                                <i class="fa fa-calendar" style="margin: 10px"></i>

                                                <input id="date" type="date">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label for="inputEmail3" class="col-sm-5 control-label">Data da Prova.:</label>

                                        <div class="col-sm-6">

                                            <div class="input-group">

                                                <i class="fa fa-calendar" style="margin: 10px"></i>

                                                <input id="date" type="date">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label for="inputEmail3" class="col-sm-5 control-label">Nome do Colégio Autorizado.:</label>

                                        <div class="col-sm-6">

                                            <input type="email" class="form-control" id="inputEmail3" placeholder="Colégio Autorizado">
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label for="inputEmail3" class="col-sm-5 control-label">Endereço do Colégio Autorizado.:</label>

                                        <div class="col-sm-6">

                                            <input type="email" class="form-control" id="inputEmail3" placeholder="Endereço do Colégio Autorizado">
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-5 control-label">Vagas para Prova.:</label>

                                        <div class="col-sm-4">
                                            <input type="email" class="form-control" id="inputEmail3" placeholder="Quantidade">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-5 control-label">Unidade Participante.:</label>

                                        <div class="col-sm-4">
                                            <select class="custom-select form-control">
                                                <option selected>Unidade</option>
                                                <option value="1">Campinas</option>
                                                <option value="2">Jundiaí</option>
                                                <option value="3">São Paulo</option>
                                            </select>
                                        </div>
                                        <div class="col-sm-1">
                                            <a class="btn btn-success">Adicionar</a>
                                        </div>
                                    </div>

                                    <div class="box-body">
                                        <h4>Unidades Participantes da Prova</h4>


                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Unidade</th>
                                                    <th>Horário de Saída</th>
                                                    <th>Local de Saída</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Campinas</td>
                                                    <td>
                                                        <input type="email" class="form-control" placeholder="Horário"></td>
                                                    <td>
                                                        <input type="email" class="form-control" placeholder="Local"></td>
                                                </tr>
                                                <tr>
                                                    <td>Jundiaí</td>
                                                    <td>
                                                        <input type="email" class="form-control" placeholder="Horário"></td>
                                                    <td>
                                                        <input type="email" class="form-control" placeholder="Local"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>

                                <br />
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <a class="btn btn-warning pull-left" style="margin-left: 5%">Carregar Dados da Prova Anterior</a>
                                            <a class="btn btn-success col-sm-3 pull-right" style="margin-right: 5%">Criar</a>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
