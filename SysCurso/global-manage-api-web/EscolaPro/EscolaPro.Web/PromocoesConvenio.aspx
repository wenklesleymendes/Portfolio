<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PromocoesConvenio.aspx.cs" Inherits="EscolaPro.Web.PromocoesConvenio" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        .modal-dialog {
            min-width: 80vw
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Promoções, Bolsas e Convênios</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">



                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalNovoPlano">Adicionar Desconto</a>
                <br />
                <br />
                <form class="form-horizontal">

                    <div class="box-body">

                        <div class="form-group">

                            <table id="example2" class="table table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Nome da Campanha</th>
                                        <th>Desconto Aplicado</th>
                                        <th>Plano</th>
                                        <th>Matrícula</th>
                                        <th>Material Didático</th>
                                        <th>Validade do Desconto</th>
                                        <th>Planos</th>
                                        <th>Unidades</th>
                                        <th style="width: 8%"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Convênio com Faculdade</td>
                                        <td>3x</td>
                                        <td>3% Desconto</td>
                                        <td>10% Desconto</td>
                                        <td>5% Desconto</td>
                                        <td>25/06/2020</td>
                                        <td>Ensino Médio</td>
                                        <td>Campinas, Jundiaí</td>
                                        <td>
                                            <a data-toggle="modal" data-target="#modalNovoPlano"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
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
                        <h4 class="modal-title">Adicionar Nova Campanha</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Promoções, Bolsas e Convênios</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <form class="form-horizontal">

                                        <div class="box-body">
                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px; width: 72%">
                                                    <label for="cars">Nome da Campanha:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Digite o nome da promoção, bolsa ou convênio">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 20%">
                                                    <label for="cars">Aplicar Desconto:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Valor Total</option>
                                                        <option value="1">1 Parcela</option>
                                                        <option value="2">2 Parcelas</option>
                                                        <option value="3">3 Parcelas</option>
                                                        <option value="3">4 Parcelas</option>
                                                        <option value="3">5 Parcelas</option>
                                                        <option value="3">6 Parcelas</option>
                                                        <option value="3">7 Parcelas</option>
                                                        <option value="3">8 Parcelas</option>
                                                        <option value="3">9 Parcelas</option>
                                                        <option value="3">10 Parcelas</option>
                                                        <option value="3">11 Parcelas</option>
                                                        <option value="3">12 Parcelas</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto: Plano de Pagamento</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="% Porcentagem">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto: Taxa de Matrícula</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="% Porcentagem">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto: Material Didático</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="% Porcentagem">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto: Taxa de Inscrições de Provas</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="% Porcentagem">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 20%">
                                                    <label for="cars">Exige Comprovante:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>
                                                </div>

                                            </div>

                                            <div class="form-group">

                                                <div class="input-group" style="margin: 10px">

                                                    <label>Data de Validade da Campanha:</label>

                                                    <div class="input-group">

                                                        <i class="fa fa-calendar" style="margin: 10px"></i>


                                                        <input id="date" type="date">

                                                        <span style="margin-left: 10px; margin-right: 10px">a</span>

                                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                                        <input id="date" type="date">
                                                    </div>
                                                </div>



                                            </div>

                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>



                        <div class="box-body">

                            <div class="row">

                                <div class="col-sm-6 col-lg-6">

                                    <div class="box-body">
                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Unidades</h3>

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
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>

                                                </form>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-6 col-lg-6">

                                    <div class="box-body">
                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Cursos</h3>

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
                                                                    <option value="3">Tec. em Administração</option>
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
                                </div>
                            </div>




                            <div class="row">



                                <div class="col-sm-6 col-lg-6">

                                    <div class="box-body">
                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Tipo de Pagamentos</h3>

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
                                                                    <option value="1">Cartão de Crédito</option>
                                                                    <option value="2">Cartão de Débito</option>
                                                                    <option value="3">Boleto Bancário</option>
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
                                                                    <th>Tipo de Pagamento</th>
                                                                    <th style="width: 5%"></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>Cartão de Crédito</td>
                                                                    <td><a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i></a></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>

                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6 col-lg-6">

                                    <div class="box-body">
                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Forma de Pagamentos</h3>

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
                                                                    <option value="1">Cartão de Crédito 12x R$ 100,00</option>
                                                                    <option value="2">Cartão de Débito 12x R$ 120,00</option>
                                                                    <option value="3">Boleto Bancário 12x R$ 95,00</option>
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
                                                                    <th>Plano</th>
                                                                    <th style="width: 5%"></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>Cartão de Crédito 12x R$ 100,00</td>
                                                                    <td><a data-toggle="modal" data-target="#modalCurso"><i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i></a></td>
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

