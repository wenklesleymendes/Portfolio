<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ConsultarAluno.aspx.cs" Inherits="EscolaPro.Web.ConsultarAluno" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link rel="stylesheet" href="dist/css/font-awesome.min.css">

    <link rel="stylesheet" href="dist/css/font-awesome.css">

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .modal-dialog {
            min-width: 80vw
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

        /*.cl-menu li ul {
            display: none;
            position: absolute;
            right: 20%;
        }*/
    </style>


</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Lista de Alunos</h3>


                        <button type="button" class="btn btn-dropbox btn-sm" style="float: right" data-toggle="modal" data-target="#myModal">
                            <span class="fa fa-search" style="margin-right: 10px"></span>Consultar Alunos
                        </button>


                    </div>

                    <div class="box-body">
                        <table id="example2" class="table table-bordered table-hover">
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent"
                                            data-toggle="modal" data-target="#visualizarModal">
                                        </button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>00233</td>
                                    <td>Jundiaí</td>
                                    <td>Ricardo Castro</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>123283</td>
                                    <td>São Paulo</td>
                                    <td>Victor Berezutchi</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>00233</td>
                                    <td>Jundiaí</td>
                                    <td>Ricardo Castro</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>123283</td>
                                    <td>São Paulo</td>
                                    <td>Victor Berezutchi</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>00233</td>
                                    <td>Jundiaí</td>
                                    <td>Ricardo Castro</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>123283</td>
                                    <td>São Paulo</td>
                                    <td>Victor Berezutchi</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>00233</td>
                                    <td>Jundiaí</td>
                                    <td>Ricardo Castro</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>123283</td>
                                    <td>São Paulo</td>
                                    <td>Victor Berezutchi</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>00233</td>
                                    <td>Jundiaí</td>
                                    <td>Ricardo Castro</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>123283</td>
                                    <td>São Paulo</td>
                                    <td>Victor Berezutchi</td>
                                    <td>Ativo</td>
                                    <td>Ensino Médio</td>
                                    <td>2020</td>
                                    <td>1º Semestre</td>
                                    <td>and@hotmail.com</td>
                                    <td>19 997016077</td>
                                    <td>
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
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
                                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent" data-toggle="modal" data-target="#visualizarModal"></button>
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
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
                                    <th></th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>

    </section>



    <div class="container">
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Filtrar Aluno</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">
                            <div class="box-body">

                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">Unidade.:</label>

                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione uma unidade</option>
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>

                                </div>


                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">Nome do Aluno.:</label>

                                    <input type="email" class="form-control" placeholder="Nome do Aluno">
                                </div>

                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">CPF.:</label>

                                    <input type="email" class="form-control" placeholder="CPF">
                                </div>

                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">Data Nasc.:</label>

                                    <input type="email" class="form-control" placeholder="Data Nasc.:">
                                </div>

                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">Celular.:</label>


                                    <input type="email" class="form-control" placeholder="Celular">
                                </div>

                                <div class="form-group" style="margin: 10px">
                                    <label for="inputEmail3">E-mail:</label>


                                    <input type="email" class="form-control" placeholder="E-mail">
                                </div>

                                <div style="margin: 10px">
                                    <label for="cars">Nº Matrícula:</label>

                                    <input type="email" class="form-control" placeholder="Matrícula">
                                    <br />
                                    <label>Data da Matrícula:</label>

                                    <div class="input-group">

                                        <i class="fa fa-calendar" style="margin: 10px"></i>


                                        <input id="date" type="date">

                                        <span style="margin-left: 10px; margin-right: 10px">a</span>

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">
                                    </div>
                                </div>

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
                                    <label for="cars">Status da Matrícula:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Status Documentos:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>


                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Status do Certificado:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Local da prova:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option selected>Selecione um curso</option>
                                        <option value="1">Ensino Fundamental</option>
                                        <option value="2">Ensino Médio</option>
                                        <option value="3">Ensino Fundamental + Médio</option>
                                    </select>

                                </div>

                                <div class="form-group" style="margin: 10px">
                                    <label for="cars">Nº Matrícula:</label>

                                    <input type="email" class="form-control" placeholder="Matrícula">
                                    <br />
                                    <label>Data da prova:</label>

                                    <div class="input-group">

                                        <i class="fa fa-calendar" style="margin: 10px"></i>


                                        <input id="date" type="date">

                                        <span style="margin-left: 10px; margin-right: 10px">a</span>

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">
                                    </div>

                                </div>

                            </div>

                        </form>

                    </div>


                    <div class="modal-footer">
                        <button type="button" class="btn btn-dropbox" style="float: left" data-dismiss="modal">Pesquisar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Sair</button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="container">

        <div class="modal fade" id="visualizarModal" role="dialog">
            <div class="modal-dialog modal-lg">

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
                                                                    <%--<li class="divider"></li>
                                                                      <li class="dropdown-submenu">
                                                                        <a class="test" href="#">Declarações formulários <span class="caret"></span></a>
                                                                        <ul class="dropdown-menu pull-right">
                                                                            <li><a href="#">Declaração cursando</a></li>
                                                                            <li><a href="#">Declaração de provas</a></li>
                                                                            <li><a href="#">Declaração de apostila</a></li>
                                                                            <li><a href="#">Carta de cancelamento</a></li>
                                                                            <li><a href="#">Contrato</a></li>
                                                                            <li><a href="#">Inscrição provas fundamental</a></li>
                                                                            
                                                                            <li><a href="#">Inscrição prova e. médio</a></li>
                                                                        </ul>
                                                                    </li>--%>

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
</asp:Content>
