<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EscalaDeServicos.aspx.cs" Inherits="EscolaPro.Web.EscalaDeServicos" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Escala de Serviços</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

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
                        <label for="cars">Departamento:</label>
                        <br />
                        <select class="custom-select form-control">
                            <option value="1">T.I</option>
                            <option value="2">Financeiro</option>
                            <option value="3">Central de Atendimento</option>
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
                    <iframe src="CalendarioEscalaDeServicos.aspx" style="border: none;" width="100%" height="800px" runat="server"></iframe>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
