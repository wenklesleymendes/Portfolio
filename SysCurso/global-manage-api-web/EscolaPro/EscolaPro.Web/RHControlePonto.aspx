<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RHControlePonto.aspx.cs" Inherits="EscolaPro.Web.RHControlePonto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Controle de Ponto - Funcionário</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <div class="form-group">

                    <div class="btn-group" style="margin: 10px">

                        <label style="margin-left: 30px">Período:</label>

                        <div class="input-group">

                            <i class="fa fa-calendar" style="margin: 10px"></i>

                            <input id="date" type="date">

                            <span style="margin-left: 10px; margin-right: 10px">a</span>

                            <i class="fa fa-calendar" style="margin: 10px"></i>

                            <input id="date" type="date">
                        </div>
                    </div>

                    <div class="btn-group " style="margin: 10px;">
                        <label for="cars">CPF</label>
                        <br />

                        <div class="autocomplete" style="width: 130px;">
                            <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="000.000.000-00">
                        </div>
                        <a class="btn btn-dropbox pull-right"><span class="fas fa-search"></span></a>
                    </div>

                    <div class="btn-group" style="margin: 10px; width: 15%">
                        <label for="cars">Regime:</label>
                        <br />
                        <select id="mySelect" onchange="myFunction()" class="custom-select form-control">
                            <option selected>Selecione o Regime</option>
                            <option value="1">CLT</option>
                            <option value="2">Estágio</option>
                            <option value="3">Autônomo Professor</option>
                            <option value="4">Autônomo</option>
                        </select>
                    </div>


                    <div class="btn-group pull-right" style="margin: 10px; width: 30%">
                        <label for="cars"></label>
                        <br />
                        <a class="btn btn-warning pull-right" data-toggle="modal" data-target="#modalFerias">Conceder Período de Férias</a>
                    </div>

                    <br /><br />

                    <div class="form-group">

                        <div class="btn-group" style="margin: 10px; width: 30%">
                            <label for="cars">Nome:</label>
                            <span class="label label-primary" style="font-size: small">João Henrique Silveira Cardoso</span>
                        </div>

                        <div class="btn-group" style="margin: 10px; width: 20%">
                            <label for="cars">Matrícula:</label>
                            <span class="label label-primary" style="font-size: small">023564</span>
                        </div>

                        <div class="btn-group" style="margin: 10px; width: 20%">
                            <label for="cars">Unidade:</label>
                            <span class="label label-primary" style="font-size: small">Campinas</span>
                        </div>

                        <div class="btn-group" style="margin: 10px; width: 20%">
                            <label for="cars">Regime:</label>
                            <span class="label label-primary" style="font-size: small">CLT</span>
                        </div>

                    </div>

                </div>

                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Data</th>
                            <th>Entrada 1</th>
                            <th>Saída 1</th>
                            <th>Entrada 2</th>
                            <th>Saída 2</th>
                            <th>Entrada 3</th>
                            <th>Saída 3</th>
                            <th>Entrada 4</th>
                            <th>Saída 4</th>
                            <th>Saldo</th>
                            <th style="width: 20px">Ocorrências</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>01/04/2020</td>
                            <td>08:00</td>
                            <td>12:00</td>
                            <td>13:00</td>
                            <td>17:30</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-success">00:30</span></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>02/04/2020</td>
                            <td>09:30</td>
                            <td>12:00</td>
                            <td>13:00</td>
                            <td>17:00</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-danger">01:30</span></td>
                            <td class="text-center"><a data-toggle="modal" data-target="#modalOcorrencias"><span class="fas fa-exclamation-triangle" style="color: gold; font-size: medium"></span></a></td>
                        </tr>
                        <tr>
                            <td>03/04/2020</td>
                            <td>08:00</td>
                            <td>12:00</td>
                            <td>13:00</td>
                            <td>17:00</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-primary">00:00</span></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>04/04/2020</td>
                            <td>08:00</td>
                            <td>12:00</td>
                            <td>13:00</td>
                            <td>17:00</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-primary">00:00</span></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>05/04/2020</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-danger">08:00</span></td>
                            <td class="text-center"><a data-toggle="modal" data-target="#modalOcorrencias"><span class="fas fa-exclamation-triangle" style="color: gold; font-size: medium"></span></a></td>
                        </tr>
                        <tr>
                            <td>06/04/2020</td>
                            <td>08:00</td>
                            <td>12:00</td>
                            <td>13:00</td>
                            <td>15:00</td>
                            <td>17:00</td>
                            <td>18:00</td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-success">00:30</span></td>
                            <td class="text-center"><a data-toggle="modal" data-target="#modalOcorrencias"><span class="fas fa-exclamation-triangle" style="color: gold; font-size: medium"></span></a></td>
                        </tr>
                        <tr>
                            <td>07/04/2020</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-warning">Férias</span></td>
                            <td class="text-center"></td>
                        </tr>
                        <tr>
                            <td>08/04/2020</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-warning">Férias</span></td>
                            <td class="text-center"></td>
                        </tr>
                        <tr>
                            <td>09/04/2020</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-warning">Férias</span></td>
                            <td class="text-center"></td>
                        </tr>
                        <tr>
                            <td>10/04/2020</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><span class="label label-warning">Férias</span></td>
                            <td class="text-center"></td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="form-group">

                    <div class="btn-group" style="margin: 10px;">
                        <label for="cars">Férias:</label>
                        <span class="label label-success" style="font-size: small">Em dia</span>
                    </div>

                    <div class="btn-group pull-right" style="margin: 10px;">
                        <label for="cars">Saldo Devedor:</label>
                        <span class="label label-danger" style="font-size: small">01:30</span>
                    </div>
                </div>


            </div>
        </div>
    </div>


    <!-- Modal Modal de Ocorrências -->
    <div class="container">
        <div class="modal fade" id="modalOcorrencias" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Controle de Ponto - Ocorrências</h4>
                    </div>

                    <div class="modal-body">


                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Dados do Ponto</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <div class="form-group">

                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars" style="color: dodgerblue">Nome</label>
                                            <br />
                                            <label for="cars">João Henrique Silveira Cardoso</label>
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars" style="color: dodgerblue">Unidade</label>
                                            <br />
                                            <label for="cars">Campinas</label>
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars" style="color: dodgerblue">Matrícula</label>
                                            <br />
                                            <label for="cars">023564</label>
                                        </div>

                                    </div>

                                    <div class="form-group">

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Entrada 1</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Entrada">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Saída 1</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Saída">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Entrada 2</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Entrada">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Saída 2</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Saída">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Entrada 3</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Entrada">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Saída 3</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Saída">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Entrada 4</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Entrada">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Saída 4</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Saída">
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars">Tipo da Ocorrência</label>
                                            <br />
                                            <select class="custom-select form-control">
                                                <option value="1">Falta</option>
                                                <option value="2">Atestado</option>
                                                <option value="3">Declaração</option>
                                                <option value="4">Atraso</option>
                                                <option value="5">Horas Extras</option>
                                            </select>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Observações</label>
                                            <br />
                                            <textarea style="width: 800px"></textarea>
                                        </div>

                                        <div class="btn-group" style="margin: 10px">
                                            <br />
                                            <label for="cars">Atestado ou Justificativa:</label>
                                            <br />

                                            <a class="btn btn-dropbox">
                                                <span class="fas fa-file-upload" style="margin-right: 10px; font-size: large"></span>
                                                Upload do Arquivo</a>
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
    </div>


    <!-- Modal Modal de Ocorrências -->
    <div class="container">
        <div class="modal fade" id="modalFerias" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Período de Férias</h4>
                    </div>

                    <div class="modal-body">

                        <div class="box box-info">


                            <div class="btn-group" style="margin: 10px">

                                <label style="margin-left: 30px">Período:</label>

                                <div class="input-group">

                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                    <input id="date" type="date">

                                    <span style="margin-left: 10px; margin-right: 10px">a</span>

                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                    <input id="date" type="date">
                                </div>
                            </div>


                            <div class="btn-group" style="margin: 10px">

                                <label for="cars">Aviso de Férias:</label>
                                <br />

                                <a class="btn btn-dropbox">
                                    <span class="fas fa-file-upload" style="margin-right: 10px; font-size: large"></span>
                                    Upload do Arquivo</a>
                            </div>

                            <div class="btn-group" style="margin: 10px;">
                                <label for="cars">Observações</label>
                                <br />
                                <textarea style="width: 850px; height: 100px"></textarea>
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 95%">
                                <br />
                                <a class="btn btn-success pull-right">Adicionar</a>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
