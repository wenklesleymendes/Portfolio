<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FolhaPagamento.aspx.cs" Inherits="EscolaPro.Web.FolhaPagamento" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">


    <script>

        function myFunction() {
            var x = document.getElementById("mySelect").value;
            debugger
            if (x == "1") { // CLT
                document.getElementById("labelSalarioBruto").innerHTML = "Salário Bruto";
                document.getElementById("labelSalarioLiquido").innerHTML = "Salário Bruto";
                document.getElementById("formSalarioLiquido").style.visibility = "visible";

                document.getElementById("labelTransporte").innerHTML = "Transporte";

                document.getElementById("formAlimentacao").style.visibility = "visible";
                document.getElementById("formTransporte").style.visibility = "visible";
                document.getElementById("formDSR").style.visibility = "visible";
                document.getElementById("formFerias").style.visibility = "visible";
                document.getElementById("formDecimo").style.visibility = "visible";
            }
            else if (x == "2") {
                debugger //Estagio
                document.getElementById("labelSalarioBruto").innerHTML = "Bolsa Auxílio";
                document.getElementById("labelSalarioLiquido").innerHTML = "Bolsa Auxílio Proporcional";
               
                document.getElementById("labelTransporte").innerHTML = "Auxílio Transporte";
                document.getElementById("formDSR").style.visibility = "hidden";
                document.getElementById("formFerias").style.visibility = "visible";
                document.getElementById("formDecimo").style.visibility = "hidden";

            } else if (x == "3") {
                debugger // Regime Professor Autonomo 
                document.getElementById("labelSalarioBruto").innerHTML = "Valor Por Aula";
                document.getElementById("labelSalarioLiquido").innerHTML = "Quantidade de Aulas";
                document.getElementById("formSalarioLiquido").style.visibility = "visible";

                document.getElementById("formAlimentacao").style.visibility = "hidden";
                document.getElementById("formTransporte").style.visibility = "hidden";
                document.getElementById("formFerias").style.visibility = "hidden";

                document.getElementById("comissao").style.visibility = "hidden";
                document.getElementById("metaMes").style.visibility = "hidden";
                document.getElementById("metaSemestre").style.visibility = "hidden";

            } else if (x == "4") {
                debugger // Autonomo
                document.getElementById("labelSalarioBruto").innerHTML = "Valor Por Dia";
                document.getElementById("labelSalarioLiquido").innerHTML = "Quantidade de Dias";

                document.getElementById("formAlimentacao").style.visibility = "visible";
                document.getElementById("formTransporte").style.visibility = "visible";

                document.getElementById("comissao").style.visibility = "hidden";
                document.getElementById("metaMes").style.visibility = "hidden";
                document.getElementById("metaSemestre").style.visibility = "hidden";
                document.getElementById("formFerias").style.visibility = "hidden";
                document.getElementById("formDSR").style.visibility = "hidden";
                document.getElementById("formDecimo").style.visibility = "hidden";
                document.getElementById("formMonitoria").style.visibility = "hidden";
            }

        }

    </script>



