<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CursoTurma.aspx.cs" Inherits="EscolaPro.Web.CursoTurma" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
        }

        .weekDays-selector input {
            display: none !important;
        }

            .weekDays-selector input[type=checkbox] + label {
                display: inline-block;
                border-radius: 6px;
                background: #dddddd;
                height: 40px;
                width: 70px;
                margin-right: 3px;
                line-height: 40px;
                text-align: center;
                cursor: pointer;
            }

            .weekDays-selector input[type=checkbox]:checked + label {
                background: #2b61b3;
                color: #ffffff;
            }
    </style>



</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Gerenciador de Cursos e Turmas</h3>
        </div>


        <form class="form-horizontal">

            <div class="box-body">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#curso">Curso</a></li>
                    <li><a data-toggle="tab" href="#turma">Turma</a></li>
                </ul>

                <div class="tab-content">
                    <div id="curso" class="tab-pane fade in active">

                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Cursos</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>


                            <div class="box-body">

                                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCurso"><i class="fas fa-book-reader" style="margin-right: 5px"></i>Adicionar</a>
                                <br />
                                <br />
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Descrição</th>
                                            <th style="width: 8%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Ensino Médio</td>
                                            <td>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ensino Fundamental</td>
                                            <td>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>


                        </div>

                    </div>

                    <div id="turma" class="tab-pane fade">


                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Cursos</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalTurma"><i class="fas fa-chalkboard-teacher" style="margin-right: 5px"></i>Adicionar</a>
                                <br />
                                <br />
                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Unidade</th>
                                            <th>Curso</th>
                                            <th>Modalidade</th>
                                            <th>Ano</th>
                                            <th>Semestre</th>
                                            <th>Dia da Semana</th>
                                            <th>Período</th>
                                            <th>Horário</th>
                                            <th>Sala</th>
                                            <th>Disponível</th>
                                            <th>Vagas</th>
                                            <th style="width: 10%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Todos</td>
                                            <td>Ensino Médio</td>
                                            <td>Presencial</td>
                                            <td>2020</td>
                                            <td>1º Semestre</td>
                                            <td>Qua-Qui-Sex</td>
                                            <td>Noite</td>
                                            <td>19:00-21:45</td>
                                            <td>Sala 2</td>
                                            <td>Sim</td>
                                            <td>65</td>
                                            <td>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="far fa-copy" style="color: gold; margin-left: 10px; font-size: large" title="Replicar Item"></i></a>
                                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Campinas</td>
                                            <td>Ensino Fundamental</td>
                                            <td>Presencial</td>
                                            <td>2020</td>
                                            <td>1º Semestre</td>
                                            <td>Qua-Qui-Sex</td>
                                            <td>Noite</td>
                                            <td>19:00-21:45</td>
                                            <td>Sala 2</td>
                                            <td>Sim</td>
                                            <td>40</td>
                                            <td>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                                <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="far fa-copy" style="color: gold; margin-left: 10px; font-size: large" title="Replicar Item"></i></a>
                                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                            </td>
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



    <!-- Criar Cursos -->
    <div class="container">
        <div class="modal fade" id="modalCurso" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Novo Curso</h4>
                    </div>
                    <div class="modal-body">
                        <form class="form-horizontal">
                            <div class="box box-info">
                                <div class="box-body">

                                    <div class="input-group">
                                        <input id="new-event" type="text" class="form-control" placeholder="Descrição do Curso">
                                        <div class="input-group-btn">
                                            <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Salvar</button>
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



    <!-- Criar Turma -->
    <div class="container">
        <div class="modal fade" id="modalTurma" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Nova Turma</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Criação de Turmas</h3>

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
                                                        <option selected>Selecione uma curso</option>
                                                        <option value="1">Ensino Médio</option>
                                                        <option value="2">Ensino Fundamental</option>
                                                        <option value="3">Ensino Médio e Fundamental</option>
                                                    </select>

                                                </div>

                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Modalidade:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Modalidade</option>
                                                        <option value="1">Presencial</option>
                                                        <option value="2">Distância</option>
                                                    </select>

                                                </div>

                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Ano:</label>
                                                    <br />
                                                    <select id="year" class="form-control"></select>
                                                    <%--<input type="email" class="form-control" id="inputEmail3" placeholder="Ano">--%>
                                                </div>

                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Semestre:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Semestre</option>
                                                        <option value="1">1º semestre</option>
                                                        <option value="2">2º semestre</option>
                                                    </select>

                                                </div>


                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Dia da Semana:</label>
                                                    <br />
                                                    <div class="weekDays-selector">
                                                        <input type="checkbox" id="weekday-mon" class="weekday" />
                                                        <label for="weekday-mon">Segunda</label>
                                                        <input type="checkbox" id="weekday-tue" class="weekday" />
                                                        <label for="weekday-tue">Terça</label>
                                                        <input type="checkbox" id="weekday-wed" class="weekday" />
                                                        <label for="weekday-wed">Quarta</label>
                                                        <input type="checkbox" id="weekday-thu" class="weekday" />
                                                        <label for="weekday-thu">Quinta</label>
                                                        <input type="checkbox" id="weekday-fri" class="weekday" />
                                                        <label for="weekday-fri">Sexta</label>
                                                        <input type="checkbox" id="weekday-sat" class="weekday" />
                                                        <label for="weekday-sat">Sabado</label>
                                                        <input type="checkbox" id="weekday-sun" class="weekday" />
                                                        <label for="weekday-sun">Domingo</label>
                                                    </div>
                                                </div>

                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Período:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione um Período</option>
                                                        <option value="1">Manhã</option>
                                                        <option value="2">Tarde</option>
                                                        <option value="3">Noite</option>
                                                    </select>

                                                </div>


                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Horário de Inicio:</label>
                                                    <br />

                                                    <input type="time" id="appt" name="appt" class="form-control"
                                                        min="09:00" max="18:00" required>
                                                </div>

                                                <div class="btn-group" style="margin: 10px">
                                                    <label for="cars">Horário de Término:</label>
                                                    <br />

                                                    <input type="time" id="appt" name="appt" class="form-control"
                                                        min="09:00" max="18:00" required>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width:40%">
                                                    <label for="cars">Sala:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione uma sala</option>
                                                        <option value="1">Sala 01</option>
                                                        <option value="2">Sala 02</option>
                                                        <option value="3">Sala 03</option>
                                                        <option value="4">Sala 04</option>
                                                        <option value="5">Sala 05</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width:30%">
                                                    <label for="cars">Disponível:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione disponibilidade</option>
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>

                                                </div>

                                                 <div class="btn-group" style="margin: 10px;width:30%">
                                                    <label for="cars">Vagas:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Quantidade">
                                                </div>


                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>


                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Adicionar Unidade</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>


                                <div class="box-body">

                                    <form class="form-horizontal">

                                        <div class="box-body">


                                            <div class="input-group">
                                                <label for="cars">Unidade:</label>
                                                <select class="custom-select form-control">
                                                    <option selected>Selecione uma Unidade</option>
                                                    <option value="1">Campinas</option>
                                                    <option value="2">Jundiai</option>
                                                    <option value="3">São Paulo</option>
                                                </select>
                                                <div class="input-group-btn" style="vertical-align: bottom">
                                                    <button type="button" class="btn btn-primary btn-flat">Adicionar</button>
                                                </div>
                                            </div>

                                            <br />

                                            <table id="example2" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Unidades Adicionadas</th>
                                                        <th style="width: 10%"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Jundiaí</td>
                                                        <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Campinas</td>
                                                        <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </form>
                                </div>
                            </div>

                            <div class="footer">
                                <a class="btn btn-success pull-right">Salvar</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
