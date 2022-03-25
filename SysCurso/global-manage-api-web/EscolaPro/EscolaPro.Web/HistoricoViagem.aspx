<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HistoricoViagem.aspx.cs" Inherits="EscolaPro.Web.HistoricoViagem" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Histórico de Viagem</h3>

        </div>

        <form class="form-horizontal">

            <div class="box-body">

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-1 control-label">Unidade.:</label>

                    <div class="col-sm-2">

                        <select class="custom-select form-control">
                            <option selected>Selecione uma unidade</option>
                            <option value="1">Campinas</option>
                            <option value="2">Jundiaí</option>
                            <option value="3">São Paulo</option>
                        </select>
                    </div>

                    <label for="inputEmail3" class="col-sm-2 control-label">Data da Viagem.:</label>

                    <div class="col-sm-2">
                        <div class="input-group">
                            <i class="fa fa-calendar" style="margin: 10px"></i>
                            <input id="date" type="date">
                        </div>
                    </div>

                    <label for="inputEmail3" class="col-sm-2 control-label">Nº do Ônibus.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" placeholder="Número">
                    </div>


                </div>

                <div class="form-group">
                    <div class="col-sm-8">
                    </div>
                    
                    <div class="col-sm-2">
                        <a class="btn btn-success pull-right"><i class="fa fa-print" style="margin-right: 10px; margin-left: 10px"></i>Imprimir</a>
                    </div>
                    <div class="col-sm-1">
                        <a class="btn btn-dropbox"><i class="fa fa-search" style="margin-right: 5px;"></i>Buscar</a>
                    </div>
                </div>

                <br />

                <table id="example2" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Nº do Ônibus</th>
                            <th>Transportadora</th>
                            <th>Unidade</th>
                            <th>Data da Viagem</th>
                            <th>Local de Destino</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>252980</td>
                            <td>Mimo</td>
                            <td>Campinas</td>
                            <td>22/04/2020</td>
                            <td>São Paulo</td>
                            <td>
                                <a class="fa fa-search" href="AgendaProva.aspx" style="border-color: transparent; background-color: transparent"></a>
                                <a class="fa fa-envelope-open" style="border-color: transparent; margin-left: 20px" data-toggle="modal" data-target="#visualizarModal"></a>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <br />


            </div>
        </form>
    </div>

    <!-- Modal de Historico de viagem -->
    <div class="container">
        <div class="modal fade" id="visualizarModal" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Detalhes do E-mail</h4>
                    </div>

                    <div class="modal-body">
                        <div class="box box-info">
                            <table id="example2" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Data do Envio</th>
                                        <th>06/04/2020</th>
                                    </tr>
                                    <tr>
                                        <th>Horário do Envio</th>
                                        <th>15:45</th>
                                    </tr>
                                    <tr>
                                        <th>Usuário</th>
                                        <th>Francisco</th>
                                    </tr>
                                    <tr>
                                        <th>Quem enviou</th>
                                        <th>escolamodelocampinas@escolamodelo.com.br</th>
                                    </tr>
                                    <tr>
                                        <th>Enviado Para</th>
                                        <th>viacao@email.com.br</th>
                                    </tr>
                                </thead>
                            </table>
                            <div class="box-body">
                                <textarea class="control-label" style="height: 226px; width: 554px">Texto com o conteúdo do corpo do email</textarea>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-7"></div>
                                <label for="inputEmail3" class="col-sm-4 control-label">
                                    Relação da Viagem
                                  <a class="fa fa-paperclip" style="font-size: x-large"></a>
                                </label>
                            </div>
                            <br />
                            <br />

                        </div>


                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
