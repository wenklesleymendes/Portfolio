<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CadastroFuncionario.aspx.cs" Inherits="EscolaPro.Web.CadastroFuncionario" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <script>

        function myFunction() {
            var x = document.getElementById("mySelect").value;
            debugger
            if (x == "1") { // CLT
                document.getElementById("dataInicio").innerHTML = "Data de Atestado Admissão";
                document.getElementById("dataFim").innerHTML = "Data de Atestado Demissão";
                document.getElementById("dataRecisao").innerHTML = "Data de Recisão";

                document.getElementById("dadosCarteira").style.visibility = "visible";
                document.getElementById("formRecisao").style.visibility = "hidden";
                document.getElementById("materias").style.visibility = "hidden";
                document.getElementById("salario").innerHTML = "Salário Bruto";

                document.getElementById("formTitulo").style.visibility = "visible";
                document.getElementById("cargaHoraria").style.visibility = "visible";

            }
            else if (x == "2") {
                debugger //Estagio
                document.getElementById("dataInicio").innerHTML = "Data de Início TCE";
                document.getElementById("dataFim").innerHTML = "Data Término TCE";
                document.getElementById("dataRecisao").innerHTML = "Data de Recisão";
                document.getElementById("formRecisao").style.visibility = "visible";
                document.getElementById("dadosCarteira").style.visibility = "hidden";
                document.getElementById("materias").style.visibility = "hidden";
                document.getElementById("salario").innerHTML = "Bolsa Auxílio";
                document.getElementById("formTitulo").style.visibility = "hidden";
                document.getElementById("cargaHoraria").style.visibility = "visible";
            } else if (x == "3" || x == "4") {
                debugger // Regime Professor Autonomo


                if (x == 3) {
                    document.getElementById("materias").style.visibility = "visible";
                    document.getElementById("salario").innerHTML = "Valor Aula";
                    document.getElementById("jornada1").style.visibility = "hidden";
                    document.getElementById("jornada2").style.visibility = "hidden";
                    document.getElementById("jornada3").style.visibility = "hidden";
                    document.getElementById("cargaHoraria").style.visibility = "hidden";

                } else {
                    document.getElementById("materias").style.visibility = "hidden";
                    document.getElementById("salario").innerHTML = "Valor Dia";
                }

                document.getElementById("dataInicio").innerHTML = "Data de Início de Contrato";
                document.getElementById("dataFim").innerHTML = "Data Término Contrato";
                document.getElementById("dataRecisao").innerHTML = "Data de Recisão";
                document.getElementById("formRecisao").style.visibility = "visible";
                document.getElementById("dadosCarteira").style.visibility = "hidden";
                document.getElementById("formTitulo").style.visibility = "hidden";

            } else if (x == "4") {
                debugger // Autonomo

            }

        }

    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Cadastro de Funcionário</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarFuncionario">Adicionar Novo Funcionário</a>
                <br />
                <br />

                <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Unidade</th>
                            <th>Nome do Colaborador</th>
                            <th>CPF</th>
                            <th>Regime</th>
                            <th>Data da Contratação</th>
                            <th>Documentos</th>
                            <th>Status</th>
                            <th style="width: 2%"></th>
                            <th style="width: 2%"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Campinas</td>
                            <td>Fernando Mendes</td>
                            <td>385.645.115.96</td>
                            <td>CLT</td>
                            <td>02/02/2020</td>
                            <td><span class="label label-warning">Documento pendente</span></td>
                            <td><span class="label label-success">Ativo</span></td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarPagamento"><i data-toggle="tooltip" class="fas fa-edit" style="color: #f39c12; font-size: large" title="Editar"></i></a>
                            </td>
                            <td>
                                <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                            </td>
                        </tr>
                        <tr>
                            <td>Campinas</td>
                            <td>João da Silva</td>
                            <td>554.654.123.89</td>
                            <td>Autônomo</td>
                            <td>02/10/2019</td>
                            <td><span class="label label-success">Cadastro OK</span></td>
                            <td><span class="label label-danger">Inativo</span></td>
                            <td>
                                <a data-toggle="modal" data-target="#modalCriarPagamento"><i data-toggle="tooltip" class="fas fa-edit" style="color: #f39c12; font-size: large" title="Editar"></i></a>
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



    <!-- Modal Cadastro de Funcionário -->


    <div class="container">
        <div class="modal fade" id="modalCriarFuncionario" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Cadastro de Funcionário</h4>
                    </div>


                    <div class="modal-body">
                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados da Pessoais</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <!-- Dados Pessoais -->
                                <div class="form-group">

                                    <%--<div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Unidade:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                        </select>
                                    </div>--%>

                                    <div class="btn-group" style="margin: 10px; width: 70%">
                                        <label for="cars">Nome Completo</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nome">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">RG</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="RG">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">CPF</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="CPF">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Data de Nascimento</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                </div>

                                <!-- Contatos -->
                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Telefone</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Telefone">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Celular</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Celular">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 48%">
                                        <label for="cars">Email</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Email">
                                    </div>

                                </div>


                                <!-- Endereço -->
                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">CEP</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="CEP">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 50%">
                                        <label for="cars">Endereço</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Endereço">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 18%">
                                        <label for="cars">Número</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número">
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 15%">
                                        <label for="cars">Complemento</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Complemento">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 35%">
                                        <label for="cars">Bairro</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Bairro">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Cidade</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Cidade">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 15%">
                                        <label for="cars">Estado</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Estado">
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados da Contratação</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <!-- Dados da Contratação -->
                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 28%">
                                        <label for="cars">Regime:</label>
                                        <br />
                                        <select id="mySelect" onchange="myFunction()" class="custom-select form-control">
                                            <option selected>Selecione o Regime</option>
                                            <option value="1">CLT</option>
                                            <option value="2">Estágio</option>
                                            <option value="3">Autônomo Professor</option>
                                            <option value="4">Autônomo</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars" id="dataInicio">Data de Atestado Admissão</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars" id="dataFim">Data de Atestado Demissão</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                    <div class="btn-group" id="formRecisao" style="margin: 10px; width: 20%">
                                        <label for="cars" id="dataRecisao">Data de Recisão</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                </div>


                                <div class="form-group">

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

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars" id="salario">Tempo de Almoço / Pausa</label>
                                        <br />
                                        <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required="">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Vale Transporte (Auxílio) por dia</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="R$">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Vale Alimentação por dia</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="R$">
                                    </div>
                                </div>

                                <div class="form-group" id="dadosCarteira">

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Número da Carteira</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Data da Emissão</label>
                                        <br />
                                        <input type="date" class="form-control">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Série</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Série">
                                    </div>

                                    <div class="btn-group" id="cargaHoraria" style="margin: 10px; width: 25%">
                                        <label for="cars">Carga Horário de Trabalho Semanal</label>
                                        <br />
                                         <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required="">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Número do PIS</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número">
                                    </div>

                                </div>

                                <div class="form-group" id="formTitulo">

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Número do Titulo</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Zona</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Zona">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Seção</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Seção">
                                    </div>

                                </div>

                            </div>
                        </div>

                        <!-- Salário / Unidade -->

                        <div class="box box-info" id="salarioUnidade">

                            <div class="box-header with-border">
                                <h3 class="box-title">Salário / Unidade</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">


                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 25%">
                                        <label for="cars">Unidade:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione a Unidade</option>
                                            <option value="1">Campinas</option>
                                            <option value="2">Jundiaí</option>
                                            <option value="3">São Paulo</option>
                                            <option value="4">Sorocaba</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Salário</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="R$">
                                    </div>


                                    <div class="btn-group" style="width: 2   0%">
                                        <br />
                                        <a class="btn btn-primary">Adicionar</a>
                                    </div>

                                </div>



                                <div class="btn-group" style="margin: 10px">
                                    <label for="cars">Dados da Unidade e Salários:</label>
                                    <br />

                                    <table id="example2" class="table table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>Unidade</th>
                                                <th style="width: 20%">Salário</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Jundiaí</td>
                                                <td>R$ 650,00</td>
                                                <td>
                                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Campinas</td>
                                                <td>R$ 780,00</td>
                                                <td>
                                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>

                            </div>
                        </div>




                        <!-- Agente de Integração -->

                        <div class="box box-info" id="agendeIntegracao">

                            <div class="box-header with-border">
                                <h3 class="box-title">Agente de Integração</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Nome</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Nome">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Telefone</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Telefone">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Email</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Email">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Site</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Site">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Pessoa Para Contato</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Contato">
                                    </div>

                                </div>

                            </div>
                        </div>

                        <!-- Cadastro de Jornada -->

                        <div class="box box-info" id="jornada1">

                            <div class="box-header with-border">
                                <h3 class="box-title">Jornada de Trabalho Padrão</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">
                                    <div class="btn-group" id="isento" style="margin: 10px; width: 20%">
                                        <label for="cars">Ativo:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Sim</option>
                                            <option value="2">Não</option>
                                        </select>
                                    </div>
                                </div>

                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Dia da Semana</th>
                                            <th style="width: 20px">Entrada</th>
                                            <th style="width: 20px">Saída</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Segunda-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Terça-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quarta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quinta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sexta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sabado</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Domingo</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>


                        <!-- Cadastro de Recesso -->

