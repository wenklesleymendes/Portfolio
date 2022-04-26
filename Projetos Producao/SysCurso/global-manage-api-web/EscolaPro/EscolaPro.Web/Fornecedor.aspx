<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Fornecedor.aspx.cs" Inherits="EscolaPro.Web.Fornecedor" %>


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

    <script>

        function myFunction() {
            var x = document.getElementById("mySelect").value;
            debugger
            if (x == "1") {
                debugger //Fisica
                //document.getElementById("fisica").style.visibility = "hidden";
                //document.getElementById("juridica").style.visibility = "visible";
                document.getElementById("labelRazaoSocial").innerHTML = "Razão Social";
                document.getElementById("inputRazaoSocial").innerHTML = "Razão Social";

                document.getElementById("labelCNPJ").innerHTML = "CNPJ";
                document.getElementById("inputCNPJ").innerHTML = "CNPJ";

                document.getElementById("labelDataNascimento").innerHTML = "Nome Fantasia";
                document.getElementById("inputDataNascimento").style.width = "400px";
                document.getElementById("inputDataNascimento").className = "form-control";

                document.getElementById("formInscricaoMunicipal").style.visibility = "visible";
                document.getElementById("formInscricaoEstatual").style.visibility = "visible";
                document.getElementById("isento").style.visibility = "visible";

            } else {
                debugger // Juridica
                document.getElementById("labelRazaoSocial").innerHTML = "Nome";
                document.getElementById("inputRazaoSocial").innerHTML = "Nome";

                document.getElementById("labelCNPJ").innerHTML = "CPF";
                document.getElementById("inputCNPJ").innerHTML = "CPF";

                document.getElementById("labelDataNascimento").innerHTML = "Data de Nascimento";
                document.getElementById("inputDataNascimento").style.width = "150px";
                document.getElementById("inputDataNascimento").className = "form-control";


                document.getElementById("formInscricaoMunicipal").style.visibility = "hidden";
                document.getElementById("formInscricaoEstatual").style.visibility = "hidden";
                document.getElementById("isento").style.visibility = "hidden";

            }
            //document.getElementById("demo").innerHTML = "You selected: " + x;
        }

    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Cadastro de Fornecedores</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalCriarFornecedor">Adicionar</a>
                <br />
                <br />

                <div class="form-group">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Nome / Razão Social</th>
                                <th>Tipo de Pessoa</th>
                                <th>Categoria / Classificação</th>
                                <th>CNPJ / CPF</th>
                                <th>Email</th>
                                <th>Telefone</th>
                                <th>Ramal</th>
                                <th>Situação Cadastro</th>
                                <th style="width: 6%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Mad Tech</td>
                                <td>Jurídica</td>
                                <td>Tecnologia</td>
                                <td>34.772.301/0001-55</td>
                                <td>tech@gmail.com</td>
                                <td>4522-0123</td>
                                <td>109</td>
                                <td><span class="label label-success">Ativo</span></td>
                                <td>
                                    <i data-toggle="tooltip" class="fas fa-edit" style="color: green; font-size: large" title="Editar"></i>
                                    <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 15px; color: #dd4b39; font-size: large" title="Remover"></i>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </div>


    <!-- Modal de Cadastro -->
    <div class="container">
        <div class="modal fade" id="modalCriarFornecedor" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Adicionar Novo Fornecedor ou Cliente</h4>
                    </div>


                    <div class="modal-body">
                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados do Fornecedor ou Cliente</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

                                <div class="form-group">
                                    <%--<div class="btn-group" style="margin: 10px; width: 25%">
                                        <label for="cars">Tipo de cadastro:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Selecione Tipo de cadastro</option>
                                            <option value="1">Fornecedor</option>
                                            <option value="3">Cliente</option>
                                            <option value="4">Ambos</option>
                                        </select>
                                    </div>--%>

                                    <div class="btn-group" style="margin: 10px; width: 35%">
                                        <label for="cars">Tipo de Pessoa:</label>
                                        <br />
                                        <select id="mySelect" onchange="myFunction()" class="custom-select form-control">
                                            <option value="1">Pessoa Jurídica</option>
                                            <option value="2">Pessoa Física</option>
                                        </select>
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Categoria / Classificação:</label>
                                        <br />

                                        <div class="autocomplete" style="width: 300px;">
                                            <input class="form-control" id="myInput" type="text" name="myCountry" placeholder="Categoria / Classificação">
                                        </div>
                                        <a class="btn btn-dropbox pull-right"><span class="fas fa-plus"></span></a>
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Situação cadastro:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Ativo</option>
                                            <option value="2">Inativo</option>
                                        </select>
                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 45%">
                                        <label for="cars" id="labelRazaoSocial">Razão Social</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputRazaoSocial" placeholder="Razão Social">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars" id="labelDataNascimento">Nome Fantasia</label>
                                        <br />
                                        <input type="email" id="inputDataNascimento" class="form-control" placeholder="Nome Fantasia" style="width: 400px">
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 35%">
                                        <label for="cars" id="labelCNPJ">CNPJ</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputCNPJ" placeholder="CNPJ">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;" id="formInscricaoMunicipal">
                                        <label for="cars" id="labelInscricaoMunicipal">Inscrição Municipal</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputInscricaoMunicipal" placeholder="Inscrição Municipal">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;" id="formInscricaoEstatual">
                                        <label for="cars" id="labelInscricaoEstatual">Inscrição Municipal</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputInscricaoEstatual" placeholder="Inscrição Estatual">
                                    </div>

                                    <div class="btn-group" id="isento" style="margin: 10px;">
                                        <label for="cars">Isento:</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option value="1">Sim</option>
                                            <option value="2">Não</option>
                                        </select>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 25%">
                                        <label for="cars">Celular</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Celular">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Telefone</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Telefone">
                                    </div>


                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Ramal</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Ramal">
                                    </div>


                                    <div class="btn-group" style="margin: 10px; width: 22%">
                                        <label for="cars">Fax</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Fax">
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px; width: 48%">
                                        <label for="cars">Email</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Email">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 44%">
                                        <label for="cars">Site</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Site">
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Observações</label>
                                        <br />
                                        <textarea class="form-control" style="width: 800px; height: 100px"></textarea>
                                    </div>
                                </div>

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
                                    <div class="btn-group" style="margin: 10px; width: 30%">
                                        <label for="cars">Complemento</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Complemento">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 40%">
                                        <label for="cars">Bairro</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Bairro">
                                    </div>

                                    <div class="btn-group" style="margin: 10px; width: 20%">
                                        <label for="cars">Cidade</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Cidade">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Estado</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Estado">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">País</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="País">
                                    </div>

                                </div>

                            </div>
                        </div>


                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Dados da Conta Corrente</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">

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


                        <div class="box box-info">

                            <div class="box-header with-border">
                                <h3 class="box-title">Anexar Documentos</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>

                            <div class="box-body">


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

                        <div class="box-footer">
                            <a class="btn btn-success pull-right">Salvar</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
