<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HistoricoPagamento.aspx.cs" Inherits="EscolaPro.Web.HistoricoPagamento" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style>
        /*the container must be positioned relative:*/
        .autocomplete {
            position: relative;
            display: inline-block;
        }

        .autocomplete-items {
            position: absolute;
            border: 1px solid #d4d4d4;
            border-bottom: none;
            border-top: none;
            z-index: 99;
            /*position the autocomplete items to be the same width as the container:*/
            top: 100%;
            left: 0;
            right: 0;
        }

            .autocomplete-items div {
                padding: 10px;
                cursor: pointer;
                background-color: #fff;
                border-bottom: 1px solid #d4d4d4;
            }

                /*when hovering an item:*/
                .autocomplete-items div:hover {
                    background-color: #e9e9e9;
                }

        /*when navigating through the items using the arrow keys:*/
        .autocomplete-active {
            background-color: DodgerBlue !important;
            color: #ffffff;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Histórico de Pagamentos</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">
                <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Busca Avançada<span class="fas fa-sort-amount-down" style="margin-left: 10px"></span>
                </a>
                <br />
                <br />

                <div class="collapse" id="collapseExample">
                    <div class="card card-body">

                        <div class="form-group">

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Tipo Fornecedor / Cliente:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option selected>Selecione Fornecedor</option>
                                    <option value="1">Mad Tech</option>
                                    <option value="2">Kaspper</option>
                                    <option value="3">Limpeza Ltda</option>
                                </select>
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Unidade:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option value="1">Campinas</option>
                                    <option value="2">Jundiaí</option>
                                    <option value="3">São Paulo</option>
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
                        </div>
                        <div class="form-group">

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">Forma de Pagamento:</label>
                                <br />
                                <select class="custom-select form-control">
                                    <option selected>Forma de Pagamento</option>
                                    <option value="1">Cartão de Crédito</option>
                                    <option value="2">Cartão de Débito</option>
                                    <option value="3">Boleto Bancário</option>
                                </select>
                            </div>


                            <div class="btn-group" style="margin: 10px;">
                                <label for="cars">Categoria</label>
                                <br />

                                <div class="autocomplete" style="width: 250px;">
                                    <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="Categoria">
                                </div>

                            </div>

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">CPF ou CNPJ</label>
                                <br />
                                <input type="email" class="form-control" placeholder="CPF/CPNPJ">
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars"></label>
                                <br />
                                <a class="btn btn-dropbox"><span class="fa fa-search" style="margin-right: 10px"></span>Buscar</a>
                            </div>
                        </div>
                    </div>

                    <div class="box-body">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Calendário de Despesas</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <br />



                <br />
                <div class="form-group">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Unidade</th>
                                <th>Nome da Despesa</th>
                                <th>Categoria</th>
                                <th>Forma de Pagamentos</th>
                                <th>Fornecedor</th>
                                <th>Status Pago</th>
                                <th>Data Pagamento</th>
                                <th>Valor da Pago</th>
                                <th style="width: 8%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Campinas</td>
                                <td>Material de Limpeza</td>
                                <td>Limpeza</td>
                                <td>Cartão de Crédito</td>
                                <td>Limpeza e Serviços Ltda</td>
                                <td><span class="label label-success">Pago</span></td>
                                <td>12/04/2020</td>
                                <td>R$ 150,00</td>
                                <td>
                                    <i data-toggle="tooltip" class="fas fa-edit" style="color: #f39c12; font-size: large" title="Editar"></i>
                                    <%--<a data-toggle="modal" data-target="#modalLiquidarDespesa"><i data-toggle="tooltip" class="fas fa-dollar-sign" style="color: green; font-size: large; margin-left: 10px;" title="Liquidar Manual"></i></a>--%>
                                    <%--<i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>--%>
                                    <a data-toggle="modal" data-target="#modalRecibo"><i data-toggle="tooltip" class="fas fa-print" style="margin-left: 10px; color: dodgerblue; font-size: large" title="Comprovante Itáu"></i></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                  
                    <div class="pull-right">

           
                    <div class="dataTables_paginate paging_simple_numbers" id="example1_paginate">
                        <ul class="pagination">
                            <li class="paginate_button previous disabled" id="example1_previous">
                                <a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0">Voltar</a>

                            </li>
                            <li class="paginate_button active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="2" tabindex="0">2</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="3" tabindex="0">3</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="4" tabindex="0">4</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="5" tabindex="0">5</a></li>
                            <li class="paginate_button "><a href="#" aria-controls="example1" data-dt-idx="6" tabindex="0">6</a></li>
                            <li class="paginate_button next" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0">Próximo</a></li>

                        </ul>

                    </div>
                                 </div>


                    <%--<span class="label label-success pull-right" style="font-size: small">Valor Total de Despesas: <span class="label label-warning" style="font-size: medium">R$ 450,00</span> </span>--%>
                </div>



            </div>
        </div>
    </div>


    <!-- Modal Adicionar Despesa -->
    <div class="container">
        <div class="modal fade" id="modalCriarDespesa" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Nova Despesa</h4>
                    </div>


                    <div class="modal-body">
                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados do pagamento</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Nome da Despesa</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Despesa">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Unidade:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Categoria</label>
                                        <br />

                                        <div class="autocomplete" style="width: 210px;">
                                            <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="Categoria">
                                        </div>
                                        <a class="btn btn-dropbox pull-right"><span class="fas fa-plus"></span></a>
                                    </div>

                                </div>

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Forma de Pagamento:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Forma de Pagamento</option>
                                            <option value="1">Cartão de Crédito</option>
                                            <option value="2">Cartão de Débito</option>
                                            <option value="3">Boleto Bancário</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 62%">
                                        <label for="cars">Código de Barras</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Código de Barras">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 23%">
                                        <label for="cars">Vencimento</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 25%">
                                        <label for="cars">Valor</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="R$">
                                    </div>

                                </div>

                                <div class="form-group">


                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Tipo de Pessoa:</label>
                                        <br />
                                        <select id="mySelect" onchange="myFunction()" class="custom-select form-control">
                                            <option value="1">Pessoa Jurídica</option>
                                            <option value="2">Pessoa Física</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px;width: 35%"">
                                        <label for="cars">Fornecedor:</label>
                                        <br />

                                        <div class="autocomplete" style="width: 300px;">
                                            <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="Fornecedor">
                                        </div>
                                        
                                    </div>

                                  <%--  <div class="btn-group" style="margin: 10px; width: 35%">
                                        <label for="cars">Fornecedor:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione Fornecedor</option>
                                            <option value="1">Mad Tech</option>
                                            <option value="2">Kaspper</option>
                                            <option value="3">Limpeza Ltda</option>
                                        </select>
                                    </div>--%>

                                    <div class="btn-group" style="margin: 10px; width: 23%">
                                        <label for="cars">Data de Emissão</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                </div>


                            </div>
                        </div>




                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Detalhes da Conta</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Nº do Documento</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nº do Documento">
                                    </div>

                                    <%--                                   <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars">Centro de Custos</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione Centro de Custos</option>
                                            <option value="1">Mad Tech</option>
                                            <option value="2">Kaspper</option>
                                            <option value="3">Limpeza Ltda</option>
                                        </select>
                                    </div>--

                                </div>

                                <div class="form-group">--%>

                                    <div class="btn-group" style="margin: 10px; width: 18%">
                                        <label for="cars">Parcelas</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Única</option>
                                            <option value="2">Parcelada</option>
                                            <option value="3">Despesas Recorrentes</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Quantidade de Parcelas</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Quantidade">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <br />
                                        <a class="btn btn-dropbox" style="width: 100px">Gerar</a>
                                    </div>

                                </div>


                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Observação</label>
                                        <br />
                                        <textarea style="width: 830px"></textarea>
                                    </div>
                                </div>


                                <div class="box-body">
                                    <span class="label label-warning">Anexar todos os boletos e notas fiscais da despesa</span>
                                    <br />
                                    <div class="btn-group" style="margin: 10px">
                                        <br />
                                        <label for="cars">Anexar Arquivo:</label>
                                        <br />

                                        <input type="file" class="btn btn-danger" style="width: 400px; margin-right: 40px">
                                        <div class="input-group" style="margin-left: 10px">
                                            <input id="new-event" type="text" class="form-control" placeholder="Nome do Arquivo">
                                            <div class="input-group-btn">
                                                <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Adicionar</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="btn-group" style="margin: 10px">
                                        <label for="cars">Lista de Documentos:</label>
                                        <br />

                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Descrição</th>
                                                    <th style="width: 20%">Data do Anexo</th>
                                                    <th style="width: 5%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Contrato Simples Nascional</td>
                                                    <td>18/03/2020</td>
                                                    <td>
                                                        <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                </div>


                            </div>
                        </div>


                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Parcelas</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">


                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Data</th>
                                            <th>Valor</th>
                                            <th>Forma de Pagamento</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>12/04/2020</td>
                                            <td>R$ 150,00</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Cartão de Crédito</option>
                                                    <option value="3">Cartão de Débito</option>
                                                    <option value="4">Boleto Bancário</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>12/05/2020</td>
                                            <td>R$ 150,00</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Cartão de Crédito</option>
                                                    <option value="3">Cartão de Débito</option>
                                                    <option value="4">Boleto Bancário</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>12/06/2020</td>
                                            <td>R$ 150,00</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Cartão de Crédito</option>
                                                    <option value="3">Cartão de Débito</option>
                                                    <option value="4">Boleto Bancário</option>
                                                </select>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>

                    <div class="box-footer">
                        <a class="btn btn-success pull-right">Salvar</a>
                    </div>


                </div>
            </div>
        </div>
    </div>



    <!-- Modal Adicionar Despesa -->
    <div class="container">
        <div class="modal fade" id="modalLiquidarDespesa" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Detalhes - Contas a pagar</h4>
                    </div>


                    <div class="modal-body">

                        <div class="box-body">

                            <div class="form-group">
                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Nome da Despesa</label>
                                    <br />
                                    <label for="cars">Compra de Celular</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Fornecedor</label>
                                    <br />
                                    <label for="cars">Vivo Celulares</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Status</label>
                                    <br />
                                    <label for="cars">Aberto - Conta Parcelada</label>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Vencimento</label>
                                    <br />
                                    <label for="cars">09/11/2020</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Valor da Despesa</label>
                                    <br />
                                    <label for="cars">R$ 250,00</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">Valor Aberto</label>
                                    <br />
                                    <label for="cars">R$ 250,00</label>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="btn-group" style="margin: 10px; width: 70%">
                                    <label for="cars" style="color: dodgerblue">Observação</label>
                                    <br />
                                    <label for="cars">Foi comprado o celular para a equipe do suporte.</label>
                                </div>
                            </div>

                        </div>


                        <div class="box-body">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#baixaManual">Baixa manual</a></li>
                                <li><a data-toggle="tab" href="#historico">Histórico</a></li>
                                <li><a data-toggle="tab" href="#comprovantes">Comprovantes</a></li>
                            </ul>

                            <div class="tab-content">
                                <div id="baixaManual" class="tab-pane fade in active">

                                    <div class="box box-info">
                                        <div class="box-body">

                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Data Pagamento</label>
                                                    <br />
                                                    <input type="date" class="form-control">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Unidade</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Selecione uma Unidade</option>
                                                        <option value="1">Campinas</option>
                                                        <option value="2">Jundiaí</option>
                                                        <option value="3">São Paulo</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 32%">
                                                    <label for="cars">Forma de Pagamento:</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option selected>Forma de Pagamento</option>
                                                        <option value="1">Cartão de Crédito</option>
                                                        <option value="2">Cartão de Débito</option>
                                                        <option value="3">Boleto Bancário</option>
                                                    </select>
                                                </div>

                                            </div>

                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Valor</label>
                                                    <br />
                                                    <input type="email" class="form-control" placeholder="R$">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Juros</label>
                                                    <br />
                                                    <input type="email" class="form-control" placeholder="0,00">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Desconto / Taxa</label>
                                                    <br />
                                                    <input type="email" class="form-control" placeholder="R$">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Acréscimo</label>
                                                    <br />
                                                    <input type="email" class="form-control" placeholder="R$">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="btn-group" style="margin: 10px; width: 80%">
                                                    <label for="cars">Oservações de pagamento</label>
                                                    <br />
                                                    <textarea style="width: 810px"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Baixar conta</label>
                                                    <br />
                                                    <select class="custom-select form-control">
                                                        <option value="1">Sim</option>
                                                        <option value="2">Não</option>
                                                    </select>
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 50%">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 10%">
                                                    <label for="cars" style="color: dodgerblue">Valor a pagar</label>
                                                    <br />
                                                    <label for="cars">R$ 250,00</label>
                                                </div>

                                            </div>
                                            <br />
                                            <br />

                                            <a class="btn btn-primary center-block" style="font-size: large; width: 400px">Liquidar conta</a>

                                        </div>
                                    </div>

                                </div>

                                <div id="historico" class="tab-pane fade">
                                    <div class="box box-info">
                                        <div class="box-body">

                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Data</th>
                                                        <th>Parcela</th>
                                                        <th>Forma de Pagamento</th>
                                                        <th>Valor</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>12/04/2020</td>
                                                        <td><span class="label label-success">2/4</span></td>
                                                        <td>Cartão de Crédito</td>
                                                        <td>R$ 150,00</td>
                                                    </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div id="comprovantes" class="tab-pane fade">
                                    <div class="box box-info">
                                        <div class="box-body">
                                            <br />
                                            <div class="btn-group" style="margin: 10px">
                                                <br />
                                                <label for="cars">Selecione o comprovante:</label>
                                                <br />

                                                <input type="file" class="btn btn-primary" style="width: 400px; margin-right: 40px">
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


    <!-- Modal Adicionar Despesa -->
    <div class="container">
        <div class="modal fade" id="modalRecibo" role="dialog">
            <div class="modal-dialog modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Imprimir recibo</h4>
                    </div>

                    <div class="modal-body">
                        <div class="box box-info">
                            <div class="form-group">

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Data</label>
                                    <br />
                                    <input type="date" class="form-control">
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 45%">
                                    <label for="cars">Valor</label>
                                    <br />
                                    <input type="email" class="form-control" placeholder="R$">
                                </div>
                            </div>

                            <div class="form-group">

                                <div class="btn-group" style="margin: 10px;">
                                    <label for="cars">Correspondente a</label>
                                    <br />
                                    <textarea style="width: 550px"></textarea>
                                </div>

                            </div>

                        </div>
                        <a class="btn btn-primary center-block" style="font-size: large; width: 200px">Imprimir</a>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
