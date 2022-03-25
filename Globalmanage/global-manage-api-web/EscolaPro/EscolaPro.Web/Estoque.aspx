<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Estoque.aspx.cs" Inherits="EscolaPro.Web.Estoque" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Estoque</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarEstoque">Adicionar</a>
                <br />
                <br />

                <div class="form-group">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Unidade</th>
                                <th>Nome do Produto</th>
                                <th>Código Interno</th>
                                <th>Código NCM</th>
                                <th>Nota Fiscal</th>
                                <th>Qtde. Entrada</th>
                                <th>Qtde. Saída</th>
                                <th>Estoque</th>
                                <th style="width: 8%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Jundiaí</td>
                                <td>Material Didático</td>
                                <td>001</td>
                                <td>789756123</td>
                                <td>789654321</td>
                                <td><span class="label label-success">500</span></td>
                                <td><span class="label label-warning">250</span></td>
                                <td><span class="label label-danger">250</span></td>
                                <td>
                                    <i data-toggle="tooltip" class="fas fa-edit" style="color: green; font-size: large" title="Editar"></i>
                                    <a data-toggle="modal" data-target="#modalHistorico"><i data-toggle="tooltip" class="fas fa-search" style="margin-left: 5px; color: dodgerblue; font-size: large" title="Visualizar"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                </td>
                            </tr>
                            <tr>
                                <td>Campinas</td>
                                <td>Material Didático</td>
                                <td>001</td>
                                <td>789756123</td>
                                <td>789654321</td>
                                <td><span class="label label-success">500</span></td>
                                <td><span class="label label-warning">500</span></td>
                                <td><span class="label label-danger">Sem Estoque</span></td>
                                <td>
                                    <i data-toggle="tooltip" class="fas fa-edit" style="color: green; font-size: large" title="Editar"></i>
                                    <a data-toggle="modal" data-target="#modalHistorico"><i data-toggle="tooltip" class="fas fa-search" style="margin-left: 5px; color: dodgerblue; font-size: large" title="Visualizar"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </div>

            </div>

        </div>
    </div>


    <!-- Modal Criar Estoque -->

    <div class="container">
        <div class="modal fade" id="modalCriarEstoque" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Novo Item</h4>
                    </div>


                    <div class="modal-body">
                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados de Estoque</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars">Nome do Produto</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nome do Produto">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Data de Entrada</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Quantidade</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Lote">
                                    </div>


                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars">Nome do Fornecedor</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nome do Fornecedor">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars">CNPJ</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="CNPJ">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 35%">
                                        <label for="cars">Unidade:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione uma Unidade</option>
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Código NCM</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Código NCM">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Código Interno</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Código Interno">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 90%">
                                        <label for="cars">Nota Fiscal</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nota Fiscal">
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



    <!-- Modal Criar Estoque -->

    <div class="container">
        <div class="modal fade" id="modalHistorico" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Histórico de Estoque</h4>
                    </div>


                    <div class="modal-body">

                        <div class="container">
                            <div class="col-md-9">
                                <!-- The time line -->
                                <ul class="timeline">
                                    <!-- timeline time label -->
                                    <li class="time-label">
                                        <span class="bg-red">Entrada de Material: 21 Março 2020
                                        </span>
                                    </li>
                                    <!-- /.timeline-label -->
                                    <!-- timeline item -->
                                    <li>
                                        <i class="fa fa-exclamation bg-yellow"></i>

                                        <div class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i>11:15</span>

                                            <h3 class="timeline-header"><a href="#">Unidade de Jundiaí</a> Entrada de 500 Apostilas</h3>

                                            <div class="timeline-body">
                                            </div>

                                        </div>
                                    </li>

                                    <li class="time-label">
                                        <span class="bg-red">Saída de item: 25 Março 2020
                                        </span>
                                        <div class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i>11:15</span>

                                            <h3 class="timeline-header"><a href="#">Matrícula</a> Realizada  Matrícula Nº 1025 - Apostila Gratuita.</h3>

                                            <div class="timeline-body">
                                            </div>

                                        </div>

                                        <div class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i>12:37</span>

                                            <h3 class="timeline-header"><a href="#">Matrícula</a> Realizada  Matrícula Nº 1026 - Apostila Pago.</h3>

                                            <div class="timeline-body">
                                            </div>

                                        </div>
                                    </li>
                                    <!-- timeline item -->
                                    <li>
                                        <i class="fa fa-clock-o bg-gray"></i>
                                    </li>
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>





</asp:Content>
