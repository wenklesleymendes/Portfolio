<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracaoTicket.aspx.cs" Inherits="EscolaPro.Web.AdministracaoTicket" %>

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
            <h3 class="box-title">Ticket - Administração</h3>
        </div>

        <form class="form-horizontal">

            <div class="box-body">

                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <h3>150</h3>

                                <p>Total de Ocorrências</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-paperclip"></i>
                            </div>
                            <a href="#" class="small-box-footer">Ver detalhes <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <h3>53<sup style="font-size: 20px">%</sup></h3>

                                <p>Resolvidos</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-checkmark-circled"></i>
                            </div>
                            <a href="#" class="small-box-footer">Ver detalhes <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <h3>44</h3>

                                <p>Abertos</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-thumbsup"></i>
                            </div>
                            <a href="#" class="small-box-footer">Ver detalhes <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-red">
                            <div class="inner">
                                <h3>65</h3>

                                <p>Atrasado</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-android-alert"></i>
                            </div>
                            <a href="#" class="small-box-footer">Ver detalhes <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>

            </div>

            <div class="box-body">

                <div class="row">

                    <div class="col-lg-6 col-xs-6">

                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Criação de Assuntos</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#assuntoModal">Adicionar Assunto</a>
                                <br />
                                <br />
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th style="width: 45%;">Descrição</th>
                                            <th>SLA</th>
                                            <th style="width: 15%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Suporte</td>
                                            <td>Documentação</td>
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

                    <div class="col-lg-6 col-xs-6">

                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Detalhes dos Tickets</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Data da Abertura</th>
                                            <th>Data do Atendimento</th>
                                            <th>SLA</th>
                                            <th>Atendente</th>
                                            <th>Usuário Responsável</th>
                                            <th>Visualizar</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>21/03/2020 08:34</td>
                                            <td>21/03/2020 11:15</td>
                                            <td>Em Dia</td>
                                            <td>Francisco</td>
                                            <td>Ricardo Castro</td>
                                            <td><a data-toggle="modal" data-target="#ticketModal"><i class="fa fa-search" style="margin-left: 5px"></i></a></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


    <!-- Adicionar assunto -->
    <div class="container">
        <div class="modal fade" id="assuntoModal" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Assunto</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">

                            <form class="form-horizontal">
                                <div class="form-group">


                                    <label for="inputEmail3" class="col-sm-3 control-label">Descrição.:</label>

                                    <div class="col-sm-8">
                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Descrição">
                                    </div>

                                </div>

                                <div class="form-group">

                                    <label for="inputEmail3" class="col-sm-4 control-label">Tempo de Atendimento.:</label>

                                    <div class="col-sm-4">
                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Tempo">
                                    </div>

                                    <div class="col-sm-3">
                                        <a class="btn btn-success pull-right">Salvar</a>
                                    </div>

                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="container">

        <div class="modal fade" id="ticketModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Timeline do Ticket</h4>
                    </div>
                    <div class="modal-body">

                        <div class="container">
                            <div class="col-md-9">
                                <!-- The time line -->
                                <ul class="timeline">
                                    <!-- timeline time label -->
                                    <li class="time-label">
                                        <span class="bg-red">Abertura do Chamada 21 Março 2020
                                        </span>
                                    </li>
                                    <!-- /.timeline-label -->
                                    <!-- timeline item -->
                                    <li>
                                        <i class="fa fa-exclamation bg-yellow"></i>

                                        <div class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i>11:15</span>

                                            <h3 class="timeline-header"><a href="#">Unidade de Jundiaí</a> Abertor por Ricardo Castro</h3>

                                            <div class="timeline-body">
                                                Segue documento de RG do Aluno Fernando da Silva, Matrícula 026546
                                            </div>
                                            <div class="timeline-footer">
                                                <a class="btn btn-primary btn-xs">Baixar Anexo</a>
                                                <a class="btn btn-danger btn-xs">Apagar</a>
                                            </div>
                                        </div>
                                    </li>
                                    <!-- END timeline item -->
                                    <!-- timeline item -->
                                    <li>
                                        <i class="fa fa-user bg-aqua"></i>

                                        <div class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i>11:40</span>

                                            <h3 class="timeline-header no-border"><a href="#">Unidade de Campinas</a> Respondido por Francisco</h3>

                                            <div class="timeline-body">
                                                Documento não aceito por está ilegível, favor encaminhar novamente.
                                            </div>
                                        </div>
                                    </li>
                                    <!-- END timeline item -->
                                    <!-- timeline item -->
                                    <li>
                                        <i class="fa fa-clock-o bg-gray"></i>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <br />
                        <!-- Form Ticket -->
                        <div class="box box-info">
                            <div class="box-body">
                                <div class="grid-container-ticket">
                                    <div class="texto">
                                        <div class="texto">
                                            <textarea style="width: 850px; height: 100px"></textarea>
                                        </div>
                                        <div class="formulario">

                                            <div class="box-body">
                                                <form class="form-horizontal">
                                                    <div class="box-body">
                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Enviar Para.:</label>

                                                            <div class="col-sm-3">

                                                                <select class="custom-select form-control">
                                                                    <option selected>Selecione a unidade:</option>
                                                                    <option value="1">Campinas</option>
                                                                    <option value="2">Jundiaí</option>
                                                                    <option value="3">São Paulo</option>
                                                                </select>
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-3 control-label">Departamento.:</label>

                                                            <div class="col-sm-3">

                                                                <select class="custom-select form-control">
                                                                    <option selected>Departamento</option>
                                                                    <option value="1">Departamento 1</option>
                                                                    <option value="2">Departamento 2</option>
                                                                    <option value="3">Departamento 3</option>
                                                                </select>
                                                            </div>
                                                        </div>


                                                        <label for="inputEmail3" class="control-label pull-left">Usuário do Departamento selecionado.:</label>
                                                        <br />

                                                        <div class="box-body">

                                                            <table id="example2" class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Selecionar</th>
                                                                        <th style="width: 100px">Nome</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 5px">
                                                                            <input type="checkbox" class="checkbox" /></td>
                                                                        <td>Daniele</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 5px">
                                                                            <input type="checkbox" class="checkbox" /></td>
                                                                        <td>Manuela</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </form>
                                            </div>
                                            <br />



                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <a class="btn btn-danger"><i class="fa fa-share" style="margin-right: 10px"></i>Anexar Arquivo</a>
                                                </div>

                                                <label for="inputEmail3" class="col-sm-3 control-label">Status do Ticket.:</label>

                                                <div class="col-sm-3">

                                                    <select class="custom-select form-control">
                                                        <option value="1">Aberto</option>
                                                        <option value="2">Devolvido</option>
                                                        <option value="3">Em Atendimento</option>
                                                        <option value="4">Finalizado</option>
                                                    </select>
                                                </div>

                                                <div class="col-sm-2">
                                                    <a class="btn btn-success">Salvar</a>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
