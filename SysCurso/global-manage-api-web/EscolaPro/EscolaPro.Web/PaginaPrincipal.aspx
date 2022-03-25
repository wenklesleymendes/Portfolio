<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PaginaPrincipal.aspx.cs" Inherits="EscolaPro.Web.PaginaPrincipal" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">




    <div class="row">

        <div class="col-sm-2">

            <div class="box-body">
                <div class="small-box bg-yellow">
                    <div class="inner">
                        <h3>44</h3>
                        <br />
                        <p>Ticket: Abertos</p>
                    </div>
                    <div class="icon">
                        <i class="ion ion-thumbsup"></i>
                    </div>
                </div>
                <br />
                <div class="small-box bg-red">
                    <div class="inner">
                        <h3>65</h3>
                        <br />
                        <p>Ticket: Atrasado</p>
                    </div>
                    <div class="icon">
                        <i class="ion ion-android-alert"></i>
                    </div>
                </div>
            </div>

        </div>

        <div class="col-sm-10">


            <div class="box-body">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Painel de Informações</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>
                    </div>

                    <div class="box-body">


                        <div class="callout callout-danger">
                            <h4>Comunicado</h4>

                            <p>
                                O prazo do pagamento de bonificação será encerrado no dia 20/04.
                            </p>
                        </div>

                        <div class="callout callout-info">
                            <h4>Mensagem do dia!</h4>

                            <p>Desafie seus paradigmas e ultrapasse seus limites!</p>
                        </div>


                    </div>
                </div>
            </div>

        </div>

    </div>


    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Lista de Ocorrências</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

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
    </div>




    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Turmas Disponíveis</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <div class="btn-group" style="margin: 10px; width: 20%">
                    <label for="cars">Ano:</label>
                    <br />
                    <select class="custom-select form-control">
                        <option value="1">2018</option>
                        <option value="2">2019</option>
                        <option value="3">2020</option>
                    </select>
                </div>

                <div class="btn-group" style="margin: 10px; width: 20%">
                    <label for="cars">Semestre:</label>
                    <br />
                    <select class="custom-select form-control">
                        <option value="1">1ª Semestre</option>
                        <option value="2">2ª Semestre</option>
                        <option value="3">3ª Semestre</option>
                        <option value="4">4ª Semestre</option>
                    </select>
                </div>

                <div class="btn-group" style="margin: 10px; width: 20%">
                    <label for="cars"></label>
                    <br />
                    <a><span class="fa fa-search" style="margin-right: 10px; font-size: medium" data-toggle="tooltip" title="Buscar"></span></a>
                </div>

                <div class="btn-group" style="margin: 10px; width: 30%">
                    <label for="cars"></label>
                    <br />
                    <a href="CursoTurma.aspx" class="btn btn-primary pull-right">Abrir Nova Turma</a>
                </div>


                <br />
                <br />
                <br />
                <table id="example2" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Ano</th>
                            <th>Semestre</th>
                            <th>Período</th>
                            <th>Dia da Semana</th>
                            <th>Sala</th>
                            <th>Horário</th>
                            <th>Cursos</th>
                            <th>Vagas</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>2019</td>
                            <td>1ª Semestre</td>
                            <td>Manhã</td>
                            <td>Segunda</td>
                            <td>Sala 01</td>
                            <td>08:00-17:00</td>
                            <td><span class="fas fa-book-reader text-center" style="font-size: large; color: dodgerblue; text-align: center" data-toggle="tooltip" data-html="true" data-original-title="Ensino Médio<br />Ensino Fundamental<br />Técnico em Administração"></span></td>
                            <td><span class="label label-danger">0 (Fechada)</span></td>
                            <td>
                                <a data-toggle="modal" data-target="#modalTurma"><i data-toggle="tooltip" class="fas fa-edit" style="color: gold; margin-left: 10px; font-size: large" title="Adicionar Vagas"></i></a>
                            </td>
                            <td>
                                <a href="ConsultarAluno.aspx"><i data-toggle="tooltip" class="fas fa-search" style="margin-left: 10px; color: dodgerblue; font-size: large" title="Visualizar Turma"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td>2019</td>
                            <td>2ª Semestre</td>
                            <td>Manhã</td>
                            <td>Segunda</td>
                            <td>Sala 02</td>

                            <td>08:00-17:00</td>
                            <td><span class="fas fa-book-reader text-center" style="font-size: large; color: dodgerblue; text-align: center" data-toggle="tooltip" data-html="true" data-original-title="Ensino Médio<br />Ensino Fundamental<br />Técnico em Administração"></span></td>

                            <td><span class="label label-success">15</span></td>

                            <td>
                                <a data-toggle="modal" data-target="#modalTurma"><i data-toggle="tooltip" class="fas fa-edit" style="color: gold; margin-left: 10px; font-size: large" title="Adicionar Vagas"></i></a>
                            </td>
                            <td>
                                <a href="ConsultarAluno.aspx"><i data-toggle="tooltip" class="fas fa-search" style="margin-left: 10px; color: dodgerblue; font-size: large" title="Visualizar Turma"></i></a>
                            </td>
                        </tr>
                    </tbody>
                </table>

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

                                                <div class="btn-group" style="margin: 10px; width: 40%">
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

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Disponível:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione disponibilidade</option>
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>

                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 30%">
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
