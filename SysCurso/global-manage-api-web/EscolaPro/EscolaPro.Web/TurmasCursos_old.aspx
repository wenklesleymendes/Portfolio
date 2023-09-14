<%@ Page Title="Cursos e Turmas" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TurmasCursos.aspx.cs" Inherits="EscolaPro.Web.TurmasCursos" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">


    <style>
        .item1 {
            grid-area: header;
        }

        .item2 {
            grid-area: menu;
        }

        .item3 {
            grid-area: main;
        }

        .item4 {
            grid-area: right;
        }

        .item5 {
            grid-area: footer;
        }

        .grid-container-pagamento {
            display: grid;
            grid-template-areas: 'header header header header header header' 'menu main main main right right' 'menu footer footer footer footer footer';
            grid-gap: 10px;
            background-color: #2196F3;
            padding: 10px;
        }

            .grid-container-pagamento > div {
                background-color: rgba(255, 255, 255, 0.8);
                text-align: center;
                padding: 20px 0;
                font-size: 30px;
            }
    </style>


    <style>
        .modal-dialog-aluno {
            min-width: 80vw
        }

        .collapse-content .fa.fa-heart:hover {
            color: #f44336 !important;
        }


        .nav-tabs li.active a, .nav-tabs li.active a:focus, .nav-tabs li.active a:hover {
            background-color: #00c0ef;
            color: white;
        }

            .nav-tabs li.active a:focus {
                background-color: #00c0ef;
                color: white;
            }
    </style>

    <style>
        .dropdown-submenu {
            position: relative;
        }

            .dropdown-submenu .dropdown-menu {
                top: -180px;
                left: -180%;
                right: 35%;
                margin-top: 20px;
                margin-left: 100px;
                margin-right: 120px;
            }
    </style>

    <style>
        .card {
            font-size: 1em;
            overflow: hidden;
            padding: 0;
            border: none;
            border-radius: .28571429rem;
            box-shadow: 0 1px 3px 0 #d4d4d5, 0 0 0 1px #d4d4d5;
        }

        .card-block {
            font-size: 1em;
            position: relative;
            margin: 0;
            padding: 1em;
            border: none;
            border-top: 1px solid rgba(34, 36, 38, .1);
            box-shadow: none;
        }

        .card-img-top {
            display: block;
            width: 100%;
            height: auto;
        }

        .card-title {
            font-size: 1.28571429em;
            font-weight: 700;
            line-height: 1.2857em;
        }

        .card-text {
            clear: both;
            margin-top: .5em;
            color: rgba(0, 0, 0, .68);
        }

        .card-footer {
            font-size: 1em;
            position: static;
            top: 0;
            left: 0;
            max-width: 100%;
            padding: .75em 1em;
            color: rgba(0, 0, 0, .4);
            border-top: 1px solid rgba(0, 0, 0, .05) !important;
            background: #fff;
        }

        .card-inverse .btn {
            border: 1px solid rgba(0, 0, 0, .05);
        }

        .card:hover {
            box-shadow: 0px 0px 10px black;
        }
    </style>

    <%--    <link rel="stylesheet" href="dist/css/cartaoCredito.css">
    <script src="dist/js/cartaoCredito.js"></script>--%>
