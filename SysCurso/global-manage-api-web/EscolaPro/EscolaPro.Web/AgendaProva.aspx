<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AgendaProva.aspx.cs" Inherits="EscolaPro.Web.AgendaProva" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Pesquisar Agenda de Provas</h3>

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

                    <label for="inputEmail3" class="col-sm-2 control-label">Data de Prova.:</label>

                    <div class="col-sm-2">

                        <div class="input-group">

                            <i class="fa fa-calendar" style="margin: 10px"></i>

                            <input id="date" type="date">
                        </div>

                    </div>


                    <div class="col-sm-5">
                        <button type="button" class="btn btn-dropbox btn-sm" style="float: right" data-toggle="modal" data-target="#myModal">
                            <span class="fa fa-search" style="margin-right: 10px"></span>Consultar
                        </button>
                    </div>

                </div>
                <br />
                <br />
                <div class="form-group">
                    <div class="col-sm-12">
                        <a class="btn btn-success pull-left" data-toggle="modal" data-target="#modalEnviarTransportadora"><i class="fa fa-envelope" style="margin-right: 5px"></i>Enviar para Transportadora</a>
                        <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalListaInscritos"><i class="fa fa-users" style="margin-right: 5px"></i>Lista de Inscritos</a>
                    </div>
                </div>
                <br />
            </div>
        </form>

    </div>


    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Ônibus - 01</h3>

            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>


        <div class="box-body">

            <div class="box-body">
                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalTransportadora">Dados da Viagem</a>
            </div>

            <table id="example2" class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Presença</th>
                        <th>Nome</th>
                        <th>Unidade</th>
                        <th>Status Matrícula</th>
                        <th>Curso</th>
                        <th>Data da Prova</th>
                        <th>Local da Prova</th>
                        <th>Horário de Saída</th>
                        <th>Ônibus</th>
                        <th>Transferir</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkbox" /></td>
                        <td>Andre Coutinho</td>
                        <td>Campinas</td>
                        <td>25298</td>
                        <td>Ensino Fundamental</td>
                        <td>17/04/2020</td>
                        <td>Campinas</td>
                        <td>08:00</td>
                        <td>Ônibus 01</td>
                        <td>
                            <a class="btn btn-success" data-toggle="modal" data-target="#modalTransferirAluno"><i class="fa fa-bus" style="margin-right:10px"></i> Transferir Aluno</a>
                        </td>
                    </tr>
                </tbody>
            </table>
            <a class="btn btn-success pull-right">Salvar Lista de Presença</a>
        </div>
    </div>

    <!-- -->
    <div class="container">

        <div class="modal fade" id="modalTransferirAluno" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">

                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <br />
                    </div>
                    <div class="box-body">
                        <h5>Transferir aluno para outro ônibus</h5>

                        <div class="form-group">
                            
                            <div class="col-sm-6">
                                <select class="custom-select form-control">
                                    <option selected="selected">Selecione o ônibus de destino</option>
                                    <option value="1">Ônibus 01</option>
                                    <option value="2">Ônibus 02</option>
                                    <option value="3">Ônibus 03</option>
                                </select>

                                
                            </div>
                            <a class="btn btn-success col-sm-3">Transferir Aluno</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="container">

        <div class="modal fade" id="modalEnviarTransportadora" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">

                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <br />
                    </div>
                    <div class="box-body">
                        <h4>Enviar para Transportadora</h4>

                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Enviar e-mail</h3>
                            </div>
                            <div class="box-body">
                                <div class="form-group">
                                    <input class="form-control" placeholder="Para: transportadora1@email.com; transportadora2@email.com;">
                                </div>
                            </div>
                        </div>

                        <div class="box box-info">
                            <br />
                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Enviar</th>
                                        <th>Número do Ônibus</th>
                                        <th>Transportadora</th>
                                        <th>Local de Saída</th>
                                        <th>Horário de Saída</th>
                                        <th>Local de Destino</th>
                                        <th>Unidade</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <input type="checkbox" class="checkbox" />
                                        </td>
                                        <td>2645852</td>
                                        <td>Mimo</td>
                                        <td>Campo Limpo Paulista</td>
                                        <td>25298</td>
                                        <td>08:30</td>
                                        <td>Campinas</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <a class="btn btn-linkedin pull-right"><i class="fa fa-envelope" style="margin-right: 5px"></i>Enviar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="container">

        <div class="modal fade" id="modalTransportadora" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <br />
                    </div>

                    <div class="modal-body">
                        <form class="form-horizontal">

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Dados do Monitor</h3>
                                </div>

                                <div class="box-body">

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Nome do Monitor:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" id="inputEmail3" placeholder="Nome">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">RG.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Número do RG">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Celular.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Celular">
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <br />

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Dados da Viagem</h3>
                                </div>

                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Nome da Transportadora.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Nome da Transportadora">
                                        </div>


                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Nome do Motorista.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Nome do Motorista">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Contato do Motorista.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Telefone">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Quantidade de Vagas no Ônibus.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Quantidade">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Local de Saída.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Local de Saída">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Horário de Saída.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Horário de Saída">
                                        </div>


                                    </div>

                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Local de Destino.:</label>

                                        <div class="col-sm-6">
                                            <input type="email" class="form-control" placeholder="Local de Saída">
                                        </div>


                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <a class="btn btn-success pull-right">Salvar</a>
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


    <div class="container">
        <div class="modal fade" id="modalListaInscritos" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <br />
                    </div>
                    <div class="box-body">
                        <h4>Lista de Inscritos</h4>
                        <div class="box box-info">
                            <br />
                            <div class="form-group">
                                <div class="col-sm-7"></div>
                                <div class="col-sm-3">
                                    <select class="custom-select form-control">
                                        <option selected>Selecione tipo de lista</option>
                                        <option value="1">Lista Simples</option>
                                        <option value="2">Lista Geral</option>
                                        <option value="2">Lista Para Matrícula Colégio Autorizado</option>
                                    </select>
                                </div>

                                <div class="col-sm-2">
                                    <a class="btn btn-dropbox">Buscar Lista</a>
                                </div>
                            </div>
                            <br />
                            <br />
                            <table class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Nome</th>
                                        <th>R.G</th>
                                        <th>Curso</th>
                                        <th>Status da Prova</th>
                                        <th>Unidade</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>ADAILSON TANAN PEREIRA</td>
                                        <td>21.909.787-2</td>
                                        <td>ENSINO FUNDAMENTAL E MÉDIO</td>
                                        <td>OK</td>
                                        <td>PIRACICABA</td>
                                    </tr>
                                    <tr style="background-color: yellow">
                                        <td>ADRIANA PEREIRA DOS SANTOS</td>
                                        <td>54.786.310-X</td>
                                        <td>ENSINO FUNDAMENTAL E MÉDIO</td>
                                        <td>REPROVA</td>
                                        <td>JUNDIAI</td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="form-group">
                                <div class="col-sm-6">
                                </div>
                                <div class="col-sm-3">
                                    <select class="custom-select form-control">
                                        <option selected>Tipo de Arquivo</option>
                                        <option value="1">PDF</option>
                                        <option value="2">Excel</option>
                                        <option value="3">XML</option>
                                    </select>
                                </div>
                                <div class="col-sm-2">
                                    <a class="btn btn-pinterest">Baixar Arquivo</a>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
