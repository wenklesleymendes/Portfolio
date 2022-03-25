<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MetasComissoes.aspx.cs" Inherits="EscolaPro.Web.MetasComissoes" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Metas e Comissões</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">


                <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Busca Avançada<span class="fas fa-sort-amount-down" style="margin-left: 10px"></span></a>


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


                <div class="btn-group" style="margin: 10px; width: 10%">
                    <div class="container" style="width: 30px; height: 30px; background-color: #E00000">
                        <label for="cars" style="margin-left: 20px">Matrícula Meta</label>
                    </div>
                </div>

                <div class="btn-group" style="margin: 10px; width: 10%">
                    <div class="container" style="width: 30px; height: 30px; background-color: #00CD00">
                        <label for="cars" style="margin-left: 20px">Matrículas Realizadas</label>
                    </div>
                </div>




                <div class="btn-group" style="margin: 10px; width: 30%">
                </div>


                <div class="btn-group" style="margin: 10px;">
                    <label for="cars">Comissão da Equipe</label>

                    <span class="label label-primary" style="font-size: small">R$ 120,00</span>
                </div>

                
                <div class="btn-group" style="margin: 10px; width: 2%">
                </div>

                <div class="btn-group" style="margin: 10px;">
                    <label for="cars">Comissão Individual</label>

                    <span class="label label-success" style="font-size: small;">R$ 20,00</span>
                </div>

                <iframe src="graficoMetas.html" style="border: none;" width="100%" height="1500px" runat="server"></iframe>

            </div>
        </div>
    </div>
</asp:Content>
