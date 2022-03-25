<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Comunicacao.aspx.cs" Inherits="EscolaPro.Web.Comunicacao" %>




<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">


    <style>
        img {
            border-radius: 50%;
        }

       
    </style>

    <style>
        .custom-file-input::-webkit-file-upload-button {
            visibility: hidden;
        }

        .custom-file-input::before {
            content: 'Anexar Arquivo';
            display: inline-block;
            background: linear-gradient(top, #f9f9f9, #e3e3e3);
            border: 1px solid #999;
            border-radius: 1px;
            padding: 5px 8px;
            outline: none;
            white-space: nowrap;
            -webkit-user-select: none;
            cursor: pointer;
            text-shadow: 1px 1px #fff;
            font-weight: 700;
            font-size: 10pt;
        }

        .custom-file-input:hover::before {
            border-color: black;
        }

        .custom-file-input:active::before {
            background: -webkit-linear-gradient(top, #e3e3e3, #f9f9f9);
        }
    </style>

    <link rel="stylesheet" href="plugins/fullcalendar/fullcalendar.min.css">
    <link rel="stylesheet" href="plugins/fullcalendar/fullcalendar.print.css" media="print">


    <script>
        function readSingleFile(e) {
            var file = e.target.files[0];
            if (!file) {
                return;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                var contents = e.target.result;
                displayContents(contents);
            };
            reader.readAsText(file);
        }

        function displayContents(contents) {
            var element = document.getElementById('file-content');
            element.textContent = contents;
        }

        document.getElementById('file-input')
            .addEventListener('change', readSingleFile, false);
    </script>

</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">




    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Comunicação</h3>
        </div>


        <form class="form-horizontal">

            <div class="box-body">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#home">Histórico de Disparo</a></li>
                    <li><a data-toggle="tab" href="#menu1">Enviar Disparo</a></li>
                    <li><a data-toggle="tab" href="#menu2">Configurar</a></li>
                </ul>

                <div class="tab-content">
                    <div id="home" class="tab-pane fade in active">

                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Histórico de Disparo</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>


                            <div class="box-body">

                                <div class="form-group">


                                    <label for="inputEmail3" class="col-sm-1 control-label">Assunto.:</label>

                                    <div class="col-sm-2">
                                        <select class="custom-select form-control">
                                            <option selected>Assunto</option>
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                        </select>
                                    </div>

                                    <label for="inputEmail3" class="col-sm-2 control-label">Data de Envio.:</label>

                                    <div class="col-sm-4">

                                        <div class="input-group">

                                            <i class="fa fa-calendar" style="margin: 10px"></i>

                                            <input id="date" type="date">

                                            <span style="margin-left: 10px; margin-right: 5px">a</span>

                                            <i class="fa fa-calendar" style="margin: 10px"></i>

                                            <input id="date" type="date">
                                        </div>

                                    </div>


                                    <div class="col-sm-3">
                                        <button type="button" class="btn btn-dropbox btn-sm" style="float: right">
                                            <span class="fa fa-search" style="margin-right: 10px"></span>Pesquisar
                                        </button>
                                    </div>

                                </div>

                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Data de Envio</th>
                                            <th>Assunto</th>
                                            <th>Tipo</th>
                                            <th>Conteúdo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>21/03/2020 08:34</td>
                                            <td>Boas vindas</td>
                                            <td><span class="fas fa-sms" style="font-size: x-large"></span></td>
                                            <td>Sejá bem vindo... 
                                                <a data-toggle="modal" data-target="#comunicacaoModal"><i class="far fa-comment-dots" style="font-size: x-large; color: dimgray; margin-left: 5px"></i></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>21/03/2020 08:34</td>
                                            <td>Boleto</td>
                                            <td><span class="fas fa-envelope-square" style="font-size: x-large"></span></td>
                                            <td>Seu boleto foi ...
                                                <a data-toggle="modal" data-target="#comunicacaoModal"><i class="far fa-comment-dots" style="font-size: x-large; color: dimgray; margin-left: 5px"></i></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>21/03/2020 08:34</td>
                                            <td>Boleto</td>
                                            <td><span class="fas fa-envelope-square" style="font-size: x-large"></span></td>
                                            <td>Seu boleto foi ...
                                                <a data-toggle="modal" data-target="#comunicacaoModal"><i class="far fa-comment-dots" style="font-size: x-large; color: dimgray; margin-left: 5px"></i></a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>


                    <div id="menu1" class="tab-pane fade">


                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Enviar Disparo</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>


                                <div class="box-body">


                                    <form class="form-horizontal">

                                        <div class="box-body">
                                            <div class="form-group">

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Curso:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Modalidade:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Ano:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Semestre:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>


                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Dia de Semana:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Período:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>


                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Horário:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>


                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Sala:</label>
                                                <br />
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione um curso</option>
                                                    <option value="1">Ensino Fundamental</option>
                                                    <option value="2">Ensino Médio</option>
                                                    <option value="3">Ensino Fundamental + Médio</option>
                                                </select>

                                            </div>

                                            <br />

                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars">Adicionar Lista de Contatos:</label>
                                                <br />
                                                <a class="btn btn-success" data-toggle="modal" data-target="#modalContatos" style="width: 200%;">Adicionar Contatos</a>

                                            </div>


                                            <div class="btn-group pull-right" style="margin: 10px">
                                                <label for="cars">Anexar Lista de Contatos:</label>
                                                <br />

                                                <input type="file" class="btn btn-danger" style="width: 400px">
                                                <%--         <input type="file" id="file-input" class="btn btn-danger" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                                <!-- curso, turma, unidade, ano, periodo -->

                                <div class="box-body">

                                    <div class="box box-info">
                                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                                            <i class="fa fa-comments-o"></i>

                                            <h3 class="box-title">Disparo de Mensagens</h3>

                                            <div class="box-tools pull-right" data-toggle="tooltip" title="" data-original-title="Tipo da Mensagem">
                                                <div class="btn-group" data-toggle="btn-toggle">
                                                    <button type="button" class="btn btn-default btn-sm active">
                                                        <span class="fas fa-border-all"></span>
                                                        <span style="font-size: small">Todos</span>
                                                    </button>
                                                    <button type="button" class="btn btn-default btn-sm active">
                                                        <span class="fab fa-facebook-messenger"></span>
                                                        <span style="font-size: small">Facebook</span>
                                                    </button>
                                                    <button type="button" class="btn btn-default btn-sm active">
                                                        <span class="fab fa-whatsapp"></span>
                                                        <span style="font-size: small">WhatsApp</span>
                                                    </button>
                                                    <button type="button" class="btn btn-default btn-sm active">
                                                        <span class="fab fa-instagram"></span>
                                                        <span style="font-size: small">Instagram</span>
                                                    </button>
                                                    <button type="button" class="btn btn-default btn-sm active">
                                                        <span class="fas fa-sms"></span>
                                                        <span style="font-size: small">SMS</span>
                                                    </button>
                                                    <button type="button" class="btn btn-default btn-sm">
                                                        <i class="fas fa-envelope"></i>
                                                        <span style="font-size: small">E-mail</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="slimScrollDiv">
                                            <div class="box-body chat" id="chat-box" style="overflow: hidden; width: auto; height: 100px;">
                                                <!-- chat item -->


                                                <!-- chat item -->
                                                <br />
                                                <div class="item">

                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="col-lg-1" style="width: 10px; text-align: center;">
                                                                <span class="fab fa-whatsapp" style="font-size: x-large"></span>
                                                            </div>

                                                            <div class="col-sm-11 col-lg-11">
                                                                <p class="message">
                                                                    <a href="#" class="name">
                                                                        <small class="text-muted pull-right"><i class="fa fa-clock-o"></i>5:30</small>
                                                                        Equipe Escola Modelo - Campinas
                                                                    </a>
                                                                    <br />
                                                                    Bem vindo a Escola Modelo
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <!-- /.item -->
                                            </div>
                                            <div class="slimScrollBar">
                                                <textarea class="form-control" placeholder="Escreva sua mensagem...">
                                                    </textarea>
                                            </div>
                                        </div>
                                        <!-- /.chat -->
                                        <div class="box-footer">
                                            <div class="input-group">

                                                <div class="input-group-btn">
                                                    <a class="btn btn-danger pull-left"><i class="fas fa-file-upload"
                                                        style="margin-right: 10px;"></i>Anexar Arquivo</a>

                                                    <a class="btn btn-success pull-right"><i class="fas fa-paper-plane"
                                                        style="margin-right: 10px;"></i>Enviar</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="menu2" class="tab-pane fade">


                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Mensagens de Campanha</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>

                            <div class="box-body">

                                <iframe src="Calendario.aspx" style="border: none;" width="100%" height="800px" runat="server"></iframe>



                                <!-- /.content-wrapper -->
                            </div>
                        </div>

                        <div class="row">
                            <!-- Regua de Contato Cobraça -->
                            <div class="col-sm-6 col-lg-6">
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Régua de Contato Cobraça</h3>

                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                        </div>
                                    </div>


                                    <div class="box-body">
                                        <a class="btn btn-success pull-right" data-toggle="modal" data-target="#modalAlertaRobo">Adicionar</a>

                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Assunto</th>
                                                    <th>Horário</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <label class="external-event bg-green">1. pré vencimento 3 dias antes</label></td>
                                                    <td>
                                                        <label class="external-event bg-red">08:00</label></td>
                                                    <td><a data-toggle="modal" data-target="#modalAlertaRobo"><span class="fas fa-edit" style="font-size: x-large"></span>Editar</a></td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <!-- Regua de Contato Relacionamento -->
                            <div class="col-sm-6 col-lg-6">
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Régua de Contato Relacionamento</h3>

                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                        </div>
                                    </div>

                                    <div class="box-body">
                                       
                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Assunto</th>
                                                    <th>Horário</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <label class="external-event bg-blue">02: Pendencia documentação</label></td>
                                                    <td>
                                                        <label class="external-event bg-red">08:00</label></td>
                                                    <td><a data-toggle="modal" data-target="#modalAlertaRobo"><span class="fas fa-edit" style="font-size: x-large"></span>Editar</a></td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </form>
    </div>


    <!-- Criar Lista de Contatos -->
    <div class="container">
        <div class="modal fade" id="modalContatos" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar de Contatos</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">
                            <div class="box-body">


                                <div class="input-group">
                                    <input id="new-event" type="text" class="form-control" placeholder="Telefone">
                                    <div class="input-group-btn">
                                        <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Adicionar Contato </button>
                                    </div>
                                    <!-- /btn-group -->
                                </div>


                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Enviar</th>
                                            <th>Telefone</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox" /></td>
                                            <td>11 985454545</td>
                                            <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: auto"></i>Excluir</a></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox" /></td>
                                            <td>11 9565656</td>
                                            <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: auto"></i>Excluir</a></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox" /></td>
                                            <td>11 9565665</td>
                                            <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: auto"></i>Excluir</a></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <br />
                                <a class="btn btn-success pull-right">Salvar</a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Criar Alerta -->


    <div class="container">
        <div class="modal fade" id="modalCriarAlerta" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Criar Alerta de Notificação</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">
                            <div class="box-body">


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Curso:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Modalidade:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Ano:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Semestre:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Dia de Semana:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Período:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Horário:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Sala:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Assunto:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um assunto</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin-left: 150px">


                                    <div class="box-tools pull-right" data-toggle="tooltip" title="" data-original-title="Tipo da Mensagem">
                                        <div class="btn-group" data-toggle="btn-toggle">
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fas fa-border-all"></span>
                                                <span style="font-size: small">Todos</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-facebook-messenger"></span>
                                                <span style="font-size: small">Facebook</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-whatsapp"></span>
                                                <span style="font-size: small">WhatsApp</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fab fa-instagram"></span>
                                                <span style="font-size: small">Instagram</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm active">
                                                <span class="fas fa-sms"></span>
                                                <span style="font-size: small">SMS</span>
                                            </button>
                                            <button type="button" class="btn btn-default btn-sm">
                                                <i class="fas fa-envelope"></i>
                                                <span style="font-size: small">E-mail</span>
                                            </button>
                                        </div>
                                    </div>

                                </div>


                                <div class="btn-group" style="margin-left: 150px">

                                    <textarea class="form-control" placeholder="Escreva sua mensagem..." style="width: 460px; height: 102px">

                                    </textarea>

                                </div>
                                <br />
                                <div class="btn-group" style="margin-left: 150px">
                                    <br />
                                    <a class="btn btn-danger pull-left"><i class="fas fa-file-upload" style="margin-right: 10px;" aria-hidden="true"></i>Anexar Arquivo</a>
                                </div>

                                <div class="btn-group" style="margin-left: 150px">
                                    <br />
                                    <a class="btn btn-success pull-right">Salvar</a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="container">
        <div class="modal fade" id="modalAlertaRobo" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Configurar Alerta - Robô</h4>
                    </div>
                    <div class="modal-body">
                        <div class="box box-info"></div>
                        <form class="form-horizontal">
                            <div class="box-body">

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Assunto:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Assunto</option>
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Horário de Envio:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Horário de Envio</option>
                                        <option value="1">01:00</option>
                                        <option value="2">02:00</option>
                                        <option value="4">03:00</option>
                                        <option value="5">04:00</option>
                                        <option value="6">05:00</option>
                                        <option value="7">06:00</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Tipo do Envio:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Tipo do Envio</option>
                                        <option value="1">Pós Vencimento</option>
                                        <option value="2">Antes do Vencimento</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Quantidade de dias:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Quantidade de dias</option>
                                        <option value="1">01</option>
                                        <option value="2">02</option>
                                        <option value="4">03</option>
                                        <option value="5">04</option>
                                        <option value="6">05</option>
                                        <option value="7">06</option>
                                    </select>

                                </div>

                                


                                <br />
                                <div class="box-tools pull-right" data-toggle="tooltip" title="" data-original-title="Tipo da Mensagem">
                                    <div class="btn-group" data-toggle="btn-toggle">
                                        <button type="button" class="btn btn-default btn-sm active">
                                            <span class="fas fa-border-all"></span>
                                            <span style="font-size: small">Todos</span>
                                        </button>
                                        <button type="button" class="btn btn-default btn-sm active">
                                            <span class="fab fa-facebook-messenger"></span>
                                            <span style="font-size: small">Facebook</span>
                                        </button>
                                        <button type="button" class="btn btn-default btn-sm active">
                                            <span class="fab fa-whatsapp"></span>
                                            <span style="font-size: small">WhatsApp</span>
                                        </button>
                                        <button type="button" class="btn btn-default btn-sm active">
                                            <span class="fab fa-instagram"></span>
                                            <span style="font-size: small">Instagram</span>
                                        </button>
                                        <button type="button" class="btn btn-default btn-sm active">
                                            <span class="fas fa-sms"></span>
                                            <span style="font-size: small">SMS</span>
                                        </button>
                                        <button type="button" class="btn btn-default btn-sm">
                                            <i class="fas fa-envelope"></i>
                                            <span style="font-size: small">E-mail</span>
                                        </button>
                                    </div>
                                </div>
                                <br />
                                <br />

                                <textarea class="form-control" style="width: 100%; height: 50%">

                                    </textarea>
                                <br />
                                <div class="footer">


                                    <a class="btn btn-danger pull-left"><i class="fas fa-file-upload" style="margin-right: 10px;" aria-hidden="true"></i>Anexar Arquivo</a>


                                    <a class="btn btn-success pull-right">Salvar</a>
                                </div>



                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
