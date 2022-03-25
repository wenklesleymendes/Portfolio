<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Ticket.aspx.cs" Inherits="EscolaPro.Web.Ticket" %>


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
            <h3 class="box-title">Ticket - Administração de Ocorrências</h3>

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

                    <label for="inputEmail3" class="col-sm-2 control-label">Período da Abertura.:</label>

                    <div class="col-sm-4">
                        <div class="input-group">

                            <i class="fa fa-calendar" style="margin: 10px"></i>


                            <input id="date" type="date">

                            <span style="margin-left: 10px; margin-right: 10px">a</span>

                            <i class="fa fa-calendar" style="margin: 10px"></i>

                            <input id="date" type="date">
                        </div>
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Protocolo.:</label>

                    <div class="col-sm-2">
                        <input type="email" class="form-control" id="inputEmail3" placeholder="Numero do Protocolo">
                    </div>


                </div>

                <div class="form-group">

                    <label for="inputEmail3" class="col-sm-1 control-label">Usuário.:</label>

                    <div class="col-sm-3">
                        <input type="email" class="form-control" id="inputEmail3" placeholder="Nome do Usuário Responsável">
                    </div>

                    <label for="inputEmail3" class="col-sm-1 control-label">Assunto.:</label>

                    <div class="col-sm-3">

                        <select class="custom-select form-control">
                            <option selected>Tipo / Assunto do Ticket</option>
                            <option value="1">Suporte T.I</option>
                            <option value="2">Documnentação</option>
                            <option value="3">Cadastro</option>
                        </select>
                    </div>
                    <div class="col-sm-3">
                         <a class="btn btn-dropbox"><i class="fa fa-search" style="margin-right: 5px;"></i>Buscar</a>
                       
                        <a class="btn btn-success pull-right" data-toggle="modal" data-target="#abrirTicketModal">Abrir Ticket</a>
                    </div>

                </div>

                <div class="box-body">

                    <br />
                    <br />
                    <table id="example2" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Protocolo</th>
                                <th>Assunto</th>
                                <th>Data da Abertura</th>
                                <th>Data do Atendimento</th>
                                <th>SLA</th>
                                <th>Status</th>
                                <th>Atendente</th>
                                <th>Usuário Responsável</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>00021</td>
                                <td>Documentação</td>
                                <td>21/03/2020 08:34</td>
                                <td>21/03/2020 11:15</td>
                                <td>Em Dia</td>
                                <td><span class="label label-warning">Em Atendimento</span></td>
                                <td>Francisco</td>
                                <td>Ricardo Castro</td>
                                <td>
                                    <a class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#ticketModal"></a>
                                </td>
                            </tr>
                            <tr>
                                <td>00021</td>
                                <td>Suporte TI</td>
                                <td>19/03/2020 09:45</td>
                                <td>19/03/2020 10:01</td>
                                <td>Finalizado</td>
                                <td><span class="label label-success">Finalizado</span></td>
                                <td>Francisco</td>
                                <td>Andre</td>
                                <td>
                                    <a class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#ticketModal"></a>
                                </td>
                            </tr>
                            <tr>
                                <td>00021</td>
                                <td>Panfletos</td>
                                <td>01/03/2020 16:20</td>
                                <td></td>
                                <td>Atrasado 3 Dias</td>
                                <td><span class="label label-danger">Atrasado</span></td>
                                <td></td>
                                <td>Francisco</td>
                                <td>
                                    <a class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#ticketModal"></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>


            </div>
        </form>
    </div>

    <div class="container">

        <div class="modal fade" id="abrirTicketModal" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Abertura de Ticket</h4>
                    </div>
                    <form class="form-horizontal">


                        <div class="modal-body">


                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">Assunto.:</label>

                                <div class="col-sm-7">

                                    <select class="custom-select form-control">
                                        <option selected>Tipo / Assunto do Ticket</option>
                                        <option value="1">Suporte T.I</option>
                                        <option value="2">Documnentação</option>
                                        <option value="3">Cadastro</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">Enviar Para.:</label>

                                <div class="col-sm-7">

                                    <select class="custom-select form-control">
                                        <option selected>Selecione a unidade:</option>
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-3 control-label">Departamento.:</label>

                                <div class="col-sm-7">

                                    <select class="custom-select form-control">
                                        <option selected>Departamento</option>
                                        <option value="1">Departamento 1</option>
                                        <option value="2">Departamento 2</option>
                                        <option value="3">Departamento 3</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="inputEmail3" class="col-sm-6 control-label">Usuário do Departamento selecionado.:</label>
                            </div>

                            <div class="form-group">

                                <div class="col-sm-8">

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


                            <div class="form-group">

                                <div class="col-sm-7">
                                    <textarea style="height: 80px; width: 550px" placeholder="Descrição da abertura do ticket aqui..."></textarea>
                                </div>
                            </div>

                            <div class="form-group">

                                <div class="col-sm-8">
                                </div>

                                <div class="col-sm-4">
                                    <a class="btn btn-danger"><i class="fa fa-share" style="margin-right: 5px"></i>Anexar arquivo</a>
                                </div>
                            </div>

                            <div class="box box-info">
                                <h5>Arquivos Anexados:</h5>
                                <br />
                                <div class="box box-body">

                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Nome do Arquivo</th>
                                                <th>Ações</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>documento-rg.jpeg</td>
                                                <td style="width: 10px">
                                                    <a class="btn btn-danger"><i class="fa fa-trash-o" style="margin-right: 5px"></i>Remover</a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <a class="btn btn-success pull-right">Salvar</a>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Ticket - Detalhes -->
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
                                        <span class="bg-red">Abertura do Ticket: 21 Março 2020
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


                                                    <label for="inputEmail3" class="control-label">Usuário do Departamento selecionado.:</label>
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

                                            <br />

                                            <div class="box-body">
                                                <div class="form-group">
                                                    <div class="col-sm-4">
                                                        <a class="btn btn-danger"><i class="fa fa-share" style="margin-right: 10px"></i>Anexar Arquivo</a>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Status do Ticket.:</label>

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
    </div>
    <!-- Fim Modal Ticket -->
</asp:Content>
