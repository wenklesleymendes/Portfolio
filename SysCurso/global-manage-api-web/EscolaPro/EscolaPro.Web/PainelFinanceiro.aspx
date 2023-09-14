<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PainelFinanceiro.aspx.cs" Inherits="EscolaPro.Web.PainelFinanceiro" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        img {
            border-radius: 50%;
        }
    </style>

<%--    <link rel="stylesheet" href="dist/css/cartaoCredito.css">--%>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Painel Financeiro</h3>

                    </div>

                    <div class="box-body">
                        <table id="example2" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Descrição</th>
                                    <th>Valor</th>
                                    <th>Descontos</th>
                                    <th>Valor até Vencimento</th>
                                    <th>Vencimento</th>
                                    <th>Nosso número</th>
                                    <th>E-mail Enviado</th>
                                    <th>Todos
                                        <input type="checkbox" class="form-check-input text-center" id="exampleCheck1">
                                    </th>
                                    <th>Ações múltipla</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>curso supletivo ensino médio + taxas de inscrição 1ª e 2ª parcela crédito 12x</td>
                                    <td>138,00</td>
                                    <td>0,00</td>
                                    <td>10/02/2020</td>
                                    <td></td>
                                    <td>Não</td>
                                    <td>Aberto</td>
                                    <td>
                                        <input type="checkbox" class="form-check-input" id="exampleCheck1"></td>
                                    <td>
                                        <button class="btn btn-success" data-toggle="modal" data-target="#myModal">Receber via Cartão</button>
                                    </td>

                                </tr>
                                <tr>
                                    <td>apostila crédito 12x</td>
                                    <td>138,00</td>
                                    <td>0,00</td>
                                    <td>10/02/2020</td>
                                    <td></td>
                                    <td>Não</td>
                                    <td>Aberto</td>
                                    <td>
                                        <input type="checkbox" class="form-check-input" id="exampleCheck1"></td>
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
        </div>

    </section>


    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Informações de Pagamento</h3>

                    </div>

                    <div class="box-body">




                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
