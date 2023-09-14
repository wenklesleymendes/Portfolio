<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configurador.aspx.cs" Inherits="EscolaPro.Web.Configurador" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="box-body">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Configurações do Sistema</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>

            <div class="box-body">

                <div class="form-group">

                    <div class="btn-group" style="margin: 10px; width: 20%">
                        <label for="cars">Operadora de Cartão:</label>
                        <br />
                        <select class="custom-select form-control">
                            <option value="1">Cielo</option>
                            <option value="2">Rede</option>
                            <option value="3">VR</option>
                        </select>
                    </div>

                   
                </div>

            </div>

            <div class="box-footer">
                 <a class="btn btn-success pull-right">Salvar Configuração</a>
            </div>
        </div>
    </div>
</asp:Content>
