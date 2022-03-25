<%@ Page Title="Cursos e Turmas" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PainelMatriculaAluno.aspx.cs" Inherits="EscolaPro.Web.TurmasCursos" %>


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


        .grid-container-provas {
            display: grid;
            /*grid-template-areas: 'header header header header header header' 'menu main main main right right' 'menu footer footer footer footer footer';*/
            grid-template-areas: 'menu menu  main main' 'menu menu main main';
            grid-gap: 5px;
            padding: 5px;
        }

        .grid-container-p > div {
            text-align: center;
            padding: 1px 0;
        }

        .texto {
            grid-area: texto;
        }

        .formulario {
            grid-area: formulario;
        }



        .anexo {
            grid-area: anexo;
        }

        .grid-container-ticket > div {
            text-align: center;
            padding: 1px 0;
        }

        .grid-container-ticket {
            display: grid;
            grid-template-areas: 'texto texto texto formulario formulario' 'anexo anexo anexo formulario formulario';
            grid-gap: 5px;
            padding: 5px;
        }

        .numberCircle {
            border-radius: 50%;
            background: red;
            font-size: xx-large;
            color: #fff;
            text-align: center;
            font: 14px verdana, sans-serif;
            padding-left: 5px;
            padding-right: 5px;
            margin-right: 5px
        }


        .slimScrollBar {
            background: rgb(0, 0, 0);
            width: 7px;
            position: absolute;
            top: 66px;
            opacity: 0.4;
            display: none;
            border-radius: 7px;
            z-index: 99;
            right: 1px;
            height: 184.911px;
        }

        .slimScrollRail {
            width: 7px;
            height: 100%;
            position: absolute;
            top: 0px;
            display: none;
            border-radius: 7px;
            background: rgb(51, 51, 51);
            opacity: 0.2;
            z-index: 90;
            right: 1px;
        }

        .slimScrollDiv {
            position: relative;
            overflow: hidden;
            width: auto;
            height: 150px;
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
                        <li><a data-toggle="tab" href="#menu2"><span class="label label-danger" style="margin-right: 10px;">2</span>Documentos</a></li>
                        <li><a data-toggle="tab" href="#menu3">Provas e Certificados</a></li>
                        <li><a data-toggle="tab" href="#menu4">Solicitações</a></li>
                        <li><a data-toggle="tab" href="#menu5"><span class="label label-danger" style="margin-right: 10px;">3</span>Tickets</a></li>
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

                                    <label for="inputEmail3" class="col-sm-3 control-label">Promoção, Bolsa ou Convênio Cod.:</label>

                                    <div class="col-sm-2">

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

                            <div class="grid-container-pagamento">
                                <!-- Card Menu -->
                                <div class="item2">

                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Forma de Pagamento</h3>

                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                            </div>
                                        </div>

                                        <div class="box-body">


                                            <ul class="nav nav-tabs">
                                                <li class="active"><a data-toggle="tab" href="#cartaoCredito">Cartão de Crédito</a></li>
                                                <li><a data-toggle="tab" href="#cartaoDebito">Cartão de Débito</a></li>
                                                <li><a data-toggle="tab" href="#boleto">Boleto Bancário</a></li>
                                            </ul>

                                            <div class="tab-content">

                                                <!-- Curso e Turma -->
                                                <div id="cartaoCredito" class="tab-pane fade in active">

                                                    <div class="box box-info">

                                                        <div class="box-info">

                                                            <div class="box-body">


                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-6 control-label">Valor da Entrada no Cartão de Crédito:</label>

                                                                    <div class="col-sm-4">
                                                                        <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-6 control-label">Quantide de Parcelas:</label>

                                                                    <div class="col-sm-4">
                                                                        <select class="custom-select form-control">
                                                                            <option selected>Forma de parcelamento</option>
                                                                            <option value="1">1x R$ 500,00</option>
                                                                            <option value="2">2x R$ 250,00</option>
                                                                            <option value="3">3x R$ 166,66</option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>


                                                <div id="cartaoDebito" class="tab-pane fade">
                                                    <div class="box box-info">

                                                        <div class="box-body">

                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-6 control-label">Valor da Entrada no Cartão de Débito:</label>

                                                                <div class="col-sm-4">
                                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="inputEmail3" class="col-sm-6 control-label">Pagamento Total no Cartão de Débito:</label>

                                                                <div class="col-sm-4">
                                                                    <select class="custom-select form-control">
                                                                        <option selected>Pagamento a Vista</option>
                                                                        <option value="1">R$ 1.491,00</option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>

                                                <div id="boleto" class="tab-pane fade">
                                                    <div class="box box-info">
                                                        <div class="form-group">
                                                            <div class="box-body">

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Quantide de Parcelas:</label>

                                                                    <div class="col-sm-4">
                                                                        <select class="custom-select form-control">
                                                                            <option selected>Forma de parcelamento</option>
                                                                            <option value="1">1x R$ 500,00</option>
                                                                            <option value="2">2x R$ 250,00</option>
                                                                            <option value="3">3x R$ 166,66</option>
                                                                        </select>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Data da Primeira Parcela:</label>

                                                                    <div class="col-sm-4">
                                                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Data: DD/MM/AA" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Data da Segunda Parcela:</label>

                                                                    <div class="col-sm-4">
                                                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Data: DD/MM/AA" />
                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                        </div>



                                    </div>


                                    <div class="box box-info">

                                        <div class="box-body">

                                            <div class="form-group">
                                                Taxas de inscrição provas inclusas no valor acima:
                                                <br />
                                                <br />
                                                <table class="table table-bordered table-hover" style="width: 100%">
                                                    <tr>
                                                        <td>Taxa de matrícula:</td>
                                                        <td align="right">R$ isento</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Data vencimento:</td>
                                                        <td align="right">----------</td>
                                                    </tr>
                                                </table>

                                                <table class="table table-bordered" style="width: 100%">
                                                    <tr>
                                                        <td>Incluir Material Didático:</td>
                                                        <td align="right">
                                                            <select class="custom-select form-control">
                                                                <option selected="selected">Incluir Material Didático?</option>
                                                                <option value="1">Sim</option>
                                                                <option value="2">Não</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Valor Material Didático:</td>
                                                        <td align="right">----------</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Data vencimento:</td>
                                                        <td align="right">R$ isento</td>
                                                    </tr>
                                                </table>



                                            </div>

                                        </div>

                                    </div>


                                </div>
                                <!-- Card Main-->
                                <div class="item3">


                                    <div class="box box-info">

                                        <div class="box-body">

                                            <h4 style="color: darkslategrey; text-align: center;">Característica do plano</h4>

                                            <table class="table table-bordered table-hover" style="width: 100%">
                                                <tr>
                                                    <td>Taxa de matrícula:</td>
                                                    <td align="right">
                                                        <p style="color: green;">Grátis</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Material Didático</td>
                                                    <td align="right">
                                                        <p>R$ 138,00</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Inscrição provas</td>
                                                    <td align="right">
                                                        <p>após quitação total</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Dsconto até vencimento</td>
                                                    <td align="right">
                                                        <p>20%</p>
                                                    </td>
                                                </tr>
                                            </table>


                                        </div>


                                        <div class="box-body">
                                            <h3>Detalhes do Pagamento</h3>

                                            <table class="table table-bordered table-hover" style="width: 100%">
                                                <tr>
                                                    <td>Valor Pago: Cartão de Crédito</td>
                                                    <td align="right">R$ 12x R$ 159,00</td>
                                                </tr>
                                                <tr>
                                                    <td>Valor Pago: Cartão de Débito</td>
                                                    <td align="right">----------</td>
                                                </tr>
                                                <tr>
                                                    <td>Parcelamento Boleto:</td>
                                                    <td align="right">----------</td>
                                                </tr>
                                                <tr>
                                                    <td>Valor da Material Didático:</td>
                                                    <td align="right">R$ 138,00</td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table class="table table-bordered table-hover" style="width: 100%">
                                                <tr>
                                                    <td>Saldo Devedor:</td>
                                                    <td align="right">R$ 0,00</td>
                                                </tr>
                                                <tr>
                                                    <td>Valor A Pagar:</td>
                                                    <td align="right">R$ 1.908,00</td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <a class="btn btn-success col-sm-6 pull-right">Ativar Matrícula</a>
                                        </div>



                                    </div>


                                </div>
                                <!-- Card Footer -->
                                <div class="item5">
                                </div>
                            </div>

                            <!-- Fim Forma de Pagamento -->

                            <!-- Painel de Pagamento -->
                            <br />
                            <br />

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
                                                <td>Material Didático crédito 12x</td>
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

                        <!-- Certificados e Provas -->
                        <div id="menu3" class="tab-pane fade">

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Inscrição de Prova</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">


                                    <div class="grid-container-pagamento">
                                        <!-- Card Menu -->
                                        <div class="item2">



                                            <div class="box-body">

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4 control-label">Status da Prova.:</label>

                                                    <div class="col-sm-4">

                                                        <select class="custom-select form-control">
                                                            <option selected></option>
                                                            <option value="1">NÃO INSCRITO</option>
                                                            <option value="2">INSCRITO PARA PROVA</option>
                                                            <option value="3">APROVADO</option>
                                                            <option value="4">REPROVADO</option>
                                                            <option value="4">FALTOU / REPROVADO</option>
                                                        </select>
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4 control-label">Local da Prova.:</label>

                                                    <div class="col-sm-4">

                                                        <select class="custom-select form-control">
                                                            <option selected>Selecione uma unidade:</option>
                                                            <option value="1">Campinas</option>
                                                            <option value="2">Jundiaí</option>
                                                            <option value="3">Valinhos</option>
                                                            <option value="4">São Paulo</option>
                                                        </select>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4 control-label">Data da Prova.:</label>

                                                    <div class="col-sm-4">

                                                        <div class="input-group">

                                                            <i class="fa fa-calendar" style="margin: 10px"></i>


                                                            <input id="date" type="date">
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label for="inputEmail3" class="col-sm-4 control-label">Tipo de Transporte.:</label>

                                                    <div class="col-sm-4">

                                                        <select class="custom-select form-control">
                                                            <option value="1">Transporte da Escola</option>
                                                            <option value="2">Transporte Próprio</option>
                                                        </select>
                                                    </div>
                                                </div>


                                                <br />
                                                <div class="form-group">
                                                    <div class="col-sm-6">
                                                        <a class="btn btn-success">Efetuar Inscrição</a>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <a class="btn btn-danger">Cancelar  Inscrição</a>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="item3">
                                            <div class="box-body">

                                                <table id="example2" class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Data da Viagem:</th>
                                                            <th>17/04/2019</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Número do Ônibus:</th>
                                                            <th>B20850026</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Horário de Saída:</th>
                                                            <th>08:30</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Local de Saída:</th>
                                                            <th>Campinas</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Data da Prova:</th>
                                                            <th>17/04/2019</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Assentos Disponíveis:</th>
                                                            <th>35</th>
                                                        </tr>
                                                    </thead>
                                                </table>

                                            </div>
                                        </div>


                                        <div class="item5">
                                        </div>
                                    </div>


                                </div>


                            </div>

                            <!-- Inicio Realizaçao de provas -->

                            <div class="box-body">

                                <div class="box box-info">

                                    <div class="box-header with-border">
                                        <h3 class="box-title">Provas Realizadas</h3>

                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                        </div>
                                    </div>

                                    <div class="box-body">
                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Data da Prova</th>
                                                    <th>Local</th>
                                                    <th>Status da Prova</th>
                                                    <th>Nº do Ônibus</th>
                                                    <th>Horário de Saída</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>21/03/2020</td>
                                                    <td>São Paulo</td>
                                                    <td>REPROVADO</td>
                                                    <td>Ônibus 01</td>
                                                    <td>17:30</td>
                                                    <td>
                                                        <a class="btn btn-danger" data-toggle="modal" data-target="#modalMaterias"><i class="fa fa-book" style="margin-right: 10px"></i>Matérias</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>


                            <div class="box-body">

                                <div class="box box-info">

                                    <div class="box-header with-border">
                                        <h3 class="box-title">Certificados</h3>

                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                        </div>
                                    </div>

                                    <div class="box-body">

                                        <div class="form-group">

                                            <label for="inputEmail3" class="col-sm-3 control-label">Status do certificado.:</label>

                                            <div class="col-sm-3">

                                                <select class="custom-select form-control">
                                                    <option selected="selected"></option>
                                                    <option value="1">AGUARDANDO EMISSÃO</option>
                                                    <option value="2">DISPONÍVEL PARA RETIRADA</option>
                                                    <option value="3">ENTREGUE AO ALUNO</option>
                                                </select>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail3" class="col-sm-3 control-label">Data do recebimento no suporte.:</label>

                                            <div class="col-sm-4">
                                                <div class="input-group">

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>
                                                    <input id="date" type="date">
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group">


                                            <label for="inputEmail3" class="col-sm-3 control-label">Data de entrega ao aluno.:</label>

                                            <div class="col-sm-3">
                                                <div class="input-group">

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>
                                                    <input id="date" type="date">
                                                </div>
                                            </div>

                                        </div>

                                        <div class="form-group">


                                            <label for="inputEmail3" class="col-sm-3 control-label">GDAE.:</label>


                                            <div class="col-sm-3">
                                                <input type="email" class="form-control" placeholder="GDAE">
                                            </div>


                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-2">
                                                <a class="btn btn-success pull-right">Salvar</a>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <!-- Fim Realização de provas-->
                        </div>

                        <!-- Certificados e Provas -->

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
                        <!-- Fim TAB Solicitações -->

                        <!-- TAB Ticket -->
                        <div id="menu5" class="tab-pane fade">

                            <div class="box box-info">
                                <div class="box-header with-border">
                                </div>

                                <form class="form-horizontal">
                                    <div class="box-body">
                                        <a class="btn btn-success pull-right" data-toggle="modal" data-target="#abrirTicketModal">Abrir Ticket</a>
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
                                </form>
                            </div>

                        </div>


                        <!-- TAB Comunicação -->
                        <div id="menu6" class="tab-pane fade">


                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Disparo de Notificações</h3>

                                    <div class="box-tools pull-right">

                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">


                                    <div class="row">

                                        <div class="col-lg-6 col-xs-6">

                                            <div class="box-body">

                                                <div class="box box-info">
                                                    <div class="box-header ui-sortable-handle" style="cursor: move;">
                                                        <i class="fa fa-comments-o"></i>

                                                        <h3 class="box-title">Disparo de Mensagens</h3>

                                                    </div>
                                                    <div class="slimScrollDiv">
                                                        <div class="box-body chat" id="chat-box" style="overflow: hidden; width: auto; height: 150px;">
                                                            <!-- chat item -->
                                                            <div class="item">
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


                                                            <!-- chat item -->
                                                            <div class="item">
                                                                <img src="dist/img/boxed-bg.jpg" alt="user image" class="online">

                                                                <p class="message">
                                                                    <a href="#" class="name">
                                                                        <small class="text-muted pull-right"><i class="fa fa-clock-o"></i>5:30</small>
                                                                        Equipe Escola Modelo
                                                                    </a>
                                                                    Bem vindo a Escola Modelo
                                                                </p>
                                                            </div>
                                              
                                                            <!-- /.item -->
                                                            <div class="item">
                                                                <textarea class="form-control" placeholder="Escreva sua mensagem...">
                                                                </textarea>
                                                            </div>
                                                   
                                                        </div>
                                                        <div class="slimScrollBar">
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

                                        <div class="col-lg-6 col-xs-6">

                                            <div class="box-body">

                                                <div class="box box-info">
                                                    <div class="box-header ui-sortable-handle" style="cursor: move;">
                                                        <i class="fas fa-history"></i>

                                                        <h3 class="box-title">Histórico de Mensagens</h3>

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
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <!-- Modal Comunicação Detalhes da mensagem -->
                <div class="container">

                    <div class="modal fade" id="comunicacaoModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="alert alert-info alert-dismissible">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h4><i class="icon fa fa-check"></i>Mensagem enviada!</h4>
                                Seu boleto foi enviado no dia 15/05/2020.   
                            </div>

                        </div>
                    </div>
                </div>

                <!-- Fim Modal Comunicação Detalhes da mensagem -->

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



                <!-- Materias Reprovadas -->
                <div class="container">

                    <div class="modal fade" id="modalMaterias" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Relação de Matérias</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="box box-info"></div>
                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Matéria</th>
                                                <th>Status</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>L. Portuguesa</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Arte</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Matemática</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Biologia</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Química	História</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Geografia</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Filosofia</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Sociologia</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">APROVADO</option>
                                                        <option value="2">REPROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="color: red">Inglês</td>
                                                <td>
                                                    <select class="custom-select form-control">
                                                        <option value="1">REPROVADO</option>
                                                        <option value="2">APROVADO</option>
                                                    </select>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                    <div class="container">
                                        <div class="form-group">

                                            <div class="col-sm-7">
                                                <textarea placeholder="Observação..." style="width: 600px; height: 50px"></textarea>

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

                <!-- Fim Materias Reprovadas -->




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
                                                    <span class="bg-red">Abertura do Chamada 21 Março. 2020
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
                                                        <textarea style="width: 600px; height: 100px"></textarea>
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
                <!-- Fim Modal Ticket -->


                <!-- Modal Ticket Abrir -->
                <!-- Modal Ticket - Detalhes -->
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
                                            <label for="inputEmail3" class="col-sm-3 control-label">Unidade.:</label>

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
                                            <label for="inputEmail3" class="col-sm-5 control-label">Usuário do Departamento selecionado.:</label>
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

                <!-- Fim - Modal Ticker Abrir -->





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
                                                            <a class="btn btn-dropbox pull-right" runat="server" href="~/PainelMatriculaAluno"
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
                                                                            <li><a runat="server" href="~/PainelMatriculaAluno">Ver matrícula</a></li>
                                                                            <li><a runat="server" href="~/PainelMatriculaAluno">Ver financeiro</a></li>
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