</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Folha de Pagamento</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">
                <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Busca Avançada<span class="fas fa-sort-amount-down" style="margin-left: 10px"></span>
                </a>
                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarPagamento">Adicionar Pagamento</a>
                <br />
                <br />

                <div class="collapse" id="collapseExample">
                    <div class="card card-body">

                        <div class="form-group">

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars">CPF</label>
                                <br />
                                <input type="email" class="form-control" placeholder="CPF">
                            </div>

                            <div class="btn-group" style="margin: 10px; width: 50%">
                                <label for="cars">Nome</label>
                                <br />
                                <input type="email" class="form-control" placeholder="Nome">
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

                            <div class="btn-group" style="margin: 10px; width: 20%">
                                <label for="cars"></label>
                                <br />
                                <a class="btn btn-dropbox"><span class="fa fa-search" style="margin-right: 10px"></span>Buscar</a>
                            </div>
                        </div>


                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Calendário de Pagamentos</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">
                                    <iframe src="CalendarioDespesa.aspx" style="border: none;" width="100%" height="1000px" runat="server"></iframe>
                                    <!-- /.content-wrapper -->
                                </div>
                            </div>
                        </div>





                    </div>
                </div>



                <br />
                <br />


                <div class="form-group">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Unidade</th>
                                <th>Nome do Colaborador</th>
                                <th>Regime</th>
                                <th>Data do Pagamento</th>
                                <th>Valor do Pagamento</th>
                                <th>Status Pagamento</th>
                                <th style="width: 10%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Campinas</td>
                                <td>Fernando Mendes</td>
                                <td>CLT</td>
                                <td>12/04/2020</td>
                                <td>R$ 1.203,00</td>
                                <td><span class="label label-warning">Pendente</span></td>
                                <td>
                                    <a data-toggle="modal" data-target="#modalCriarPagamento"><i data-toggle="tooltip" class="fas fa-edit" style="color: #f39c12; font-size: large" title="Editar"></i></a>
                                    <a data-toggle="modal" data-target="#modalDetalhesPagamento"><i data-toggle="tooltip" class="fas fa-dollar-sign" style="color: green; font-size: large; margin-left: 10px;" title="Detalhes do Pagamento"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                    <a data-toggle="modal" data-target="#modalRecibo"><i data-toggle="tooltip" class="fas fa-print" style="margin-left: 10px; color: dodgerblue; font-size: large" title="Imprimir recibo"></i></a>
                                </td>
                            </tr>
                            <tr>
                                <td>Campinas</td>
                                <td>João da Silva</td>
                                <td>Autônomo</td>
                                <td>08/04/2020</td>
                                <td>R$ 650,34</td>
                                <td><span class="label label-success">Pago</span></td>
                                <td>
                                    <a data-toggle="modal" data-target="#modalCriarPagamento"><i data-toggle="tooltip" class="fas fa-edit" style="color: #f39c12; font-size: large" title="Editar"></i></a>
                                    <a data-toggle="modal" data-target="#modalDetalhesPagamento"><i data-toggle="tooltip" class="fas fa-dollar-sign" style="color: green; font-size: large; margin-left: 10px;" title="Detalhes do Pagamento"></i></a>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                    <a data-toggle="modal" data-target="#modalRecibo"><i data-toggle="tooltip" class="fas fa-print" style="margin-left: 10px; color: dodgerblue; font-size: large" title="Imprimir recibo"></i></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
    </div>






    <!-- Modal Adicionar Pagamento -->
    <div class="container">
        <div class="modal fade" id="modalCriarPagamento" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Folha de Pagamento</h4>
                    </div>

                    <div class="modal-body">


                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Dados do Colaborador</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <div class="form-group">

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">CPF</label>
                                            <br />

                                            <div class="autocomplete" style="width: 210px;">
                                                <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="000.000.000-00">
                                            </div>
                                            <a class="btn btn-dropbox pull-right"><span class="fas fa-search"></span></a>
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 40%">
                                            <label for="cars">Nome do Colaborador</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Nome">
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

                                    </div>

                                    <div class="form-group">

                                        <div class="btn-group" style="margin: 10px; width: 28%">
                                            <label for="cars">Regime:</label>
                                            <br />
                                            <select id="mySelect" onchange="myFunction()" class="custom-select form-control">
                                                <option value="1">CLT</option>
                                                <option value="2">Estágio</option>
                                                <option value="3">Autônomo Professor</option>
                                                <option value="4">Autônomo</option>
                                            </select>
                                        </div>


                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars">Banco</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Banco">
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars">Agência Bancária</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Agência">
                                        </div>

                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars">Conta Corrente</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="Conta Corrente">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Dados do Pagamento</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">
                                    <div class="form-group">
                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars" id="labelSalarioBruto">Salário Bruto</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div id="formSalarioLiquido" class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars" id="labelSalarioLiquido">Salário Líquido</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="">
                                        </div>

                                        <div class="btn-group" id="formAlimentacao" style="margin: 10px; width: 30%">
                                            <label for="cars">Alimentação</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="btn-group"  id="formTransporte" style="margin: 10px;">
                                            <label for="cars" id="labelTransporte">Transporte</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" id="comissao" style="margin: 10px;">
                                            <label for="cars">Comissão</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" id="metaMes" style="margin: 10px;">
                                            <label for="cars">Bônus Meta Mês</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" id="metaSemestre" style="margin: 10px;">
                                             <label for="cars">Bônus Meta Período</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" id="formMonitoria" style="margin: 10px;">
                                            <label for="cars">Monitoria de Prova</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>
                                    </div>

                                    <div class="form-group" style="background-color: #EDEBF0; border: 1px dashed">



                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars">Valor Adicional</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Adicional (Justificativa)</label>
                                            <br />
                                            <textarea style="width: 800px"></textarea>
                                        </div>
                                    </div>






                                </div>
                            </div>

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Horas Extras</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <div class="form-group">
                                        <div class="btn-group" style="margin: 10px; width: 20%">
                                            <label for="cars">Porcentagem:</label>
                                            <br />
                                            <select class="custom-select form-control">
                                                <option value="1">50%</option>
                                                <option value="2">75%</option>
                                                <option value="3">100%</option>
                                                <option value="3">150%</option>
                                                <option value="3">200%</option>
                                            </select>
                                        </div>

                                        <div class="btn-group" style="margin: 15px; width: 40%">
                                            <br />
                                            <div class="input-group" style="margin-left: 10px">
                                                <input id="new-event" type="text" class="form-control" placeholder="Quantidade de Horas">
                                                <div class="input-group-btn">
                                                    <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Adicionar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="btn-group" style="margin: 10px">
                                        <label for="cars">Horas extras trabalhadas:</label>
                                        <br />

                                        <table id="example2" class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Porcentagem</th>
                                                    <th style="width: 20%">Quantidade de Horas</th>
                                                    <th style="width: 5%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>50%</td>
                                                    <td>10 Horas</td>
                                                    <td>
                                                        <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>100%</td>
                                                    <td>12 Horas</td>
                                                    <td>
                                                        <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <div class="form-group" id="formDSR" style="background-color: #EDEBF0; border: 1px dashed;">
                                            <div class="btn-group" style="margin: 10px; width: 30%">
                                                <label for="cars">DSR (Descanso Semanal Remunerado)</label>
                                                <br />
                                                <input type="email" class="form-control" placeholder="Quantidade de Dias">
                                            </div>

                                            <div class="btn-group" style="margin: 10px;">
                                                <label for="cars">DSR (Justificativa)</label>
                                                <br />
                                                <textarea style="width: 800px"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Férias e Décimo Terceiro</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <div class="form-group" id="formFerias" style="background-color: #EDEBF0; border: 1px dashed;">
                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars">Férias</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Férias (Justificativa)</label>
                                            <br />
                                            <textarea style="width: 800px"></textarea>
                                        </div>
                                    </div>

                                    <div class="form-group" id="formDecimo" style="background-color: #EDEBF0; border: 1px dashed;">
                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars">Valor Décimo Terceiro</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Décimo Terceiro (Justificativa)</label>
                                            <br />
                                            <textarea style="width: 800px"></textarea>
                                        </div>
                                    </div>

                                </div>
                            </div>








                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Descontos</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <div class="form-group" style="background-color: #EDEBF0; border: 1px dashed;">
                                        <div class="btn-group" style="margin: 10px; width: 30%">
                                            <label for="cars">Valor Total de Descontos</label>
                                            <br />
                                            <input type="email" class="form-control" placeholder="R$">
                                        </div>

                                        <div class="btn-group" style="margin: 10px;">
                                            <label for="cars">Descontos (Justificativa)</label>
                                            <br />
                                            <textarea style="width: 800px"></textarea>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <br />

                        <div class="box-body">
                            <span class="label label-success pull-right" style="font-size: small">Valor a ser pago: <span class="label label-warning" style="font-size: medium">R$ 450,00</span> </span>

                        </div>

                        <div class="box-footer">
                            <a class="btn btn-success pull-right">Salvar</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>





    <!-- Modal Detalhes do Pagamento -->
    <div class="container">
        <div class="modal fade" id="modalDetalhesPagamento" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Detalhes dos Pagamentos</h4>
                    </div>


                    <div class="modal-body">

                        <div class="box-body">

                            <div class="form-group">
                                <div class="btn-group" style="margin: 10px; width: 8%">
                                    <label for="cars" style="color: dodgerblue">Regime</label>
                                    <br />
                                    <label for="cars">CLT</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 40%">
                                    <label for="cars" style="color: dodgerblue">Nome do Colaborador</label>
                                    <br />
                                    <label for="cars">João da Silva</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Salário Bruto</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Salário Líquido</label>
                                    <br />
                                    <label for="cars">R$ 110,00</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Alimentação</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Transporte</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Comissão</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Bônus Meta Mês</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Bônus Meta Semestre</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Horas Extras</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 30%">
                                    <label for="cars" style="color: dodgerblue">DSR (Descanso Semanal Remunerado)</label>
                                    <br />
                                    <label for="cars">4 dias</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Monitoria Prova</label>
                                    <br />
                                    <label for="cars">R$ 134,50</label>
                                </div>



                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Férias</label>
                                    <br />
                                    <label for="cars">R$ 250,00</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Décimo Terceiro</label>
                                    <br />
                                    <label for="cars">R$ 110,00</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Valor Total</label>
                                    <br />
                                    <label for="cars">R$ 650,34</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Data do Pagamento</label>
                                    <br />
                                    <label for="cars">08/04/2020</label>
                                </div>

                                <div class="btn-group" style="margin: 10px; width: 20%">
                                    <label for="cars" style="color: dodgerblue">Status Pagamento</label>
                                    <br />
                                    <label for="cars">Pago</label>
                                </div>

                            </div>

                        </div>


                        <div class="box-body">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#historico">Histórico de Pagamento</a></li>
                                <li><a data-toggle="tab" href="#comprovantes">Recibo de Pagamento</a></li>
                                <li><a data-toggle="tab" href="#transacao">Comprovante de Transação Bancária</a></li>
                            </ul>

                            <div class="tab-content">

                                <div id="historico" class="tab-pane fade in active">
                                    <div class="box box-info">
                                        <div class="box-body">

                                            <ul class="timeline">
                                                <!-- timeline time label -->

                                                <!-- /.timeline-label -->
                                                <!-- timeline item -->
                                                <li>

                                                    <i class="fa fa-check bg-green-active" aria-hidden="true"></i>

                                                    <div class="timeline-item">


                                                        <div class="el-timeline-item__wrapper">
                                                            <div class="el-timeline-item__timestamp is-top">
                                                                08/04/2020
                                                            </div>
                                                            <div class="el-timeline-item__content">
                                                                <h4 class="m-0" style="color: rgb(76, 208, 76);">Pagamento Efetuado.</h4>
                                                                <p class="m-0"><b>Valor pago</b>: R$ 650,34</p>
                                                                <p class="m-0">
                                                                    <span><b>Usuário</b>: SisPag Itaú</span><b> Data</b>: 08/04/2020 15:20:57
                                                                </p>
                                                            </div>
                                                            <!---->
                                                        </div>
                                                        <br />
                                                    </div>
                                                </li>
                                                <!-- END timeline item -->
                                                <!-- timeline item -->
                                                <li>
                                                    <i class="fa fa-money bg-yellow" aria-hidden="true"></i>

                                                    <div class="timeline-item">

                                                        <div class="el-timeline-item__wrapper">
                                                            <div class="el-timeline-item__timestamp is-top">
                                                                04/04/2020
                                                            </div>
                                                            <div class="el-timeline-item__content">
                                                                <h4 class="m-0" style="color: rgb(180, 180, 180);">Pagamento Cadastrado.</h4>
                                                                <p class="m-0"><b>Valor pago</b>: R$ 650,34</p>
                                                                <p class="m-0">
                                                                    <span><b>Usuário</b>: Daiane</span><b> Data</b>: 04/04/2020 14:43:57
                                                                </p>
                                                            </div>
                                                            <!---->
                                                        </div>
                                                        <br />
                                                    </div>
                                                </li>
                                                <!-- END timeline item -->
                                                <!-- timeline item -->
                                                <li></li>
                                            </ul>


                                        </div>
                                    </div>
                                </div>

                                <div id="comprovantes" class="tab-pane fade">
                                    <div class="box box-info">
                                        <div class="box-body">
                                            <br />
                                            <div class="btn-group" style="margin: 10px">
                                                <br />
                                                <label for="cars">Selecione o comprovante ou holerite assinado:</label>
                                                <br />

                                                <input type="file" class="btn btn-primary" style="width: 400px; margin-right: 40px">
                                            </div>
                                            <br />
                                            <br />
                                            <a class="btn btn-success pull-right">Salvar</a>
                                        </div>

                                    </div>
                                </div>

                                <div id="transacao" class="tab-pane fade">
                                    <div class="box box-info">
                                        <div class="box-body">
                                            <br />
                                            <div class="btn-group" style="margin: 10px">
                                                <br />
                                                <label for="cars">Anexo comprovante da transação bancária:</label>
                                                <br />

                                                <a class="btn btn-dropbox">
                                                    <span class="fas fa-file-download" style="margin-right: 10px; font-size: large"></span>
                                                    Download do Arquivo</a>
                                            </div>
                                            <br />
                                            <br />
                                            <a class="btn btn-success pull-right">Salvar</a>
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
