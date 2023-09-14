<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TabDocumentos.aspx.cs" Inherits="EscolaPro.Web.TabDocumentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>


    <div class="box-body">
        <table id="example2" class="table table-bordered table-hover">
            <thead>
                <tr>
                    <th>Matricula</th>
                    <th>Unidade</th>
                    <th>Nome</th>
                    <th>Status Matricula</th>
                    <th>Curso</th>
                    <th>Ano</th>
                    <th>Semestre</th>
                    <th>E-mail</th>
                    <th>Celular</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>25298</td>
                    <td>Campinas</td>
                    <td>Andre Coutinho</td>
                    <td>Ativo</td>
                    <td>Ensino Médio</td>
                    <td>2020</td>
                    <td>1º Semestre</td>
                    <td>and@hotmail.com</td>
                    <td>19 997016077</td>
                    <td>
                        <button class="fa fa-search" style="border-color: transparent; background-color: transparent"
                            data-toggle="modal" data-target="#visualizarModal">
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
</body>
</html>
