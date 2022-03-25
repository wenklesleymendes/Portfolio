<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CriarComissao.aspx.cs" Inherits="EscolaPro.Web.CriarComissao" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Criação de Comissão</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>


            <div class="box-body">

                <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Busca Avançada<span class="fas fa-sort-amount-down" style="margin-left: 10px"></span></a>

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarComissao">Adicionar Comissão</a>
                <br />
                <br />

                <div class="collapse" id="collapseExample">
                    <div class="card card-body">

                        <div class="form-group">

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Unidade:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option value="1">Campinas</option>
                                    <option value="2">Jundiaí</option>
                                    <option value="3">São Paulo</option>
                                </select>
                            </div>


                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Tipo do Pagamento:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option value="1">Cartão de Crédito</option>
                                    <option value="3">Cartão de Débito</option>
                                    <option value="4">Boleto Bancário</option>
                                </select>
                            </div>

                            <div class="btn-group" style="margin: 10px">

                                <label style="margin-left: 30px">Período:</label>

                                <div class="input-group">

                                    <i class="fa fa-calendar" style="margin: 10px"></i>


                                    <input id="date" type="date">

                                    <span style="margin-left: 10px; margin-right: 10px">a</span>

                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                    <input id="date" type="date">
                                </div>
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars"></label>
                                <br />
                                <a class="btn btn-dropbox"><span class="fa fa-search" style="margin-right: 10px"></span>Buscar</a>
                            </div>
                        </div>
                    </div>
                </div>

                <br />
                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Unidade</th>
                            <th>Início da Vigência</th>
                            <th>Término da Vigência</th>
                            <th>Tipo da Comissão</th>
                            <th>Tipo de Pagamento</th>
                            <th style="width: 2%"></th>
                            <th style="width: 2%"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Campinas</td>
                            <td>05/01/2020</td>
                            <td>28/07/2020</td>
                            <td>Individual</td>
                            <td>Crédito</td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Jundiaí</td>
                            <td>16/06/2020</td>
                            <td>31/12/2020</td>
                            <td>Equipe</td>
                            <td>Crédito</td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarMeta"><i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>


        </div>
    </div>



    <!-- Modal Adicionar Comissão -->
    <div class="container">
        <div class="modal fade" id="modalCriarComissao" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Nova Comissão</h4>
                    </div>


                    <div class="modal-body">

                        <div class="box box-info">

                            <div class="form-group">

                                <div class="btn-group" style="margin: 10px; width: 25%">
                                    <label for="cars">Unidade:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Campinas</option>
                                        <option value="2">Jundiaí</option>
                                        <option value="3">São Paulo</option>
                                    </select>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars">Tipo do Pagamento:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Cartão de Crédito</option>
                                        <option value="2">Cartão de Débito</option>
                                        <option value="3">Boleto Bancário</option>
                                    </select>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars">Tipo do Comissão:</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Equipe</option>
                                        <option value="2">Individual</option>
                                    </select>
                                </div>


                                <div class="btn-group" style="margin: 10px">

                                    <label style="margin-left: 30px">Vigência da Comissão:</label>

                                    <div class="input-group">

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">

                                        <span style="margin-left: 10px; margin-right: 10px">a</span>

                                        <i class="fa fa-calendar" style="margin: 10px"></i>

                                        <input id="date" type="date">
                                    </div>
                                </div>


                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars">Período Indeterminado</label>
                                    <br />
                                    <select class="custom-select form-control">
                                        <option value="1">Não</option>
                                        <option value="2">Sim</option>
                                    </select>
                                </div>

                                <br />
                                <br />

                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th style="width: 2px"></th>
                                            <th>Parcela</th>
                                            <th style="width: 150px">Valor da Comissão</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>1ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>2ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>3ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>4ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>5ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>6ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>7ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>8ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>9ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>10ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>11ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>12ª Parcela</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="checkbox" class="checkbox-inline" /></td>
                                            <td>Pagamento Total</td>
                                            <td>
                                                <input type="email" class="form-control" placeholder="R$">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <div class="btn-group" style="margin: 10px; width: 67%">
                                </div>


                            </div>

                        </div>

                        <div class="box-footer">
                            <a class="btn btn-success pull-right">Salvar Comissão</a>
                        </div>

                    </div>


                </div>
            </div>
        </div>
    </div>



</asp:Content>
