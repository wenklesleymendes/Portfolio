<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PlanosPagamento.aspx.cs" Inherits="EscolaPro.Web.PlanosPagamento" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <%--   <style>
        .modal-dialog {
            min-width: 80vw
        }
    </style>--%>


    <style>
        .container-pagamento {
            background-color: dodgerblue;
            box-shadow: rgb(128, 128, 128), 3px 3px;
        }

        .weekDays-selector input {
            display: none !important;
        }

            .weekDays-selector input[type=radio] + label {
                display: inline-block;
                border-radius: 6px;
                background: #77c2fc;
                height: 40px;
                width: 180px;
                margin-right: 3px;
                line-height: 40px;
                text-align: center;
                cursor: pointer;
            }

            .weekDays-selector input[type=radio]:checked + label {
                /*background: #2b61b3;*/
                background: #13d61d;
                color: #ffffff;
            }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Planos de Pagamentos</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">



                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalNovoPlano">Adicionar Novo Plano</a>
                <br />
                <br />
                <form class="form-horizontal">

                    <div class="box-body">

                        <div class="form-group">

                            <table id="example2" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Tipo de Pagamento</th>
                                        <th>Nº Parcelas</th>
                                        <th>Valor da Parcela</th>
                                        <th>Valor Total do Plano</th>
                                        <th>Cursos</th>
                                        <th>Unidades</th>
                                        <th style="width: 10%"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Cartão de Crédito</td>
                                        <td>12</td>
                                        <td>R$ 100,00</td>
                                        <td>R$ 1.200,00</td>
                                        <td>Técnico em Administração</td>
                                        <td>Campinas, Jundiaí</td>
                                        <td>
                                            <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                                            <a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-power-off" style="color: gold; margin-left: 10px; font-size: large" title="Desativar"></i></a>
                                            <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Modal para adicionar novo plano -->

    <div class="container">
        <div class="modal fade" id="modalNovoPlano" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Nova Plano de Pagamento</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Informações do Plano</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <form class="form-horizontal">

                                        <div class="box-body">

                                            <div class="form-group">
                                                <div class="container">
                                                    <form>
                                                        <div class="btn-group" style="margin: 10px">
                                                            <label for="cars">Tipos de Pagamentos</label>
                                                            <br />

                                                            <div class="weekDays-selector">
                                                                <input type="radio" id="weekday-mon" class="weekday" name="optradio" />
                                                                <label for="weekday-mon">
                                                                    <i class="fas fa-credit-card" style="margin-right: 10px"></i>Cartão de Crédito</label>
                                                                <input type="radio" id="weekday-tue" class="weekday" name="optradio" />
                                                                <label for="weekday-tue">
                                                                    <i class="fas fa-money-check-alt" style="margin-right: 10px"></i>Cartão de Débito</label>
                                                                <input type="radio" id="weekday-wed" class="weekday" name="optradio" />
                                                                <label for="weekday-wed">
                                                                    <i class="fas fa-receipt" style="margin-right: 10px"></i>Boleto Bancário</label>
                                                            </div>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>


                                            <div class="form-group">

                                                <%-- <div class="btn-group" style="margin: 10px; width: 70%">
                                                    <label for="cars">Nome do Plano:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Plano">
                                                </div>--%>



                                                <div class="btn-group" style="margin: 10px; width: 45%">
                                                    <label for="cars">Quantidade de Parcela:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Quantidade de Parcelas</option>
                                                        <option value="1">1</option>
                                                        <option value="2">2</option>
                                                        <option value="3">3</option>
                                                        <option value="3">4</option>
                                                        <option value="3">5</option>
                                                        <option value="3">6</option>
                                                        <option value="3">7</option>
                                                        <option value="3">8</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Valor da Parcela:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Valor Total do Plano:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto Pontualidade:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Desconto %">
                                                </div>


                                            </div>


                                            <div class="form-group">
                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Inscrição Provas Valor Total:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" value="R$ 138,00">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <br />
                                                    <label for="cars" class="label label-danger">Valor de 2x de R$69,00</label>

                                                </div>
                                            </div>


                                            <div class="form-group">


                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Material Didático:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                    <select class="custom-select form-control">
                                                        <option selected>Deseja Isentar?</option>
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Taxa de Matrícula:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                    <select class="custom-select form-control">
                                                        <option selected>Deseja Isentar?</option>
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>
                                                </div>




                                            </div>

                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>


                        <div class="row">



                            <div class="col-sm-6 col-lg-6">

                                <div class="box-body">
                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Curso</h3>

                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                            </div>
                                        </div>


                                        <div class="box-body">

                                            <form class="form-horizontal">

                                                <div class="box-body">

                                                    <div class="form-group">


                                                        <div class="col-sm-9">
                                                            <select class="custom-select form-control">
                                                                <option selected>Selecionar Todos</option>
                                                                <option value="1">Ensino Médio</option>
                                                                <option value="2">Ensino Fundamental</option>
                                                                <option value="3">Téc. em Administração</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <a class="btn btn-success">Adicionar</a>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Curso</th>
                                                                <th style="width: 5%"></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Ensino Médio</td>
                                                                <td><a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i></a></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </form>
                                        </div>

                                    </div>
                                </div>
                                <div class="footer">
                                    <a class="btn btn-success pull-right">Salvar</a>
                                </div>
                            </div>

                            <div class="col-sm-6 col-lg-6">

                                <div class="box-body">
                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Unidade</h3>

                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                            </div>
                                        </div>

                                        <div class="box-body">

                                            <form class="form-horizontal">

                                                <div class="box-body">

                                                    <div class="form-group">


                                                        <div class="col-sm-9">
                                                            <select class="custom-select form-control">
                                                                <option selected>Todas Unidades</option>
                                                                <option value="1">Campinas</option>
                                                                <option value="2">Jundiaí</option>
                                                                <option value="3">São Paulo</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <a class="btn btn-success">Adicionar</a>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Unidade</th>
                                                                <th style="width: 5%"></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Campinas</td>
                                                                <td><a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i></a></td>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </form>
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
