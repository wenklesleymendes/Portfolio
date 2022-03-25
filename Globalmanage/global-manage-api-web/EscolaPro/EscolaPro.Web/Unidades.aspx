<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Unidades.aspx.cs" Inherits="EscolaPro.Web.Unidades" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Gerenciador da Unidades</h3>

            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

            </div>
        </div>


        <div class="box-body">

            <a class="btn btn-dropbox pull-right" data-toggle="modal" data-target="#modalUnidade">Adicionar</a>

            <br />
            <br />
            <table id="example2" class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Unidade</th>
                        <th>CNPJ</th>
                        <th>Razão Social</th>
                        <th>Nome Fantasia</th>
                        <th>Telefone</th>
                        <th>Vigência AVCB</th>
                        <th>Vigência de Alvará</th>
                        <th style="width: 10%"></th>


                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Campinas</td>
                        <td>79.975.896/0001-84</td>
                        <td>Escola de Ensino Ltda.</td>
                        <td>Escola Pro</td>
                        <td>1145658554</td>
                        <td><span class="label label-danger">Vencido</span></td>
                        <td><span class="label label-success">Em dia</span></td>
                        <td>
                            <a data-toggle="modal" data-target="#modalCentroCusto"><i data-toggle="tooltip" class="fas fa-comments-dollar" style="color: darkgoldenrod; font-size: large" title="Centro de Custo"></i></a>
                            <i data-toggle="tooltip" class="fas fa-edit" style="color: green; margin-left: 10px; font-size: large" title="Editar"></i>
                            <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                        </td>
                        <%--<td><a class="btn btn-success" data-toggle="modal" data-target="#modalCurso"><i class="fas fa-edit" style="margin-right: 5px"></i>Editar</a></td>
                        <td><a class="btn btn-danger" data-toggle="modal" data-target="#modalCurso"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                        --%>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>


    <div class="container">
        <div class="modal fade" id="modalUnidade" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Nova Unidade</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Informações da Unidade</h3>

                                    <div class="box-tools pull-right">
                                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                    </div>
                                </div>

                                <div class="box-body">

                                    <form class="form-horizontal">

                                        <div class="box-body">
                                            <div class="form-group">

                                                <div class="btn-group" style="margin: 10px; width: 40%">
                                                    <label for="cars">Unidade:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Nome da Unidade">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">CEP:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="CEP">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 40%">
                                                    <label for="cars">Endereço:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Endereço">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 10%">
                                                    <label for="cars">Numero:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Numero">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Complemento:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Complemento">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Bairro:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Bairro">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Cidade:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Cidade">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Estado:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Estado">
                                                </div>



                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Telefone Fixo Principal:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Telefone Fixo 2:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo 2">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Telefone Fixo 3:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo 3">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Telefone Fixo 4:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo 4">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Telefone Fixo 5:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo 5">
                                                </div>


                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">Celular:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Celular">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars">WhatsApp:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="WhatsApp">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 40%">
                                                    <label for="cars">E-mail:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="E-mail">
                                                </div>


                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">WebSite:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="URL WebSite">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Instagram:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Instagram">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 30%">
                                                    <label for="cars">Facebook:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Facebook">
                                                </div>

                                                <%--Nome da Unidade, Endereço, Numero, Complemento, Bairro, Cidade, Estado, CEP
                                                telefone fixo principal, telefone fixo 2, telefone fixo 3... até telefone fixo 5.
                                                celular, WhatsApp, email, website, instagram, facebook,--%>



                                                <div class="btn-group" style="margin: 10px; width: 40%">
                                                    <label for="cars">CNPJ:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="CNPJ">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 50%">
                                                    <label for="cars">Razão Social:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Razão Social">
                                                </div>


                                                <form class="form-horizontal">

                                                    <div class="box-body">
                                                        <div class="box box-danger">
                                                            <div class="box-body">


                                                                <label>Horário de funcionamento sem aulas:</label>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Segunda a Sexta:</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Sábado:</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="box box-success">
                                                            <div class="box-body">
                                                                <label>Horário de funcionamento com aulas:</label>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Segunda a Sexta:</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label for="inputEmail3" class="col-sm-5 control-label">Sábado:</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                    <label for="inputEmail3" class="col-sm-1 control-label">Até</label>

                                                                    <div class="col-sm-2">
                                                                        <input type="time" id="appt" name="appt" class="form-control"
                                                                            min="09:00" max="18:00" required>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>

                                                </form>
                                            </div>


                                            <div class="input-group" style="margin: 10px">

                                                <label>Vigência AVCB:</label>

                                                <div class="input-group">

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>


                                                    <input id="date" type="date">

                                                    <span style="margin-left: 10px; margin-right: 10px">a</span>

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                                    <input id="date" type="date">
                                                </div>
                                            </div>

                                            <br />
                                            <br />

                                            <div class="input-group" style="margin: 10px">

                                                <label>Vigência de Alvará:</label>

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
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Dados Bancário da Unidade</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>



                            <div class="box-body">

                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Banco</label>
                                        <br />
                                        <select class="custom-select form-control">
                                            <option selected>Banco</option>
                                            <option value="1">Itáu</option>
                                            <option value="2">Banco do Brasil</option>
                                            <option value="3">Bradesco</option>
                                            <option value="4">Santander</option>
                                        </select>


                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Agência Bancária</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número da Agência">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars">Conta Bancária</label>
                                        <br />
                                        <input type="email" class="form-control" placeholder="Número da Conta">
                                    </div>

                                </div>


                            </div>
                        </div>
                    </div>


                    <div class="box-body">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Contrato de Locação</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>



                            <div class="box-body">

                                <form class="form-horizontal">

                                    <div class="box-body">
                                        <div class="form-group">

                                            <div class="btn-group" style="margin: 10px; width: 60%">
                                                <label for="cars">Nome do Proprietário:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Nome">
                                            </div>

                                            <div class="btn-group" style="margin: 10px;">
                                                <label for="cars">Telefone Proprietário:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo ou Celular">
                                            </div>

                                            <div class="btn-group" style="margin: 10px; width: 40%">
                                                <label for="cars">Imobiliária - Administrador do Imóvel:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Imobiliária - Administrador do Imóvel">
                                            </div>

                                            <div class="btn-group" style="margin: 10px;">
                                                <label for="cars">Telefone:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Telefone Fixo">
                                            </div>

                                            <div class="btn-group" style="margin: 10px;">
                                                <label for="cars">Celular:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Celular">
                                            </div>

                                            <div class="btn-group" style="margin: 10px; width: 40%">
                                                <label for="cars">Email:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="Email">
                                            </div>


                                            <div class="btn-group" style="margin: 10px">
                                                <label for="cars" style="margin-left: 10px">Período de Vigência:</label>

                                                <div class="input-group">

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                                    <input id="date" type="date">

                                                    <span style="margin-left: 10px; margin-right: 10px">a</span>

                                                    <i class="fa fa-calendar" style="margin: 10px"></i>

                                                    <input id="date" type="date">
                                                </div>
                                            </div>

                                            <div class="btn-group" style="margin: 10px; width: 30%">
                                                <label for="cars">Aluguel Valor Mensal:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                            </div>

                                            <div class="btn-group" style="margin: 10px; width: 30%">
                                                <label for="cars">Condomínio Valor Mensal:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                            </div>

                                            <div class="btn-group" style="margin: 10px; width: 30%">
                                                <label for="cars">IPTU Valor Mensal:</label>
                                                <br />
                                                <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                            </div>

                                            <div class="form-group" style="margin: 10px; width: 100%">

                                                <div class="btn-group" style="margin: 10px; width: 60%">
                                                    <label for="cars">Adicionar Despesas:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="Descrição">
                                                </div>

                                                <div class="btn-group" style="margin: 10px; width: 20%">
                                                    <label for="cars">Valor:</label>
                                                    <br />
                                                    <input type="email" class="form-control" id="inputEmail3" placeholder="R$">
                                                </div>

                                                <div class="btn-group" style="margin: 10px;">
                                                    <label for="cars"></label>
                                                    <br />
                                                    <a class="btn btn-dropbox">Adicionar</a>
                                                </div>
                                            </div>

                                            <table id="example2" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Despesa</th>
                                                        <th>Valor</th>
                                                        <th style="width: 5%"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Despesa 1</td>
                                                        <td>R$ 200,00</td>
                                                        <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Despesa 2</td>
                                                        <td>R$ 100,00</td>
                                                        <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                                    </tr>
                                                </tbody>
                                            </table>




                                        </div>


                                    </div>

                                </form>

                            </div>
                        </div>
                    </div>


                    <div class="box-body">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Documentos da Unidade</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>


                            <div class="box-body">

                                <div class="btn-group" style="margin: 10px">

                                    <span class="label label-warning">Documentos Mínimos Necessários (Contrato Social, CNPJ, Procuração Pública, Contrato de Locação, Alvará de Funcionamento, AVCB - Bombeiros, Espelho de IPTU).</span>
                                    <br />
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
                                                <td>Contrato Social</td>
                                                <td>18/03/2020</td>
                                                <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                            </tr>
                                            <tr>
                                                <td>Aditivos</td>
                                                <td>10/02/2020</td>
                                                <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>

                            </div>
                        </div>
                    </div>



                    <div class="box-body">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Histórico de Ocorrências</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                </div>
                            </div>


                            <div class="box-body">


                                <div class="form-group">

                                    <div class="btn-group" style="margin: 10px; width: 60%">
                                        <label for="cars">Registro de Ocorrências:</label>
                                        <br />
                                        <input type="email" class="form-control" id="inputEmail3" placeholder="Registro de Ocorrências">
                                    </div>

                                    <div class="btn-group" style="margin: 10px;">
                                        <label for="cars"></label>
                                        <br />
                                        <a class="btn btn-dropbox">Adicionar</a>
                                    </div>
                                </div>


                                <table id="example2" class="table table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Descrição da Ocorrências</th>
                                            <th style="width: 5%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Negociação de Aluguel 1</td>
                                            <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                        </tr>
                                        <tr>
                                            <td>Negociaçaõ de Alguel 2</td>
                                            <td><a class="btn btn-danger"><i class="fas fa-trash" style="margin-right: 5px"></i>Remover</a></td>
                                        </tr>
                                    </tbody>
                                </table>
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

    <!-- Modal Centro de Custo -->

    <div class="container">
        <div class="modal fade" id="modalCentroCusto" role="dialog">
            <div class="modal-dialog modal-lg">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Centro de Custo</h4>
                    </div>
                    <div class="modal-body">

                        <div class="box-body">
                            <div class="box box-info">

                                <div class="box-body">
                                    <label for="cars">Adicionar Centro de Custo</label>
                                    <br />
                                    <br />
                                    <div class="input-group" style="margin-left: 10px">
                                        <input id="new-event" type="text" class="form-control" placeholder="Nome do Centro de Custo">
                                        <div class="input-group-btn">
                                            <button id="add-new-event" type="button" class="btn btn-primary btn-flat">Adicionar</button>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="btn-group" style="margin: 10px">

                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Nome do Centro de Custo</th>
                                                    <th style="width: 3%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Centro de Relacionamento</td>
                                                    <td>
                                                        <i data-toggle="tooltip" class="fas fa-trash" style="margin-left: 10px; color: #dd4b39; font-size: large" title="Remover"></i>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Aditivos</td>
                                                    <td>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