</asp:Content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="box box-info">
        <div class="box-header with-border" style="background-color: #3c8dbc">

            <div class="row">
                <div class="col-sm-3">
                    <label style="color: white">Nome:</label>
                </div>

                <div class="col-sm-3">
                    <label style="color: white">Matrícula:</label>
                </div>

                <div class="col-sm-3">
                    <label style="color: white">Data da Matrícula:</label>
                </div>

                <div class="col-sm-2">
                    <label style="color: white">Status da Matrícula:</label>
                </div>
            </div>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
        <form class="form-horizontal">

            <!-- form start -->
            <form class="form-horizontal">

                <div class="box-body">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#home">Curso e Turma</a></li>
                        <li><a data-toggle="tab" href="#menu1">Financeiro e Contrato</a></li>
                        <li><a data-toggle="tab" href="#menu2">Documentos</a></li>
                        <li><a data-toggle="tab" href="#menu3">Provas e Certificados</a></li>
                        <li><a data-toggle="tab" href="#menu4">Solicitações</a></li>
                        <li><a data-toggle="tab" href="#menu5">Tickets</a></li>
                        <li><a data-toggle="tab" href="#menu6">Comunicação</a></li>
                        <li><a data-toggle="tab" href="#menu7">Portal do Aluno</a></li>
                    </ul>

                    <div class="tab-content">

                        <!-- Curso e Turma -->
                        <div id="home" class="tab-pane fade in active">

                            <div class="box box-info">
                                <form class="form-horizontal">

                                    <div class="box-body">

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-1 control-label">Curso.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Selecione uma curso</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                            <label for="inputEmail3" class="col-sm-2 control-label">Modalidade.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Modalidade</option>
                                                    <option value="1">Presencial</option>
                                                    <option value="2">Distância</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-1 control-label">Ano.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Selecione uma unidade</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                            <label for="inputEmail3" class="col-sm-1 control-label">Semestre.:</label>

                                            <div class="col-sm-2">

                                                <select class="custom-select form-control">
                                                    <option selected>Semestre</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                            <label for="inputEmail3" class="col-sm-2 control-label">Dia da Semana.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Dia da Semana</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-1 control-label">Período.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Período</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                            <label for="inputEmail3" class="col-sm-1 control-label">Horário.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Horário</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                            <label for="inputEmail3" class="col-sm-1 control-label">Sala.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected>Sala</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-2 control-label">Vagas disponíveis.:</label>

                                            <div class="col-sm-2">
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Quantidade">
                                            </div>

                                            <label for="inputEmail3" class="col-sm-4 control-label">Transferir matrícula para outro turma.:</label>

                                            <div class="col-sm-4">

                                                <select class="custom-select form-control">
                                                    <option selected>Turma</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiaí</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="input-group-btn pull-left col-sm-2">
                                                <button class="btn btn-dropbox dropdown-toggle" aria-expanded="false" type="button" data-toggle="dropdown">
                                                    Lista Presença
                                                   
                                                <span class="fa fa-caret-down"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li><a href="#">Lista de Presença</a></li>
                                                    <li><a href="#">Lista(s) Salva(s)</a></li>
                                                    <li><a href="#">Upload de Lista</a></li>
                                                </ul>
                                            </div>

                                            <%--             <button class="btn btn-success pull-right" style="margin-right: 15px">Continuar</button>--%>
                                        </div>

                                    </div>
                                </form>
                            </div>


                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Professores vinculados a esta turma</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>


                                <div class="box-body">


                                    <div class="form-group">

                                        <div class="col-lg-9">
                                            <div class="box-body">

                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Professor</th>
                                                            <th>Disciplina</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>João Almeida</td>
                                                            <td>Fisíca</td>
                                                            <td>
                                                                <a>Ver cadastro do professor</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Roberto da Silva</td>
                                                            <td>Matématica</td>
                                                            <td>
                                                                <a>Ver cadastro do professor</a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="col-lg-3">
                                            <div class="box-group">
                                                <a>Ver calendário de aulas</a>
                                                <br />
                                                <i class="fa fa-calendar col-lg-6" style="font-size: 80px; text-align: center"></i>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- Financeiro e Contrato -->
                        <div id="menu1" class="tab-pane fade">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title"></h3>
                                </div>
                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-2 control-label">N. da Matrícula.:</label>

                                    <div class="col-sm-2">
                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Quantidade">
                                    </div>

                                    <label for="inputEmail3" class="col-sm-2 control-label">Status da Matrícula.:</label>

                                    <div class="col-sm-1">

                                        <span class="btn btn-success">
                                            <a class="fa fa-check" style="color: white">Ativo</a>
                                        </span>

                                    </div>

                                    <label for="inputEmail3" class="col-sm-2 control-label">Bolsa/Convênio Cod.:</label>

                                    <div class="col-sm-3">

                                        <select class="custom-select form-control">
                                            <option selected>Condições bolsa - convênio</option>
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                        </select>
                                    </div>

                                    <div class="col-sm-1">
                                    </div>

                                </div>

                                <br />
                                <br />

                                <div class="form-group">
                                    <label for="inputEmail3" class="col-sm-3 control-label">Período de validade do contrato.:</label>

                                    <div class="col-sm-1">
                                        <a for="inputEmail3" class="control-label">Início 06/06/2019</a>
                                    </div>


                                    <div class="col-sm-1">
                                        <a for="inputEmail3" class="control-label">Término 07/06/2020</a>
                                    </div>


                                    <div class="box-body col-sm-1 pull-right" style="margin-right: 100px">

                                        <table class="table" border="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <div class="input-group-btn">
                                                        <button class="btn btn-dropbox dropdown-toggle" aria-expanded="false" type="button" data-toggle="dropdown">
                                                            Ações Matrícula
                                               
                                                            <span class="fa fa-caret-down"></span>
                                                        </button>
                                                        <ul class="dropdown-menu">
                                                            <li><a href="#">Visualizar Contrato</a></li>
                                                            <li><a href="#">Editar Contrato</a></li>
                                                            <li><a href="#">Upload de Contrato Assinado</a></li>
                                                        </ul>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a class="btn btn-danger center-block" data-toggle="modal" data-target="#myModalCancelamento">Cancelamento</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>


                            <!-- Forma de Pagamento -->




                            <div class="grid-container">
                                <div class="item1">Header</div>
                                <div class="item2">Menu</div>
                                <div class="item3">Main</div>
                                <div class="item4">Right</div>
                                <div class="item5">Footer</div>
                            </div>





                            <!-- Fim Forma de Pagamento -->


                            <!-- Painel de Pagamento -->

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Painel de Pagamento</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>


                                <div class="box-body">
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="text-center">Descrição</th>
                                                <th>Valor</th>
                                                <th style="width: 20px">Descontos: Assiduidade promoção bolsa/convênio</th>
                                                <th>Valor até o Vencimento</th>
                                                <th>Data de Vencimento</th>
                                                <th>Nosso número</th>
                                                <th>E-mail Enviado</th>
                                                <th class="text-center">Selecionar Todos
                                                        <br />
                                                    <input type="checkbox" class="form-check-input text-center" id="exampleCheck1">
                                                </th>
                                                <th>Situação</th>
                                                <th>
                                                    <select class="custom-select form-control">
                                                        <option selected>Ações múltipla</option>
                                                        <option value="1">Gerar boleto / Enviar por e-mail</option>
                                                        <option value="2">Enviar por e-mail</option>
                                                        <option value="3">Recalcular / Enviar por e-mail</option>
                                                        <option value="4">Receber via Cartão e Excluir boleto</option>
                                                        <option value="5">Cancelar baixa de pagamento</option>
                                                        <option value="6">Gerar boleto residual</option>
                                                        <option value="7">Gerar recibo</option>
                                                        <option value="8">NF-e / Gerar Nota Fiscal</option>
                                                    </select>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr style="background-color: #b2ffb2">
                                                <td>curso supletivo ensino médio + taxas de inscrição 1ª e 2ª parcela crédito 12x</td>
                                                <td>138,00</td>
                                                <td>0,00</td>
                                                <td></td>
                                                <td>10/02/2020</td>
                                                <td>9836623</td>
                                                <td>
                                                    <a style="text-align: center" data-toggle="modal" data-target="#confirmacaoEmailModal">
                                                        <i class="fa fa-envelope" style="text-align: center;"></i>
                                                    </a>
                                                    <p>Sim</p>
                                                </td>
                                                <td class="text-center">
                                                    <input type="checkbox" class="form-check-input"></td>
                                                <td>
                                                    <a class="btn btn-success" style="background-color: #00b200" data-toggle="modal" data-target="#detalhePagamentoModal">
                                                        <i class="fa fa-money" style="margin-right: 5px;"></i>Pago</a>
                                                </td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option selected>Ações</option>
                                                        <option value="1">Gerar boleto / Enviar por e-mail</option>
                                                        <option value="2">Enviar por e-mail</option>
                                                        <option value="3">Recalcular / Enviar por e-mail</option>
                                                        <option value="4">Excluir boleto</option>
                                                        <option value="5">Cancelar baixa de pagamento</option>
                                                        <option value="6">Gerar boleto residual</option>
                                                        <option value="7">Gerar recibo</option>
                                                        <option value="8">NF-e / Gerar Nota Fiscal</option>
                                                    </select>


                                                </td>

                                            </tr>
                                            <tr>
                                                <td>apostila crédito 12x</td>
                                                <td>138,00</td>
                                                <td>0,00</td>
                                                <td></td>
                                                <td>10/02/2020</td>
                                                <td>Não</td>
                                                <td>Aberto</td>
                                                <td class="text-center">
                                                    <input type="checkbox" class="form-check-input" id="exampleCheck1"></td>
                                                <td></td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option selected>Ações</option>
                                                        <option value="1">Gerar boleto / Enviar por e-mail</option>
                                                        <option value="2">Enviar por e-mail</option>
                                                        <option value="3">Recalcular / Enviar por e-mail</option>
                                                        <option value="4">Excluir boleto</option>
                                                        <option value="5">Cancelar baixa de pagamento</option>
                                                        <option value="6">Gerar boleto residual</option>
                                                        <option value="7">Gerar recibo</option>
                                                        <option value="8">NF-e / Gerar Nota Fiscal</option>
                                                    </select>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>

                        </div>



                        <!-- Documentos -->

                        <div id="menu2" class="tab-pane fade">


                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Lista de Documentos</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>


                                <div class="box-body">

                                    <div class="form-group">

                                        <div class="col-sm-8">


                                            <table id="example2" class="table table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">RG</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">CPF</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-close" style="color: red; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">3 Fotos 3x4</th>
                                                        <th style="width: 1px;"><i class="fa fa-upload" style="color: dodgerblue; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Comprovante de Residência com cep</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-close" style="color: red; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Histórico Escolar</th>
                                                        <th style="width: 1px;"><i class="fa fa-upload" style="color: dodgerblue; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Documento que comprove alfabetização</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Reservista</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Título de Eleitor</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">Certidão de Nascimento ou Casamento</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                    <tr>
                                                        <th style="width: 1px;"><i class="fa fa-check" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="font-size: medium;">CNH</th>
                                                        <th style="width: 1px;"><i class="fa fa-file-photo-o" style="color: green; font-size: xx-large;"></i></th>
                                                        <th style="width: 10px"><a class="btn btn-dropbox">Visualizar</a></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>


                                        <div class="col-sm-3">

                                            <a class="btn btn-default pull-right">Ver registro de envio de documentos</a>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="col-sm-3">
                                            <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#documentoModal">Visualizar Todos</a>

                                            <br />

                                        </div>

                                        <br />
                                        <br />

                                        <div class="col-sm-3">

                                            <div class="input-group-btn">
                                                <button class="btn btn-dropbox dropdown-toggle pull-right" aria-expanded="false" type="button" data-toggle="dropdown">
                                                    Ações Documentos
                                               
                                                            <span class="fa fa-caret-down"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li><a href="#">Gerar Declaração de Pendência</a></li>
                                                    <li><a href="#">Upload da Declaração Assinada</a></li>
                                                    <li><a href="#">Visualizar Declaração Assinada</a></li>
                                                </ul>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>


                        <!-- Fim Documentos -->


                        <div id="menu3" class="tab-pane fade">
                            <div class="box box-info">
                            </div>
                            <h3>Provas e Certificados</h3>
                            <p>Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
                        </div>


                        <!-- Solicitações -->
                        <div id="menu4" class="tab-pane fade">


                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Realizar Solicitação</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>


                                <div class="box-body">

                                    <table id="example2" class="table table-bordered table-hover">

                                        <thead>
                                            <tr>
                                                <th style="width: 1px;"></th>
                                                <th style="font-size: medium;">Descrição</th>
                                                <th style="font-size: medium;">Valor</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Solicitação de Apostila</th>
                                                <th>R$ 185,90</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Declaração de cursando</th>
                                                <th>
                                                    <p style="color: forestgreen">Grátis</p>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Declaração de provas</th>
                                                <th>
                                                    <p style="color: forestgreen">Grátis</p>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Declaração de realização de exames - Colégio autorizado</th>
                                                <th>R$ 50,00</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Declaração de conclusão - Colégio autorizado</th>
                                                <th>R$ 100,00</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 1px;">
                                                    <input type="checkbox" class="checkbox" /></th>
                                                <th>Declaração de 3ª ano - Ensino médio - Colégio autorizado</th>
                                                <th>R$ 80,00</th>
                                            </tr>
                                            <tr>
                                                <th colspan="2" style="background-color: #F7F7F7">Total a pagar:</th>
                                                <th style="color: red; background-color: #F7F7F7">R$ 300,00</th>
                                            </tr>
                                        </thead>
                                    </table>
                                    <br />
                                    <div class="footer">
                                        <a class="btn btn-success pull-right" data-toggle="modal" data-target="#myModal">Efetuar Solicitação</a>
                                    </div>
                                </div>


                            </div>

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Histórico Solicitação</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th style="width: 10%">Data Solicitação</th>
                                                <th>Descrição</th>
                                                <th style="width: 10%">Pagamento</th>
                                                <th style="width: 10%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>14/02/2020</td>
                                                <td>Declaração de provas</td>
                                                <td>
                                                    <p style="color: forestgreen">Grátis</p>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>14/02/2020</td>
                                                <td>Declaração de provas</td>
                                                <td>
                                                    <p style="color: red">Pendente</p>
                                                </td>
                                                <td>
                                                    <a><i class="fa fa-search" style="margin-right: 5px"></i>Visualizar</a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>

                        </div>
                        <!-- Fim Solicitações -->

                        <div id="menu5" class="tab-pane fade">
                            <h3>Comunicação</h3>
                            <p>Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
                        </div>
                        <div id="menu6" class="tab-pane fade">
                            <h3>Portal do Aluno</h3>
                            <p>Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
                        </div>
                    </div>
                </div>



                <!-- Modal - Detalhamento documentos -->
                <div class="container">

                    <div class="modal fade" id="documentoModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Documentos</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-sm-7">

                                                <h1>Documentos exibido</h1>

                                            </div>

                                            <div class="col-sm-5">

                                                <table id="example2" class="table table-bordered table-hover">

                                                    <thead>
                                                        <tr>
                                                            <th colspan="3" style="width: 1px;">o que deseja fazer:</th>
                                                        </tr>
                                                        <tr>
                                                            <th colspan="3"><a><i class="fa fa-print" style="margin-right: 5px"></i>Imprimir</a></th>
                                                        </tr>
                                                        <tr>
                                                            <th colspan="3"><a><i class="fa fa-download" style="margin-right: 5px"></i>Download</a></th>
                                                        </tr>
                                                        <tr>
                                                            <th colspan="3"><a><i class="fa fa-remove" style="margin-right: 5px; color: red;"></i>Excluir</a></th>
                                                        </tr>
                                                        <tr>
                                                            <th><a><i class="fa fa-envelope-o" style="margin-right: 5px"></i>enviar por e-mail:</a></th>
                                                            <th>
                                                                <select class="custom-select form-control">
                                                                    <option selected>Informar e-mail</option>
                                                                    <option value="1">Campinas</option>
                                                                    <option value="2">Jundiaí</option>
                                                                    <option value="3">São Paulo</option>
                                                                </select>
                                                            </th>
                                                            <th><a>Enviar</a></th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>

                                        </div>



                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Fim Detalhamento documentos -->


                <!-- Pagament Cartão -->
                <div class="container">

                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Pagamento com Cartão</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">


                                        <!-- Cartão -->

                                        <iframe src="CartaoCredito.html" style="border: 0px; background-color: white; height: 50vw; width: 80%"></iframe>
                                        <a class="btn btn-success" data-dismiss="modal">Confirmar Pagamento</a>
                                        <!-- Fim Cartão -->

                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Fim Pagamento Cartão -->



                <!-- Confirmacao Email Modal  -->
                <div class="container">

                    <div class="modal fade" id="confirmacaoEmailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Detalhamento enviado de E-mail</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">

                                        <div>
                                            <label class="control-label">Envio(2) de boleto(s) por e-mail:</label>
                                        </div>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Data</th>
                                                    <th>Para</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <%--      <i class="fa fa-calendar"><p style="margin-left:5px"></p></i>--%>
                                                        <i class="fa fa-calendar" style="color: dodgerblue"></i><a style="margin-left: 10px">23/09/2019 22:06:11</a>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <i class="fa fa-envelope" style="color: dodgerblue"></i><a style="margin-left: 10px">andrerc77@hotmail.com</a>
                                                    </td>
                                                    <td>
                                                        <a>Detalhar</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <%--      <i class="fa fa-calendar"><p style="margin-left:5px"></p></i>--%>
                                                        <i class="fa fa-calendar" style="color: dodgerblue"></i><a style="margin-left: 10px">23/09/2019 22:08:21 </a>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <i class="fa fa-envelope" style="color: dodgerblue"></i><a style="margin-left: 10px">andrerc77@hotmail.com</a>
                                                    </td>
                                                    <td>
                                                        <a>Detalhar</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Fim Confirmacao Email -->




                <!-- Detalhe do Pagamento - Botão Pagar -->
                <div class="container">

                    <div class="modal fade" id="detalhePagamentoModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Detalhes do Pagamento</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">
                                        <div class="box-body">

                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Valor Total:</th>
                                                        <th>R$ 186,25</th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" style="text-align: center">Recebimento em múltiplos cartões</th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="2" style="text-align: center">Cartão 01:</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Valor Pago:</th>
                                                        <th>R$ 86,25</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Data do Pagamento:</th>
                                                        <th>26/02/2020</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Forma de Recebimento:</th>
                                                        <th>Cartão de Crédito 1x</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Taxas Cartão:</th>
                                                        <th>1,30 %</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Valor Liquido Recebido:</th>
                                                        <th>R$ 85,12</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Autorização Operadora:</th>
                                                        <th>22548</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Usuário Responsável:</th>
                                                        <th>Vinicius_Rodrigues</th>
                                                    </tr>


                                                </thead>
                                                <tbody>
                                                    <tr>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Fim Detalhe do Pagamento - Botão Pagar -->






                <!-- Modal Cancelamento -->
                <div class="container">

                    <div class="modal fade" id="myModalCancelamento" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Cancelamento</h4>
                                </div>
                                <div class="modal-body">
                                    <form class="form-horizontal">
                                        <div class="box-body">

                                            <div class="box box-info">

                                                <table class="table table-bordered table-hover" style="width: 100%">
                                                    <tr>
                                                        <td>Data do Cancelamento:</td>
                                                        <td align="right">16/02/2020</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Selecione o motivo do cancelamento:</td>
                                                        <td align="right">
                                                            <select class="custom-select form-control">
                                                                <option selected>Motivo.:</option>
                                                                <option value="1">Horário</option>
                                                                <option value="2">Perdeu emprego</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Comentários (informar aqui sobre o cancelamento):
                                                        <br />
                                                            <input type="text" style="width: 100%; height: 100px" placeholder="Comentários" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Valor da multa de cancelamento:</td>
                                                        <td align="right">R$ ---------- </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Isentar multa de cancelamento:</td>
                                                        <td align="right">
                                                            <select class="custom-select form-control">
                                                                <option value="1">Sim</option>
                                                                <option value="2">Não</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Motivo da isenção da multa:</td>
                                                        <td align="right">
                                                            <select class="custom-select form-control">
                                                                <option selected>Motivo da isenção</option>
                                                                <option value="1">Sim</option>
                                                                <option value="2">Não</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Isenção autorizada pelo usuário:</td>
                                                        <td align="right">
                                                            <select class="custom-select form-control">
                                                                <option selected>Usuário</option>
                                                                <option value="1">Sim</option>
                                                                <option value="2">Não</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <br />
                                                <div class="footer">
                                                    <button class="btn btn-dropbox"><i class="fa fa-print" style="margin-right: 5px"></i>Carta de Cancelamento</button>
                                                    <button class="btn btn-dropbox"><i class="fa fa-upload" style="margin-right: 5px"></i>Carta Assinada</button>
                                                    <button class="btn btn-dropbox"><i class="fa fa-upload" style="margin-right: 5px"></i>Atestado</button>

                                                    <button class="btn btn-danger pull-right">Salvar Cancelamento</button>
                                                </div>

                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <!-- Dados Usuário Modal -->


                <div class="container">

                    <div class="modal fade" id="visualizarModal" role="dialog">
                        <div class="modal-dialog-aluno modal-lg text-center" style="margin-left: 10%">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Dados Aluno</h4>
                                </div>
                                <div class="modal-body">

                                    <!-- Visuliza Dados -->

                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Dados Pessoais</h3>
                                        </div>
                                        <!-- /.box-header -->
                                        <!-- form start -->
                                        <form class="form-horizontal">


                                            <div class="row">

                                                <div class="col-sm-9 col-lg-9">
                                                    <%--<img src="dist/img/avatar04.png" alt="Avatar">--%>

                                                    <div class="box-body">

                                                        <div class="form-group">



                                                            <label for="inputEmail3" class="col-sm-2 control-label">Unidade.:</label>



                                                            <div class="col-sm-4">

                                                                <select class="custom-select form-control">
                                                                    <option selected>Selecione uma unidade</option>
                                                                    <option value="1">Campinas</option>
                                                                    <option value="2">Jundiaí</option>
                                                                    <option value="3">São Paulo</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Nome</label>

                                                            <div class="col-sm-5">
                                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Nome">
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-2 control-label">Data de Nasc.:</label>

                                                            <div class="col-sm-3">
                                                                <input type="email" class="form-control" placeholder="Data de Nascimento">
                                                            </div>

                                                        </div>

                                                        <div class="form-group">
                                                            <label for="inputEmail3" class="col-sm-2 control-label">Nome da Mãe</label>

                                                            <div class="col-sm-5">
                                                                <input type="email" class="form-control" placeholder="Nome da Mãe">
                                                            </div>

                                                            <label for="inputEmail3" class="col-sm-1 control-label">Sexo.:</label>

                                                            <div class="col-sm-2">

                                                                <select class="custom-select form-control">
                                                                    <option selected>Sexo.:</option>
                                                                    <option value="1">Masculino</option>
                                                                    <option value="2">Feminino</option>
                                                                </select>
                                                            </div>

                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-3 col-lg-3">
                                                    <img src="dist/img/avatar04.png" alt="Avatar" style="width: 150px; border-radius: 50%; margin-top: 10px; margin-left: 60px">
                                                </div>

                                            </div>


                                            <div class="box-body">

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">CPF.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="000.000.000-00">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">RG.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="RG">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">Órgão Exp.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="Órgão Exp.:">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">Estado:</label>

                                                    <div class="col-sm-1">
                                                        <input type="email" class="form-control" placeholder="UF">
                                                    </div>


                                                </div>


                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Estado Civil.:</label>

                                                    <div class="col-sm-2">
                                                        <select class="custom-select form-control">
                                                            <option selected>Estado Civil</option>
                                                            <option value="1">Solteiro (a)</option>
                                                            <option value="2">Casado (a)</option>
                                                        </select>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Nacionalidade.:</label>

                                                    <div class="col-sm-2">
                                                        <select class="custom-select form-control">
                                                            <option selected>Nacionalidade</option>
                                                            <option value="1">Brasileiro</option>
                                                            <option value="2">Estrangeiro</option>
                                                        </select>
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-2 control-label">Naturalidade.:</label>

                                                    <div class="col-sm-2">
                                                        <select class="custom-select form-control">
                                                            <option selected>Naturalidade</option>
                                                            <option value="1">Campinas</option>
                                                            <option value="2">Jundiaí</option>
                                                            <option value="3">São Paulok</option>
                                                        </select>
                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Título Eleitoral.:</label>

                                                    <div class="col-sm-4">
                                                        <input type="email" class="form-control" placeholder="Título Eleitoral">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">Zona.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="Zona">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">Seção.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="Seção">
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-2 control-label">Nome.:</label>

                                                    <div class="col-sm-4">
                                                        <input type="email" class="form-control" placeholder="Nome do Responsável">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">RG.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="RG do Responsável">
                                                    </div>

                                                    <label for="inputEmail3" class="col-sm-1 control-label">CPF.:</label>

                                                    <div class="col-sm-2">
                                                        <input type="email" class="form-control" placeholder="CPF do Responsável">
                                                    </div>
                                                </div>


                                            </div>
                                            <!-- /.box-body -->
                                            <%--   <div class="box-footer">

                <button type="submit" class="btn btn-info pull-right">Salvar</button>
            </div>--%>
                                            <!-- /.box-footer -->
                                        </form>

                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Endereço</h3>
                                            </div>
                                            <!-- /.box-header -->
                                            <!-- form start -->
                                            <form class="form-horizontal">
                                                <div class="box-body">

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">CEP.:</label>

                                                        <div class="col-sm-2">
                                                            <input type="email" class="form-control" placeholder="CEP">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Rua.:</label>

                                                        <div class="col-sm-2">
                                                            <input type="email" class="form-control" placeholder="Rua">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Número.:</label>

                                                        <div class="col-sm-1">
                                                            <input type="email" class="form-control" placeholder="Número">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Complemento:</label>

                                                        <div class="col-sm-2">
                                                            <input type="email" class="form-control" placeholder="Complemento">
                                                        </div>


                                                    </div>

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Bairro.:</label>

                                                        <div class="col-sm-3">
                                                            <input type="email" class="form-control" placeholder="Bairro">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-2 control-label">Cidade.:</label>

                                                        <div class="col-sm-3">
                                                            <input type="email" class="form-control" placeholder="Cidade">
                                                        </div>


                                                        <label for="inputEmail3" class="col-sm-1 control-label">Estado.:</label>

                                                        <div class="col-sm-1">

                                                            <select class="custom-select form-control">
                                                                <option selected>UF.:</option>
                                                                <option value="1">SP</option>
                                                                <option value="2">RJ</option>
                                                                <option value="3">MG</option>
                                                            </select>
                                                        </div>

                                                    </div>

                                                </div>
                                            </form>
                                        </div>


                                        <!-- Dados de contato -->

                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Contato</h3>
                                            </div>
                                            <!-- /.box-header -->
                                            <!-- form start -->
                                            <form class="form-horizontal">
                                                <div class="box-body">

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">E-mail.:</label>

                                                        <div class="col-sm-3">
                                                            <input type="email" class="form-control" placeholder="E-mail">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Envio de Email.:</label>

                                                        <div class="col-sm-1">
                                                            <select class="custom-select form-control">
                                                                <option value="1" style="color: red;">Ativo</option>
                                                                <option value="2" style="color: red">Inativo</option>
                                                            </select>
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Celular.:</label>

                                                        <div class="col-sm-2">
                                                            <input type="email" class="form-control" placeholder="Telefone">
                                                        </div>

                                                        <label for="inputEmail3" class="col-sm-1 control-label">Sms.:</label>

                                                        <div class="col-sm-1">
                                                            <select class="custom-select form-control">
                                                                <option value="1" style="color: red;">Ativo</option>
                                                                <option value="2" style="color: red">Inativo</option>
                                                            </select>
                                                        </div>


                                                    </div>

                                                    <div class="form-group">
                                                        <label for="inputEmail3" class="col-sm-2 control-label">Telefone fixo.:</label>

                                                        <div class="col-sm-3">
                                                            <input type="email" class="form-control" placeholder="Telefone">
                                                        </div>



                                                        <label for="inputEmail3" class="col-sm-2 control-label">Como nos conheceu?</label>

                                                        <div class="col-sm-3">

                                                            <select class="custom-select form-control">
                                                                <option selected>Selecione uma opção.:</option>
                                                                <option value="1">Facebook</option>
                                                                <option value="2">Site</option>
                                                                <option value="3">Instagram</option>
                                                            </select>
                                                        </div>

                                                    </div>
                                                </div>
                                            </form>
                                            <!-- Fim visualizar dados -->

                                            <!-- Matrícula -->
                                            <div class="box box-info">
                                                <div class="box-header with-border">
                                                    <div class="row">
                                                        <div class="col-lg-8">

                                                            <h3 class="box-title">Matrícula(s) vinculada(s)a este aluno:</h3>
                                                        </div>

                                                        <div class="col-lg-4">
                                                            <a class="btn btn-dropbox pull-right" runat="server" href="~/TurmasCursos"
                                                                style="margin-left: 5px; margin-right: 5px">
                                                                <span class="fa fa-plus" style="margin-left: 5px; margin-right: 5px"></span>Incluir matrícula
                                                            </a>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="box-body">
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Matricula</th>
                                                                <th>Unidade</th>
                                                                <th>Nome</th>
                                                                <th>Status Matricula</th>
                                                                <th>Curso</th>
                                                                <th>Ano</th>
                                                                <th>Semestre</th>
                                                                <th>E-mail</th>
                                                                <th>Celular</th>
                                                                <th>Financeiro</th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>25298</td>
                                                                <td>Campinas</td>
                                                                <td>Andre Coutinho</td>
                                                                <td>Ativo</td>
                                                                <td>Ensino Médio</td>
                                                                <td>2020</td>
                                                                <td>1º Semestre</td>
                                                                <td>and@hotmail.com</td>
                                                                <td>19 997016077</td>
                                                                <td>
                                                                    <label style="color: red; text-align: center; font-size: large">$!</label></td>
                                                                <td>
                                                                    <%--<button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>--%>

                                                                    <div class="input-group-btn">
                                                                        <span class="fa fa-search dropdown-toggle" data-toggle="dropdown" style="border-color: transparent; background-color: transparent" aria-expanded="true"></span>
                                                                        <button type="button" class="btn btn-default dropdown-toggle" style="border-color: transparent; background-color: transparent" data-toggle="dropdown" aria-expanded="true">
                                                                            <span class="fa fa-search"></span>
                                                                        </button>
                                                                        <ul class="dropdown-menu pull-right">
                                                                            <li><a runat="server" href="~/TurmasCursos">Ver matrícula</a></li>
                                                                            <li><a runat="server" href="~/TurmasCursos">Ver financeiro</a></li>
                                                                            <li class="divider"></li>
                                                                            <li class="dropdown-submenu">
                                                                                <a class="test" href="#">Declarações formulários <span class="caret"></span></a>
                                                                                <ul class="dropdown-menu pull-right">
                                                                                    <li><a href="#">Declaração cursando</a></li>
                                                                                    <li><a href="#">Declaração de provas</a></li>
                                                                                    <li><a href="#">Declaração de apostila</a></li>
                                                                                    <li><a href="#">Carta de cancelamento</a></li>
                                                                                    <li><a href="#">Contrato</a></li>
                                                                                    <li><a href="#">Inscrição provas fundamental</a></li>
                                                                                    <%--     <li class="divider"></li>--%>
                                                                                    <li><a href="#">Inscrição prova e. médio</a></li>
                                                                                </ul>
                                                                            </li>

                                                                        </ul>
                                                                    </div>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <!-- Fim Matrícula -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <!-- Fim dados usuário modal -->
            </form>

            <!-- /.box-body -->
            <div class="box-footer">

                <button class="btn btn-dropbox pull-left" data-toggle="modal" data-target="#visualizarModal">Dados Aluno</button>
                <%-- <form id="form1" runat="server">
                    <button ID='btn1' class="btn btn-dropbox" runat='server' onserverclick='btn_Click'>Dados Aluno</button>
                </form>--%>

                <button type="submit" class="btn btn-success pull-right" data-toggle="modal" data-target="#myModal">Continuar</button>
            </div>
            <!-- /.box-footer -->

        </form>

    </div>



</asp:Content>