<%--                        <div class="box box-info" id="jornada2">

                            <div class="box-header with-border">
                                <h3 class="box-title">Jornada de Trabalho Recesso</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">

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

                                    <div class="btn-group" id="isento" style="margin: 10px; width: 20%">
                                        <label for="cars">Ativo:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Sim</option>
                                            <option value="2">Não</option>
                                        </select>
                                    </div>
                                </div>

                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Dia da Semana</th>
                                            <th style="width: 20px">Entrada</th>
                                            <th style="width: 20px">Saída</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Segunda-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Terça-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quarta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quinta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sexta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sabado</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Domingo</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>--%>

                        <!-- Cadastro de Flexivel -->

             <%--           <div class="box box-info" id="jornada3">

                            <div class="box-header with-border">
                                <h3 class="box-title">Jornada de Trabalho Flexível</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">

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

                                    <div class="btn-group" id="isento" style="margin: 10px; width: 20%">
                                        <label for="cars">Ativo:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Sim</option>
                                            <option value="2">Não</option>
                                        </select>
                                    </div>
                                </div>

                                <table class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Dia da Semana</th>
                                            <th style="width: 20px">Entrada</th>
                                            <th style="width: 20px">Saída</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Segunda-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Terça-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quarta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Quinta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sexta-Feira</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Sabado</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                        <tr>
                                            <td>Domingo</td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                            <td>
                                                <input type="time" id="appt" name="appt" class="form-control" min="09:00" max="18:00" required=""></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>--%>


                        <!-- Matérias -->

                        <div class="box box-info" id="materias" style="visibility: hidden">

                            <div class="box-header with-border">
                                <h3 class="box-title">Relação de Matérias</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse" aria-expanded="false"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Matéria</th>
                                            <th style="width: 20px">Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>L. Portuguesa</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Arte</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Matemática</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Biologia</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Química	História</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Geografia</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Filosofia</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sociologia</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Inglês</td>
                                            <td>
                                                <select class="custom-select form-control">
                                                    <option value="1">Inativo</option>
                                                    <option value="2">Ativo</option>
                                                </select>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>


                            </div>
                        </div>

                        <!-- Anexo de Documentos -->

                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Anexar Documentos</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">



                                <div class="form-group">


                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Tipo do Documento:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Outros</option>
                                            <option value="3">Termo de compromisso de estágio</option>
                                            <option value="3">Contrato de trabalho</option>
                                            <option value="4">Exame Médico</option>
                                            <option value="5">Comprovante de Endereço</option>
                                            <option value="6">Cópia do RG</option>
                                            <option value="7">Cópia do CPF</option>
                                            <option value="8">Certidão de Nascimento</option>
                                            <option value="9">Contrato de prestação de serviço</option>
                                        </select>
                                    </div>


                                    <div class="btn-group" style="margin: 10px">
                                    
                                        <label for="cars">Anexar Arquivo:</label>
                                        <br />


                                        <button type="button" class="btn btn-danger btn-flat"><span class="fas fa-paperclip" style="margin-right:10px"></span>Anexar Arquivo</button>

                                    </div>

                                    <div class="btn-group" style="margin: 10px;width:45%">
                                   
                                        <label for="cars"></label>
                                        <br />

                                        <button type="button" class="btn btn-primary pull-right">Adicionar</button>

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
                                                <td>Certidão de Nascimento</td>
                                                <td>18/03/2020</td>
                                                <td>
                                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Cópia do RG</td>
                                                <td>22/03/2020</td>
                                                <td>
                                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 5px; color: #dd4b39; font-size: large" title="Remover"></i>
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
    </div>




</asp:Content>
