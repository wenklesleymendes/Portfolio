<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CriarMetas.aspx.cs" Inherits="EscolaPro.Web.CriarMetas" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Criação de Metas</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Busca Avançada<span class="fas fa-sort-amount-down" style="margin-left: 10px"></span></a>

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarMeta">Adicionar Meta</a>
                <br />
                <br />

                <div class="collapse" id="collapseExample">
                    <div class="card card-body">

                        <div class="form-group">

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Unidade:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option value="1">Campinas</option>
                                    <option value="2">Jundiaí</option>
                                    <option value="3">São Paulo</option>
                                </select>
                            </div>


                            <div class="    " style="margin: 10px">

                                <label style="margin-left: 30px">Período:</label>

                                <div class="input-group">

                                    <i class="fa fa-calendar" style="margin: 10px"></i>


                                    <input id="date" type="date">

                                    <span style="margin-left: 10px; margin-right: 10px">a</span>

                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                    <input id="date" type="date">
                                </div>
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars"></label>
                                <br />
                                <a class="btn btn-dropbox"><span class="fa fa-search" style="margin-right: 10px"></span>Buscar</a>
                            </div>
                        </div>
                    </div>
                </div>

                <br />
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Unidade</th>
                            <th>Início da Meta</th>
                            <th>Término da Meta</th>
                            <th>Meta</th>
                            <th>Bônus Meta Período</th>
                            <th style="width: 2%"></th>
                            <th style="width: 2%"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Campinas</td>
                            <td>05/01/2020</td>
                            <td>28/07/2020</td>
                            <td><span class="label label-success">361 Matrículas</span></td>
                            <td><span class="label label-danger">R$ 200,00</span></td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Jundiaí</td>
                            <td>16/06/2020</td>
                            <td>31/12/2020</td>
                            <td><span class="label label-success">460 Matrículas</span></td>
                            <td><span class="label label-danger">R$ 500,00</span></td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>
    </div>



    <!-- Modal de Adicionar Meta -->

    <div class="container">
        <div class="modal fade" id="modalCriarMeta" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Nova Meta</h4>
                    </div>

                    <div class="modal-body">

                        <div class="box box-info">

                            <div class="form-group">

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars">Unidade:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>
                                </div>


                                <div class="btn-group" style="margin: 10px">

                                    <label style="margin-left: 30px">Período da Meta:</label>

                                    <div class="input-group">

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">

                                        <span style="margin-left: 10px; margin-right: 10px">a</span>

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">
                                    </div>
                                </div>

                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars">Total de Matrículas Meta</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="Quantidade">
                                </div>


                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars">Bônus Meta Período</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="R$">
                                </div>



                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Mês e Ano</th>
                                            <th style="width: 150px">Quantidade de Matrículas</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Novembro/2019</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="Quantidade"></td>
                                        </tr>
                                        <tr>
                                            <td>Dezembro/2019</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="Quantidade"></td>
                                        </tr>
                                        <tr>
                                            <td>Janeiro/2020</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="Quantidade"></td>
                                        </tr>
                                        <tr>
                                            <td>Fevereiro/2020</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="Quantidade"></td>
                                        </tr>
                                    </tbody>
                                </table>

                                <div class="btn-group" style="margin: 10px; width:67%">
                                </div>

                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars">Total Restante:</label>
                              
                                    <span class="label label-danger" style="font-size:small">300</span>
                                </div>


                            </div>

                        </div>

                        <div class="box-footer">
                            <a class="btn btn-success pull-right">Salvar Meta</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

